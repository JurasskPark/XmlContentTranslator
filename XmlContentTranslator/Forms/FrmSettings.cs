using Lang;
using ProjectSettings;
using XmlContentTranslator.Translator;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Application settings form.
    /// <para>Форма настроек приложения.</para>
    /// </summary>
    public partial class FrmSettings : Form
    {
        #region Variable
        private ITranslator _translator;

        private bool _modified;
        private bool _isBinding;

        public FrmStart formParent = null!;
        public Project project = null!;

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets a value indicating whether the form contains unsaved changes.
        /// <para>Получает или задает признак наличия несохраненных изменений.</para>
        /// </summary>
        private bool Modified
        {
            get => _modified;
            set
            {
                _modified = value;
                btnSave.Enabled = value;
            }
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmSettings()
        {
            InitializeComponent();

            _translator = new GoogleTranslator1();
            ConfigureUi();
        }

        /// <summary>
        /// Configures the form controls.
        /// <para>Настраивает элементы управления формы.</para>
        /// </summary>
        private void ConfigureUi()
        {
            FillComboWithLanguages(cmbTranslateFrom, _translator);
            FillComboWithLanguages(cmbTranslateTo, _translator);
            txtTranslationServices.Font = new Font("Consolas", 9F);
        }

        /// <summary>
        /// Translates the form.
        /// <para>Переводит форму.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// Binds project settings to controls.
        /// <para>Привязывает настройки проекта к элементам управления.</para>
        /// </summary>
        private void BindProject()
        {
            if (project == null)
            {
                return;
            }

            _isBinding = true;
            try
            {
                BindServices(project.TranslationServices);
                SelectLanguage(cmbTranslateFrom, project.FromLanguage);
                SelectLanguage(cmbTranslateTo, project.ToLanguage);
                SelectService(project.TranslationService);
                Modified = false;
            }
            finally
            {
                _isBinding = false;
            }
        }

        /// <summary>
        /// Binds translation services to controls.
        /// <para>Привязывает сервисы перевода к элементам управления.</para>
        /// </summary>
        private void BindServices(string? csv)
        {
            List<string> services = TranslationServiceRegistry.NormalizeServices(csv);
            cmbTranslationService.Items.Clear();
            foreach (string service in services)
            {
                cmbTranslationService.Items.Add(service);
            }

            txtTranslationServices.Text = string.Join(", ", services);
        }

        /// <summary>
        /// Writes control values back to the project configuration.
        /// <para>Записывает значения элементов управления обратно в конфигурацию проекта.</para>
        /// </summary>
        private void ControlsToConfig()
        {
            project.FromLanguage = GetSelectedLanguageCode(cmbTranslateFrom);
            project.ToLanguage = GetSelectedLanguageCode(cmbTranslateTo);

            List<string> services = TranslationServiceRegistry.NormalizeServices(txtTranslationServices.Text);
            project.TranslationServices = string.Join(",", services);

            string selectedService = cmbTranslationService.Text;
            if (!services.Contains(selectedService, StringComparer.OrdinalIgnoreCase))
            {
                selectedService = services[0];
            }

            project.TranslationService = selectedService;
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
        /// Selects a language in a ComboBox.
        /// <para>Выбирает язык в ComboBox.</para>
        /// </summary>
        private static void SelectLanguage(ComboBox comboBox, string? value)
        {
            if (comboBox.Items.Count == 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                comboBox.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is ComboBoxItem item &&
                    string.Equals(item.Value, value, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            int byText = comboBox.FindStringExact(value);
            comboBox.SelectedIndex = byText >= 0 ? byText : 0;
        }

        /// <summary>
        /// Selects a translation service.
        /// <para>Выбирает сервис перевода.</para>
        /// </summary>
        private void SelectService(string? serviceName)
        {
            if (cmbTranslationService.Items.Count == 0)
            {
                return;
            }

            int index = cmbTranslationService.FindStringExact(serviceName ?? string.Empty);
            cmbTranslationService.SelectedIndex = index >= 0 ? index : 0;
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
        /// Rebuilds the translation service list from text.
        /// <para>Перестраивает список сервисов перевода из текста.</para>
        /// </summary>
        private void RefreshServicesFromText()
        {
            string selectedService = cmbTranslationService.Text;
            List<string> services = TranslationServiceRegistry.NormalizeServices(txtTranslationServices.Text);

            _isBinding = true;
            try
            {
                cmbTranslationService.Items.Clear();
                foreach (string service in services)
                {
                    cmbTranslationService.Items.Add(service);
                }

                int selectedIndex = cmbTranslationService.FindStringExact(selectedService);
                cmbTranslationService.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            }
            finally
            {
                _isBinding = false;
            }
        }

        #endregion Basic

        #region Control

        /// <summary>
        /// Handles form load.
        /// <para>Обрабатывает загрузку формы.</para>
        /// </summary>
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Translate();
            BindProject();
        }

        /// <summary>
        /// Handles control changes.
        /// <para>Обрабатывает изменения элементов управления.</para>
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            if (_isBinding)
            {
                return;
            }

            if (sender == txtTranslationServices)
            {
                RefreshServicesFromText();
            }

            Modified = true;
        }

        /// <summary>
        /// Saves settings.
        /// <para>Сохраняет настройки.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();
            formParent.ProjectSave();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancels editing.
        /// <para>Отменяет редактирование.</para>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

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
                Text = string.IsNullOrWhiteSpace(text)
                    ? value
                    : text;
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
