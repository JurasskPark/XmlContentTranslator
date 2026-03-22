using DebugerLog;
using Lang;
using ProjectSettings;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Main MDI shell of the application.
    /// <para>Главная MDI-оболочка приложения.</para>
    /// </summary>
    public partial class FrmStart : Form
    {
        #region Variable

        private readonly List<ToolStripMenuItem> _windowItems = new List<ToolStripMenuItem>();
        private string _projectFilePath = string.Empty;
        private string _languageDir = string.Empty;
        private bool _isRussian;

        public Project project = new Project();

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmStart()
        {
            InitializeComponent();

            _languageDir = ApplicationPath.LangDir;
            _projectFilePath = Path.Combine(ApplicationPath.StartPath, AppSettings.GetFileName);

            LoadProject(_projectFilePath);
            SyncManagerSettings();
            LoadLanguage(_isRussian);
            Translate();
        }

        /// <summary>
        /// Loads application language dictionaries.
        /// <para>Загружает словари языка приложения.</para>
        /// </summary>
        public void LoadLanguage(bool isRussian = false)
        {
            _isRussian = isRussian;

            string culture = isRussian ? "ru-RU" : "en-GB";
            string languageFile = ApplicationPath.ResolveLanguageFile(AppSettings.Code, culture);
            if (!File.Exists(languageFile))
            {
                MessageBox.Show(
                    this,
                    $"Language file not found:{Environment.NewLine}{languageFile}",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Locale.LoadDictionaries(languageFile, out string errMsg);
            string? dictName = GetType().FullName;
            if (!string.IsNullOrWhiteSpace(dictName))
            {
                Locale.GetDictionary(dictName);
            }

            Locale.GetDictionary("XmlContentTranslator.Forms.FrmStart");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmTranslate");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmGenerateXML");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmGenerateCode");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmSettings");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmFind");
            Locale.GetDictionary("XmlContentTranslator.Forms.FrmAboutBox");
            Locale.GetDictionary("Application");
            Locale.GetDictionary("ListView");     
            Locale.GetDictionary("Combobox");
            Locale.GetDictionary("DialogBox");
            Locale.GetDictionary("Files");

            ClientPhrases.Init();

            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                MessageBox.Show(this, errMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the current project.
        /// <para>Сохраняет текущий проект.</para>
        /// </summary>
        public void ProjectSave()
        {
            SaveProject(_projectFilePath);
        }

        /// <summary>
        /// Loads a project from file.
        /// <para>Загружает проект из файла.</para>
        /// </summary>
        private void LoadProject(string filePath)
        {
            Project loadedProject = new Project();
            if (!loadedProject.Load(filePath, out string errMsg) && !string.IsNullOrWhiteSpace(errMsg))
            {
                MessageBox.Show(this, errMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            project = loadedProject;
            _projectFilePath = filePath;
            _isRussian = project.LanguageIsRussian;
            UpdateLanguageIcon();
        }

        /// <summary>
        /// Saves a project to file.
        /// <para>Сохраняет проект в файл.</para>
        /// </summary>
        private void SaveProject(string filePath)
        {
            if (!project.Save(filePath, out string errMsg) && !string.IsNullOrWhiteSpace(errMsg))
            {
                MessageBox.Show(this, errMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _projectFilePath = filePath;
            SyncManagerSettings();
        }

        /// <summary>
        /// Synchronizes project settings with the manager.
        /// <para>Синхронизирует настройки проекта с менеджером.</para>
        /// </summary>
        private void SyncManagerSettings()
        {
            ManagerAssistant.Manager.PathProject = _projectFilePath;
            ManagerAssistant.Manager.IsDll = false;
            ManagerAssistant.Manager.LogPath = project.DebugerSettings.LogPath;
            ManagerAssistant.Manager.LogDays = project.DebugerSettings.LogDays;
            ManagerAssistant.Manager.LogWrite = project.DebugerSettings.LogWrite;
        }

        /// <summary>
        /// Updates the language icon.
        /// <para>Обновляет иконку языка.</para>
        /// </summary>
        private void UpdateLanguageIcon()
        {
            if (imgList.Images.Count < 2)
            {
                return;
            }

            tolLang.Image = _isRussian ? imgList.Images[1] : imgList.Images[0];
        }

        /// <summary>
        /// Shows a child MDI form.
        /// <para>Показывает дочернюю MDI-форму.</para>
        /// </summary>
        private void ShowChild(Form child)
        {
            child.MdiParent = this;
            child.WindowState = FormWindowState.Maximized;
            child.FormClosing += Child_FormClosing;
            child.Activated += Child_Activated;
            child.Deactivate += Child_Deactivated;
            child.Show();

            ToolStripMenuItem menuItem = new ToolStripMenuItem(child.Text)
            {
                Tag = child
            };
            menuItem.Click += ChildMenuItem_Click;

            _windowItems.Add(menuItem);
            tolWindows.DropDownItems.Insert(_windowItems.Count - 1, menuItem);
        }

        /// <summary>
        /// Displays an exception message.
        /// <para>Показывает сообщение об исключении.</para>
        /// </summary>
        private void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show(
                this,
                ex.InnerException?.Message ?? ex.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Translates the shell form.
        /// <para>Переводит форму оболочки.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        #endregion Basic

        #region Control

        #region ToolStrip

        /// <summary>
        /// Switches the interface language.
        /// <para>Переключает язык интерфейса.</para>
        /// </summary>
        private void tolLang_Click(object sender, EventArgs e)
        {
            _isRussian = !_isRussian;
            project.LanguageIsRussian = _isRussian;
            UpdateLanguageIcon();
            LoadLanguage(_isRussian);
            Translate();
            ProjectSave();
        }

        /// <summary>
        /// Opens the XML generator form.
        /// <para>Открывает форму генерации XML.</para>
        /// </summary>
        private void tolGenerateXML_Click(object sender, EventArgs e)
        {
            ShowChild(new FrmGenerateXML());
        }

        /// <summary>
        /// Opens the code generator form.
        /// <para>Открывает форму генерации кода.</para>
        /// </summary>
        private void tolGenerateCode_Click(object sender, EventArgs e)
        {
            ShowChild(new FrmGenerateCode());
        }

        /// <summary>
        /// Opens the translation form.
        /// <para>Открывает форму перевода.</para>
        /// </summary>
        private void tolTranslate_Click(object sender, EventArgs e)
        {
            ShowChild(new FrmTranslate
            {
                formParent = this,
                project = project
            });
        }

        /// <summary>
        /// Opens the settings form.
        /// <para>Открывает форму настроек.</para>
        /// </summary>
        private void tolSettings_Click(object sender, EventArgs e)
        {
            ShowChild(new FrmSettings
            {
                formParent = this,
                project = project
            });
        }

        /// <summary>
        /// Opens the About dialog.
        /// <para>Открывает диалог О программе.</para>
        /// </summary>
        private void tolAbout_Click(object sender, EventArgs e)
        {
            using (FrmAboutBox aboutBox = new FrmAboutBox())
            {
                aboutBox.ShowDialog(this);
            }
        }

        /// <summary>
        /// Arranges MDI windows in cascade.
        /// <para>Располагает MDI-окна каскадом.</para>
        /// </summary>
        private void tolCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// Arranges MDI windows horizontally.
        /// <para>Располагает MDI-окна горизонтально.</para>
        /// </summary>
        private void tolHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Arranges MDI windows vertically.
        /// <para>Располагает MDI-окна вертикально.</para>
        /// </summary>
        private void tolVertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// Closes all child windows.
        /// <para>Закрывает все дочерние окна.</para>
        /// </summary>
        private void tolCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren.ToArray())
            {
                child.Close();
            }
        }

        #endregion ToolStrip

        #region MDI

        /// <summary>
        /// Handles child form activation.
        /// <para>Обрабатывает активацию дочерней формы.</para>
        /// </summary>
        private void Child_Activated(object? sender, EventArgs e)
        {
            if (sender is FrmTranslate frmTranslate)
            {
                try
                {
                    ToolStripManager.Merge(frmTranslate.ToolStripForMerge, toolStripHost);
                }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex);
                }
            }
        }

        /// <summary>
        /// Handles child form deactivation.
        /// <para>Обрабатывает деактивацию дочерней формы.</para>
        /// </summary>
        private void Child_Deactivated(object? sender, EventArgs e)
        {
            try
            {
                ToolStripManager.RevertMerge(toolStripHost);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Handles child form closing.
        /// <para>Обрабатывает закрытие дочерней формы.</para>
        /// </summary>
        private void Child_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender is not Form form)
            {
                return;
            }

            try
            {
                ToolStripManager.RevertMerge(toolStripHost);
            }
            catch
            {
            }

            form.Activated -= Child_Activated;
            form.Deactivate -= Child_Deactivated;
            form.FormClosing -= Child_FormClosing;

            ToolStripMenuItem? menuItem = _windowItems.FirstOrDefault(item => ReferenceEquals(item.Tag, form));
            if (menuItem == null)
            {
                return;
            }

            tolWindows.DropDownItems.Remove(menuItem);
            _windowItems.Remove(menuItem);
            menuItem.Dispose();
        }

        /// <summary>
        /// Activates a child form from the windows menu.
        /// <para>Активирует дочернюю форму из меню окон.</para>
        /// </summary>
        private void ChildMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.Tag is Form form && !form.IsDisposed)
            {
                form.Activate();
            }
        }

        #endregion MDI

        #endregion Control
    }
}
