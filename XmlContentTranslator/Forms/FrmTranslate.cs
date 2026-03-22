using Lang;
using ProjectSettings;
using System.Text;
using System.Web;
using System.Xml;
using XmlContentTranslator.Core;
using XmlContentTranslator.Translator;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Main form for translating XML content files.
    /// <para>Основная форма перевода XML-файлов.</para>
    /// </summary>
    public partial class FrmTranslate : Form
    {
        #region Variable

        private const int MaxFileSizeMb = 20;
        private const int TagColumnWidth = 260;
        private const int TextColumnWidth = 240;

        private readonly FrmFind _formFind;
        private readonly XmlTranslationWorkspaceService _workspaceService;
        private readonly Dictionary<string, TreeNode> _treeNodesByPath =
            new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

        private TranslationWorkspace? _workspace;
        private ITranslator _translator;
        private List<string> _availableServices;
        private string _activeServiceName;
        private bool _isUpdatingEditor;

        public FrmStart? formParent;
        public Project? project;

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets the ToolStrip used for MDI merge.
        /// <para>Получает ToolStrip для MDI-слияния.</para>
        /// </summary>
        public ToolStrip ToolStripForMerge => mnuMenu;

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmTranslate()
        {
            InitializeComponent();

            _formFind = new FrmFind();
            _workspaceService = new XmlTranslationWorkspaceService();
            _translator = new GoogleTranslator1();
            _availableServices = new List<string> { "GoogleWeb", "YandexWeb" };
            _activeServiceName = "GoogleWeb";

            ConfigureUi();
        }

        /// <summary>
        /// Configures the user interface.
        /// <para>Настраивает пользовательский интерфейс.</para>
        /// </summary>
        private void ConfigureUi()
        {
            tssStatus1.Text = string.Empty;
            tssStatus2.Text = string.Empty;

            txtLog.ReadOnly = true;
            txtLog.BackColor = SystemColors.Window;

            FillLanguageCombos();
            UpdateToolsMenu();
        }

        /// <summary>
        /// Fills language ComboBox controls.
        /// <para>Заполняет ComboBox выбора языков.</para>
        /// </summary>
        private void FillLanguageCombos()
        {
            FillComboWithLanguages(cmbTranslateFrom, _translator);
            FillComboWithLanguages(cmbTranslateTo, _translator);
        }

        /// <summary>
        /// Fills a ComboBox with translator languages.
        /// <para>Заполняет ComboBox языками переводчика.</para>
        /// </summary>
        private static void FillComboWithLanguages(ComboBox comboBox, ITranslator translator)
        {
            comboBox.Items.Clear();
            foreach (TranslationPair pair in translator.GetTranslationPairs())
            {
                comboBox.Items.Add(new ComboBoxItem(pair.Name, pair.Code));
            }
        }

        /// <summary>
        /// Updates the tools menu according to the active service.
        /// <para>Обновляет меню инструментов согласно активному сервису.</para>
        /// </summary>
        private void UpdateToolsMenu()
        {
            string serviceTitle = $"{ClientPhrases.TranslateSelectedLines} ({_translator.GetName()})";
            mnuToolsTranslateSelectedLines.Text = serviceTitle;
            cmnuTranslateGoogle.Text = serviceTitle;

            UpdateTranslationServiceMenuItem(mnuToolsTranslationServiceGoogleWeb, "GoogleWeb");
            UpdateTranslationServiceMenuItem(mnuToolsTranslationServiceYandexWeb, "YandexWeb");
        }

        /// <summary>
        /// Applies project configuration to the form.
        /// <para>Применяет конфигурацию проекта к форме.</para>
        /// </summary>
        private void ApplyProjectConfiguration()
        {
            if (project == null)
            {
                return;
            }

            _availableServices = TranslationServiceRegistry.NormalizeServices(project.TranslationServices);
            _activeServiceName = _availableServices.Contains(project.TranslationService, StringComparer.OrdinalIgnoreCase)
                ? project.TranslationService
                : _availableServices[0];
            _translator = TranslationServiceRegistry.Create(_activeServiceName);

            FillLanguageCombos();
            SelectComboItem(cmbTranslateFrom, project.FromLanguage);
            SelectComboItem(cmbTranslateTo, project.ToLanguage);
            UpdateToolsMenu();
        }

        /// <summary>
        /// Selects an item in a ComboBox by text or value.
        /// <para>Выбирает элемент ComboBox по тексту или значению.</para>
        /// </summary>
        private static void SelectComboItem(ComboBox comboBox, string textOrValue)
        {
            if (string.IsNullOrWhiteSpace(textOrValue))
            {
                comboBox.SelectedIndex = comboBox.Items.Count > 0 ? 0 : -1;
                return;
            }

            int byText = comboBox.FindStringExact(textOrValue);
            if (byText >= 0)
            {
                comboBox.SelectedIndex = byText;
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is ComboBoxItem item &&
                    string.Equals(item.Value, textOrValue, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            comboBox.SelectedIndex = comboBox.Items.Count > 0 ? 0 : -1;
        }


        /// <summary>
        /// Initializing ListView columns for translation.
        /// <para>Иницилизация столбцов ListView для перевода.</para>
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmTag.Name = nameof(clmTag.Name);
            clmSource.Name = nameof(clmSource.Name);
            clmTarget.Name = nameof(clmTarget.Name);
        }

        /// <summary>
        /// Translates the form and context menu.
        /// <para>Переводит форму и контекстное меню.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuMenu, GetType().FullName);
            // tranlaste the listview
            FormTranslator.Translate(lstLanguageTags, GetType().FullName);
        }

        /// <summary>
        /// Opens a source XML workspace.
        /// <para>Открывает рабочую область исходного XML.</para>
        /// </summary>
        private void OpenWorkspace(string sourceFilePath)
        {
            try
            {
                _workspace = _workspaceService.LoadSource(sourceFilePath);
                TryOpenTargetDocument();
                BindWorkspace();
                Log(string.Format(ClientPhrases.LoadedSourceFile, sourceFilePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens or creates the target document.
        /// <para>Открывает или создает целевой документ.</para>
        /// </summary>
        private void TryOpenTargetDocument()
        {
            if (_workspace == null)
            {
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = ClientPhrases.TitleXMLFiles;
                dialog.Filter = ClientPhrases.FilterXMLFiles;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _workspaceService.LoadTarget(_workspace, dialog.FileName);
                    return;
                }
            }

            _workspaceService.CreateEmptyTarget(_workspace);
        }

        /// <summary>
        /// Binds workspace data to the tree and list.
        /// <para>Привязывает данные рабочей области к дереву и списку.</para>
        /// </summary>
        private void BindWorkspace()
        {
            if (_workspace == null)
            {
                return;
            }

            lstLanguageTags.BeginUpdate();
            trvProject.BeginUpdate();

            try
            {
                _treeNodesByPath.Clear();
                lstLanguageTags.Items.Clear();
                trvProject.Nodes.Clear();

                lstLanguageTags.Columns[1].Text = _workspace.SourceLanguageName;
                lstLanguageTags.Columns[2].Text = _workspace.TargetLanguageName;

                if (_workspace.SourceDocument.DocumentElement != null)
                {
                    BuildTree(_workspace.SourceDocument.DocumentElement, string.Empty, null);
                }

                foreach (TranslationEntry entry in _workspace.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.DisplayKey);
                    item.SubItems.Add(entry.SourceText);
                    item.SubItems.Add(entry.TargetText);
                    item.Tag = entry;
                    lstLanguageTags.Items.Add(item);
                }

                if (lstLanguageTags.Items.Count > 0)
                {
                    SelectListItem(0);
                }
            }
            finally
            {
                trvProject.EndUpdate();
                lstLanguageTags.EndUpdate();
                AdjustLastColumnWidth();
                HighlightLinesWithSameText();
            }
        }

        /// <summary>
        /// Builds the tree view recursively.
        /// <para>Строит дерево рекурсивно.</para>
        /// </summary>
        private void BuildTree(XmlNode node, string parentPath, TreeNode? parentTreeNode)
        {
            string currentPath = ResolveNodePath(node, parentPath);
            string treeText = node.Attributes?["key"]?.InnerText ?? node.Name;
            TreeNode treeNode = new TreeNode(treeText)
            {
                Tag = currentPath
            };

            _treeNodesByPath[currentPath] = treeNode;

            if (parentTreeNode == null)
            {
                trvProject.Nodes.Add(treeNode);
            }
            else
            {
                parentTreeNode.Nodes.Add(treeNode);
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    BuildTree(childNode, currentPath, treeNode);
                }
            }
        }

        /// <summary>
        /// Resolves a stable node path.
        /// <para>Определяет стабильный путь узла.</para>
        /// </summary>
        private static string ResolveNodePath(XmlNode node, string parentPath)
        {
            string? key = node.Attributes?["key"]?.InnerText;
            if (!string.IsNullOrWhiteSpace(key))
            {
                return $"{parentPath}/{node.Name}[@key='{key}']";
            }

            int siblingIndex = 0;
            XmlNode? previous = node.PreviousSibling;
            while (previous != null)
            {
                if (previous.NodeType == XmlNodeType.Element && previous.Name == node.Name)
                {
                    siblingIndex++;
                }

                previous = previous.PreviousSibling;
            }

            return $"{parentPath}/{node.Name}[{siblingIndex}]";
        }

        /// <summary>
        /// Translates selected rows.
        /// <para>Переводит выбранные строки.</para>
        /// </summary>
        private void TranslateSelectedRows()
        {
            if (_workspace == null || _workspace.TargetDocument == null)
            {
                return;
            }

            if (cmbTranslateFrom.SelectedItem is not ComboBoxItem fromLanguage ||
                cmbTranslateTo.SelectedItem is not ComboBoxItem toLanguage)
            {
                MessageBox.Show(this, ClientPhrases.SelectSourceAndTargetLanguages, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<TranslationEntry> selectedEntries = GetSelectedEntries();
            if (selectedEntries.Count == 0)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            UpdateStatus(string.Format(ClientPhrases.TranslatingItemsViaService, selectedEntries.Count, _translator.GetName()));

            try
            {
                List<string> sourceTexts = selectedEntries.Select(x => NormalizeForTranslation(x.SourceText)).ToList();
                StringBuilder log = new StringBuilder();
                List<string> translated = _translator.Translate(fromLanguage.Value, toLanguage.Value, sourceTexts, log);

                if (translated.Count != selectedEntries.Count)
                {
                    throw new InvalidOperationException("Translation service returned an unexpected number of items.");
                }

                for (int i = 0; i < selectedEntries.Count; i++)
                {
                    selectedEntries[i].TargetText = CleanupTranslatedText(translated[i]);
                }

                RefreshListFromEntries(selectedEntries);
                HighlightLinesWithSameText();
                Log(log.Length > 0
                    ? log.ToString()
                    : string.Format(ClientPhrases.TranslatedItemsViaService, selectedEntries.Count, _translator.GetName()));
                UpdateStatus(string.Format(ClientPhrases.TranslatedItems, selectedEntries.Count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus(ClientPhrases.TranslationFailed);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets selected translation entries.
        /// <para>Получает выбранные элементы перевода.</para>
        /// </summary>
        private List<TranslationEntry> GetSelectedEntries()
        {
            return lstLanguageTags.SelectedItems
                .Cast<ListViewItem>()
                .Select(x => x.Tag)
                .OfType<TranslationEntry>()
                .ToList();
        }

        /// <summary>
        /// Normalizes text for translation service input.
        /// <para>Нормализует текст для передачи в сервис перевода.</para>
        /// </summary>
        private static string NormalizeForTranslation(string text)
        {
            return text.Replace(Environment.NewLine, "<br/>", StringComparison.Ordinal).Trim();
        }

        /// <summary>
        /// Cleans translated text returned by the service.
        /// <para>Очищает переведенный текст, возвращенный сервисом.</para>
        /// </summary>
        private static string CleanupTranslatedText(string text)
        {
            return HttpUtility.HtmlDecode(text)
                .Replace("<br>", Environment.NewLine, StringComparison.Ordinal)
                .Replace("<br/>", Environment.NewLine, StringComparison.Ordinal)
                .Replace("<br />", Environment.NewLine, StringComparison.Ordinal)
                .Trim();
        }

        /// <summary>
        /// Refreshes list rows from updated entries.
        /// <para>Обновляет строки списка по измененным элементам.</para>
        /// </summary>
        private void RefreshListFromEntries(IEnumerable<TranslationEntry> entries)
        {
            HashSet<string> updated = new HashSet<string>(entries.Select(x => x.Path), StringComparer.OrdinalIgnoreCase);
            foreach (ListViewItem item in lstLanguageTags.Items)
            {
                if (item.Tag is TranslationEntry entry && updated.Contains(entry.Path))
                {
                    item.SubItems[2].Text = entry.TargetText;
                }
            }

            if (lstLanguageTags.SelectedItems.Count == 1)
            {
                SyncEditorWithSelection();
            }
        }

        /// <summary>
        /// Saves the workspace to file.
        /// <para>Сохраняет рабочую область в файл.</para>
        /// </summary>
        private void SaveWorkspace(string filePath)
        {
            if (_workspace == null)
            {
                return;
            }

            try
            {
                _workspaceService.SaveTarget(_workspace, filePath);
                Text = string.Format(ClientPhrases.XmlContentTranslatorFileTitle, Path.GetFileName(filePath));
                UpdateStatus(string.Format(ClientPhrases.SavedTo, filePath));
                Log(string.Format(ClientPhrases.SavedTargetFile, filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clears the current workspace.
        /// <para>Очищает текущую рабочую область.</para>
        /// </summary>
        private void ClearWorkspace()
        {
            _workspace = null;
            _treeNodesByPath.Clear();
            trvProject.Nodes.Clear();
            lstLanguageTags.Items.Clear();
            txtCurrentText.Clear();
            txtLog.Clear();
            lstLanguageTags.Columns[1].Text = ClientPhrases.Source;
            lstLanguageTags.Columns[2].Text = ClientPhrases.Target;
            UpdateStatus(ClientPhrases.Ready);
        }

        /// <summary>
        /// Confirms that unsaved changes can be discarded.
        /// <para>Подтверждает возможность потерять несохраненные изменения.</para>
        /// </summary>
        private bool ConfirmDiscardChanges()
        {
            if (_workspace == null)
            {
                return true;
            }

            bool hasChanges = _workspace.Entries.Any(x => !string.Equals(x.SourceText, x.TargetText, StringComparison.Ordinal));
            if (!hasChanges)
            {
                return true;
            }

            return MessageBox.Show(this, ClientPhrases.UnsavedTargetTextWillBeLost, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        /// <summary>
        /// Finds the next matching row.
        /// <para>Находит следующую подходящую строку.</para>
        /// </summary>
        private void FindNextMatch()
        {
            if (string.IsNullOrWhiteSpace(_formFind.SearchText))
            {
                return;
            }

            int startIndex = lstLanguageTags.SelectedItems.Count > 0
                ? lstLanguageTags.SelectedItems[0].Index + 1
                : 0;

            for (int i = startIndex; i < lstLanguageTags.Items.Count; i++)
            {
                ListViewItem item = lstLanguageTags.Items[i];
                string haystack = _formFind.SearchTags
                    ? item.Text
                    : $"{item.SubItems[1].Text} {item.SubItems[2].Text}";

                if (haystack.Contains(_formFind.SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    SelectListItem(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Selects the next blank translation line.
        /// <para>Выбирает следующую пустую строку перевода.</para>
        /// </summary>
        private void GoToNextBlankLine()
        {
            int startIndex = lstLanguageTags.SelectedItems.Count > 0
                ? lstLanguageTags.SelectedItems[0].Index + 1
                : 0;

            for (int i = startIndex; i < lstLanguageTags.Items.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lstLanguageTags.Items[i].SubItems[2].Text))
                {
                    SelectListItem(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Selects a list item by index.
        /// <para>Выбирает элемент списка по индексу.</para>
        /// </summary>
        private void SelectListItem(int index)
        {
            foreach (ListViewItem item in lstLanguageTags.SelectedItems)
            {
                item.Selected = false;
            }

            if (index < 0 || index >= lstLanguageTags.Items.Count)
            {
                return;
            }

            lstLanguageTags.Items[index].Selected = true;
            lstLanguageTags.Items[index].Focused = true;
            lstLanguageTags.Items[index].EnsureVisible();
        }

        /// <summary>
        /// Synchronizes the editor with the selected list row.
        /// <para>Синхронизирует редактор с выбранной строкой списка.</para>
        /// </summary>
        private void SyncEditorWithSelection()
        {
            _isUpdatingEditor = true;
            try
            {
                if (lstLanguageTags.SelectedItems.Count != 1 ||
                    lstLanguageTags.SelectedItems[0].Tag is not TranslationEntry entry)
                {
                    txtCurrentText.Enabled = false;
                    txtCurrentText.Text = string.Empty;
                    tssStatus2.Text = string.Format(ClientPhrases.ItemsSelected, lstLanguageTags.SelectedItems.Count);
                    return;
                }

                txtCurrentText.Enabled = true;
                txtCurrentText.Text = entry.TargetText;
                tssStatus2.Text = $"{entry.DisplayKey}    {lstLanguageTags.SelectedItems[0].Index + 1}/{lstLanguageTags.Items.Count}";

                if (_treeNodesByPath.TryGetValue(entry.ParentPath, out TreeNode? treeNode) ||
                    _treeNodesByPath.TryGetValue(entry.Path, out treeNode))
                {
                    trvProject.SelectedNode = treeNode;
                    treeNode.EnsureVisible();
                }
            }
            finally
            {
                _isUpdatingEditor = false;
                HighlightLinesWithSameText();
            }
        }

        /// <summary>
        /// Highlights rows based on translation state.
        /// <para>Подсвечивает строки по состоянию перевода.</para>
        /// </summary>
        private void HighlightLinesWithSameText()
        {
            foreach (ListViewItem item in lstLanguageTags.Items)
            {
                if (item.SubItems.Count < 3)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(item.SubItems[2].Text))
                {
                    item.BackColor = Color.MistyRose;
                }
                else if (string.Equals(item.SubItems[1].Text.Trim(), item.SubItems[2].Text.Trim(), StringComparison.Ordinal))
                {
                    item.BackColor = Color.LemonChiffon;
                }
                else
                {
                    item.BackColor = lstLanguageTags.BackColor;
                }
            }
        }

        /// <summary>
        /// Loads a target file into the current workspace.
        /// <para>Загружает целевой файл в текущую рабочую область.</para>
        /// </summary>
        private void LoadTargetFile(string filePath)
        {
            if (_workspace == null)
            {
                return;
            }

            try
            {
                _workspaceService.LoadTarget(_workspace, filePath);
                BindWorkspace();
                Log(string.Format(ClientPhrases.LoadedTargetFile, filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves selected UI values to the project configuration.
        /// <para>Сохраняет выбранные значения интерфейса в конфигурацию проекта.</para>
        /// </summary>
        private void PersistProjectConfiguration()
        {
            if (project == null || formParent == null)
            {
                return;
            }

            project.FromLanguage = GetSelectedLanguageCode(cmbTranslateFrom);
            project.ToLanguage = GetSelectedLanguageCode(cmbTranslateTo);
            project.TranslationService = _activeServiceName;
            project.TranslationServices = string.Join(",", _availableServices);
            formParent.ProjectSave();
        }

        /// <summary>
        /// Gets the selected language code.
        /// <para>Получает код выбранного языка.</para>
        /// </summary>
        private static string GetSelectedLanguageCode(ComboBox comboBox)
        {
            return comboBox.SelectedItem is ComboBoxItem item
                ? item.Value
                : comboBox.Text;
        }

        /// <summary>
        /// Adjusts the width of the last list column.
        /// <para>Подстраивает ширину последнего столбца списка.</para>
        /// </summary>
        private void AdjustLastColumnWidth()
        {
            if (lstLanguageTags.Columns.Count == 0)
            {
                return;
            }

            int fixedWidth = 0;
            for (int i = 0; i < lstLanguageTags.Columns.Count - 1; i++)
            {
                fixedWidth += lstLanguageTags.Columns[i].Width;
            }

            int width = Math.Max(120, lstLanguageTags.ClientSize.Width - fixedWidth - 4);
            lstLanguageTags.Columns[lstLanguageTags.Columns.Count - 1].Width = width;
        }

        /// <summary>
        /// Updates the status bar text.
        /// <para>Обновляет текст строки состояния.</para>
        /// </summary>
        private void UpdateStatus(string text)
        {
            tssStatus1.Text = text;
        }

        /// <summary>
        /// Appends a message to the log.
        /// <para>Добавляет сообщение в журнал.</para>
        /// </summary>
        private void Log(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {text}{Environment.NewLine}");
        }

        #endregion Basic

        #region Control

        #region Form

        /// <summary>
        /// Handles form load.
        /// <para>Обрабатывает загрузку формы.</para>
        /// </summary>
        private void FrmTranslate_Load(object? sender, EventArgs e)
        {
            ApplyProjectConfiguration();
            SetListViewColumnNames();
            Translate();
        }

        /// <summary>
        /// Handles form key presses.
        /// <para>Обрабатывает нажатия клавиш формы.</para>
        /// </summary>
        private void FrmTranslate_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                GoToNextBlankLine();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                mnuEditFind_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles form closing.
        /// <para>Обрабатывает закрытие формы.</para>
        /// </summary>
        private void FrmTranslate_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!ConfirmDiscardChanges())
            {
                e.Cancel = true;
                return;
            }

            PersistProjectConfiguration();
        }

        /// <summary>
        /// Handles form resize.
        /// <para>Обрабатывает изменение размера формы.</para>
        /// </summary>
        private void FrmTranslate_Resize(object? sender, EventArgs e)
        {
            AdjustLastColumnWidth();
        }

        /// <summary>
        /// Handles the end of form resize.
        /// <para>Обрабатывает завершение изменения размера формы.</para>
        /// </summary>
        private void FrmTranslate_ResizeEnd(object? sender, EventArgs e)
        {
            AdjustLastColumnWidth();
        }

        #endregion Form

        #region Menu

        /// <summary>
        /// Updates a translation service menu item state.
        /// <para>Обновляет состояние пункта меню сервиса перевода.</para>
        /// </summary>
        private void UpdateTranslationServiceMenuItem(ToolStripMenuItem menuItem, string serviceName)
        {
            bool isAvailable = _availableServices.Contains(serviceName, StringComparer.OrdinalIgnoreCase);

            menuItem.Visible = isAvailable;
            menuItem.Checked = isAvailable &&
                string.Equals(serviceName, _activeServiceName, StringComparison.OrdinalIgnoreCase);
            menuItem.Tag = serviceName;
        }

        /// <summary>
        /// Handles translation service menu selection.
        /// <para>Обрабатывает выбор сервиса перевода в меню.</para>
        /// </summary>
        private void mnuToolsTranslationService_Click(object? sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem menuItem || menuItem.Tag is not string serviceName)
            {
                return;
            }

            string fromLanguage = GetSelectedLanguageCode(cmbTranslateFrom);
            string toLanguage = GetSelectedLanguageCode(cmbTranslateTo);

            _activeServiceName = serviceName;
            _translator = TranslationServiceRegistry.Create(serviceName);

            FillLanguageCombos();
            SelectComboItem(cmbTranslateFrom, fromLanguage);
            SelectComboItem(cmbTranslateTo, toLanguage);

            PersistProjectConfiguration();
            UpdateToolsMenu();
        }

        /// <summary>
        /// Creates a new translation workspace.
        /// <para>Создает новую рабочую область перевода.</para>
        /// </summary>
        private void mnuFileNew_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscardChanges())
            {
                return;
            }

            ClearWorkspace();
        }

        /// <summary>
        /// Opens a source XML file.
        /// <para>Открывает исходный XML-файл.</para>
        /// </summary>
        private void mnuFileOpen_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscardChanges())
            {
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = ClientPhrases.OpenSourceXmlFile;
                dialog.Filter = ClientPhrases.FilterXMLFiles;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                OpenWorkspace(dialog.FileName);
            }
        }

        /// <summary>
        /// Translates the selected lines.
        /// <para>Переводит выбранные строки.</para>
        /// </summary>
        private void mnuToolsTranslateSelectedLines_Click(object? sender, EventArgs e)
        {
            TranslateSelectedRows();
        }

        /// <summary>
        /// Copies the source value into the target field.
        /// <para>Копирует исходное значение в целевое поле.</para>
        /// </summary>
        private void cmnuSetValueFromMaster_Click(object? sender, EventArgs e)
        {
            List<TranslationEntry> selectedEntries = GetSelectedEntries();
            foreach (TranslationEntry entry in selectedEntries)
            {
                entry.TargetText = entry.SourceText;
            }

            RefreshListFromEntries(selectedEntries);
            HighlightLinesWithSameText();
            UpdateStatus(string.Format(ClientPhrases.CopiedItemsFromSource, selectedEntries.Count));
        }

        /// <summary>
        /// Saves the current target document.
        /// <para>Сохраняет текущий целевой документ.</para>
        /// </summary>
        private void mnuFileSave_Click(object? sender, EventArgs e)
        {
            if (_workspace == null || _workspace.TargetDocument == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(_workspace.TargetFilePath))
            {
                mnuFileSaveAs_Click(sender, e);
                return;
            }

            SaveWorkspace(_workspace.TargetFilePath);
        }

        /// <summary>
        /// Saves the target document with a new name.
        /// <para>Сохраняет целевой документ под новым именем.</para>
        /// </summary>
        private void mnuFileSaveAs_Click(object? sender, EventArgs e)
        {
            if (_workspace == null || _workspace.TargetDocument == null)
            {
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = ClientPhrases.SaveTranslatedXmlFile;
                dialog.Filter = ClientPhrases.FilterXMLFiles;
                dialog.DefaultExt = "xml";

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                SaveWorkspace(dialog.FileName);
            }
        }

        /// <summary>
        /// Opens the search dialog.
        /// <para>Открывает диалог поиска.</para>
        /// </summary>
        private void mnuEditFind_Click(object? sender, EventArgs e)
        {
            if (_formFind.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            FindNextMatch();
        }

        #endregion Menu

        #region Button

        /// <summary>
        /// Goes to the next blank translation line.
        /// <para>Переходит к следующей пустой строке перевода.</para>
        /// </summary>
        private void btnGoToNextBlankLine_Click(object? sender, EventArgs e)
        {
            GoToNextBlankLine();
        }

        #endregion Button

        #region ListView

        /// <summary>
        /// Handles the selected index changed event of the language tags list.
        /// <para>Обрабатывает изменение выбранного элемента списка языковых тегов.</para>
        /// </summary>
        private void lstLanguageTags_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SyncEditorWithSelection();
        }

        /// <summary>
        /// Handles double click on the language tags list.
        /// <para>Обрабатывает двойной щелчок по списку языковых тегов.</para>
        /// </summary>
        private void listViewLanguageTags_DoubleClick(object? sender, EventArgs e)
        {
            if (lstLanguageTags.SelectedItems.Count != 1 ||
                lstLanguageTags.SelectedItems[0].Tag is not TranslationEntry entry)
            {
                return;
            }

            if (_treeNodesByPath.TryGetValue(entry.ParentPath, out TreeNode? node) ||
                _treeNodesByPath.TryGetValue(entry.Path, out node))
            {
                trvProject.SelectedNode = node;
                node.EnsureVisible();
            }
        }

        /// <summary>
        /// Handles drag enter over the language tags list.
        /// <para>Обрабатывает вход перетаскиваемых данных в список языковых тегов.</para>
        /// </summary>
        private void lstLanguageTags_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        /// <summary>
        /// Handles file drop on the language tags list.
        /// <para>Обрабатывает сброс файла в список языковых тегов.</para>
        /// </summary>
        private void lstLanguageTags_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is not string[] files || files.Length != 1)
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(files[0]);
            if (!fileInfo.Exists || fileInfo.Length > MaxFileSizeMb * 1024L * 1024L)
            {
                MessageBox.Show(this, string.Format(ClientPhrases.OnlyOneXmlFileUpToMbIsSupported, MaxFileSizeMb), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_workspace == null)
            {
                OpenWorkspace(files[0]);
            }
            else
            {
                LoadTargetFile(files[0]);
            }
        }

        #endregion ListView

        #region TreeView

        /// <summary>
        /// Handles tree node selection.
        /// <para>Обрабатывает выбор узла дерева.</para>
        /// </summary>
        private void trvProject_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not string nodePath)
            {
                return;
            }

            for (int i = 0; i < lstLanguageTags.Items.Count; i++)
            {
                if (lstLanguageTags.Items[i].Tag is TranslationEntry entry &&
                    (entry.Path.StartsWith(nodePath, StringComparison.OrdinalIgnoreCase) ||
                     entry.ParentPath.StartsWith(nodePath, StringComparison.OrdinalIgnoreCase)))
                {
                    SelectListItem(i);
                    return;
                }
            }
        }

        #endregion TreeView

        #region TextBox

        /// <summary>
        /// Handles text changes in the current translation editor.
        /// <para>Обрабатывает изменение текста в редакторе текущего перевода.</para>
        /// </summary>
        private void txtCurrentText_TextChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingEditor)
            {
                return;
            }

            if (lstLanguageTags.SelectedItems.Count != 1 ||
                lstLanguageTags.SelectedItems[0].Tag is not TranslationEntry entry)
            {
                return;
            }

            entry.TargetText = txtCurrentText.Text;
            lstLanguageTags.SelectedItems[0].SubItems[2].Text = entry.TargetText;
            HighlightLinesWithSameText();
        }

        /// <summary>
        /// Handles key presses in the current translation editor.
        /// <para>Обрабатывает нажатия клавиш в редакторе текущего перевода.</para>
        /// </summary>
        private void txtCurrentText_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                GoToNextBlankLine();
                e.Handled = true;
            }
        }

        #endregion TextBox

        #endregion Control

        #region Support class

        /// <summary>
        /// ComboBox language item.
        /// <para>Элемент языка для ComboBox.</para>
        /// </summary>
        private sealed class ComboBoxItem
        {
            /// <summary>
            /// Initializes a new instance of the class.
            /// <para>Инициализирует новый экземпляр класса.</para>
            /// </summary>
            public ComboBoxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            /// <summary>
            /// Gets the display text.
            /// <para>Получает отображаемый текст.</para>
            /// </summary>
            public string Text { get; }

            /// <summary>
            /// Gets the language value.
            /// <para>Получает значение языка.</para>
            /// </summary>
            public string Value { get; }

            /// <summary>
            /// Returns the display text.
            /// <para>Возвращает отображаемый текст.</para>
            /// </summary>
            public override string ToString()
            {
                return Text;
            }
        }


        #endregion Support class
    }
}
