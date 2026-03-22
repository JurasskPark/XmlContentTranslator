#pragma warning disable 1591

using Lang;
using static System.Windows.Forms.LinkLabel;

namespace XmlContentTranslator
{
    /// <summary>
    /// Common client phrases.
    /// <para>Общие клиентские фразы.</para>
    /// </summary>
    public static class ClientPhrases
    {
        #region Variable

        public static string ProductName { get; private set; } = "XML Content Translator 6.0.0.0";
        public static string TitleProject { get; private set; } = "Project";
        public static string FilterProject { get; private set; } = "Project (*.xml)|*.xml|All files (*.*)|*.*";
        public static string TitleXMLFiles { get; private set; } = "XML files";
        public static string FilterXMLFiles { get; private set; } = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        public static string TitleDesignerFiles { get; private set; } = "Designer files";
        public static string FilterDesignerFiles { get; private set; } = "Designer files (*.Designer.cs)|*.Designer.cs|All files (*.*)|*.*";
        public static string All { get; private set; } = "All";
        public static string DeleteRow { get; private set; } = "Are you sure you want to delete this entry?";
        public static string Error { get; private set; } = "Error";
        public static string Message { get; private set; } = "Message";
        public static string NoDataToSave { get; private set; } = "There is no data to save.";
        public static string NamedFileNotFound { get; private set; } = "File {0} not found.";
        public static string FileDoesNotExist { get; private set; } = "The file does not exist!";
        public static string TranslationRequired { get; private set; } = "Translation required";
        public static string Tag { get; private set; } = "Tag";
        public static string Source { get; private set; } = "Source";
        public static string Target { get; private set; } = "Target";

        public static string TranslateSelectedLines { get; private set; } = "Translate selected lines";
        public static string LoadedSourceFile { get; private set; } = "Loaded source file: {0}";
        public static string LoadedTargetFile { get; private set; } = "Loaded target file: {0}";
        public static string SavedTargetFile { get; private set; } = "Saved target file: {0}";
        public static string SavedTo { get; private set; } = "Saved to {0}";
        public static string Ready { get; private set; } = "Ready.";
        public static string SelectSourceAndTargetLanguages { get; private set; } = "Select source and target languages.";
        public static string TranslatingItemsViaService { get; private set; } = "Translating {0} item(s) via {1}...";
        public static string TranslatedItemsViaService { get; private set; } = "Translated {0} item(s) via {1}.";
        public static string TranslatedItems { get; private set; } = "Translated {0} item(s).";
        public static string TranslationFailed { get; private set; } = "Translation failed.";
        public static string XmlContentTranslatorFileTitle { get; private set; } = "XML Content Translator - {0}";
        public static string UnsavedTargetTextWillBeLost { get; private set; } = "Unsaved target text will be lost. Continue?";
        public static string ItemsSelected { get; private set; } = "{0} item(s) selected";
        public static string CopiedItemsFromSource { get; private set; } = "Copied {0} item(s) from source.";
        public static string OpenSourceXmlFile { get; private set; } = "Open source XML file";
        public static string SaveTranslatedXmlFile { get; private set; } = "Save translated XML file";
        public static string OnlyOneXmlFileUpToMbIsSupported { get; private set; } = "Only one XML file up to {0} MB is supported.";

        #endregion Variable

        #region Basic

        static ClientPhrases()
        {
            ProductName = "XML Content Translator 6.0.0.0";
        }

        /// <summary>
        /// Initializes phrases from locale dictionaries.
        /// <para>Инициализирует фразы из словарей локали.</para>
        /// </summary>
        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Application");
            ProductName = dict[nameof(ProductName)];
            TitleProject = dict[nameof(TitleProject)];
            FilterProject = dict[nameof(FilterProject)];
            TitleXMLFiles = dict[nameof(TitleXMLFiles)];
            FilterXMLFiles = dict[nameof(FilterXMLFiles)];

            dict = Locale.GetDictionary("TreeView");
            Message = dict[nameof(Message)];

            dict = Locale.GetDictionary("ListView");
            Tag = dict[nameof(Tag)];
            Source = dict[nameof(Source)];
            Target = dict[nameof(Target)];

            dict = Locale.GetDictionary("Combobox");
            All = dict[nameof(All)];

            dict = Locale.GetDictionary("DialogBox");
            DeleteRow = dict[nameof(DeleteRow)];
            Error = dict[nameof(Error)];

            dict = Locale.GetDictionary("Files");
            NoDataToSave = dict[nameof(NoDataToSave)];
            NamedFileNotFound = dict[nameof(NamedFileNotFound)];
            TranslationRequired = dict[nameof(TranslationRequired)];

            dict = Locale.GetDictionary("Format");
            FileDoesNotExist = dict[nameof(FileDoesNotExist)];

            dict = Locale.GetDictionary("Message");
            TranslateSelectedLines = dict[nameof(TranslateSelectedLines)];
            LoadedSourceFile = dict[nameof(LoadedSourceFile)];
            LoadedTargetFile = dict[nameof(LoadedTargetFile)];
            SavedTargetFile = dict[nameof(SavedTargetFile)];
            SavedTo = dict[nameof(SavedTo)];
            Ready = dict[nameof(Ready)];
            SelectSourceAndTargetLanguages = dict[nameof(SelectSourceAndTargetLanguages)];
            TranslatingItemsViaService = dict[nameof(TranslatingItemsViaService)];
            TranslatedItemsViaService = dict[nameof(TranslatedItemsViaService)];
            TranslatedItems = dict[nameof(TranslatedItems)];
            TranslationFailed = dict[nameof(TranslationFailed)];
            XmlContentTranslatorFileTitle = dict[nameof(XmlContentTranslatorFileTitle)];
            UnsavedTargetTextWillBeLost = dict[nameof(UnsavedTargetTextWillBeLost)];
            ItemsSelected = dict[nameof(ItemsSelected)];
            CopiedItemsFromSource = dict[nameof(CopiedItemsFromSource)];
            OpenSourceXmlFile = dict[nameof(OpenSourceXmlFile)];
            SaveTranslatedXmlFile = dict[nameof(SaveTranslatedXmlFile)];
            OnlyOneXmlFileUpToMbIsSupported = dict[nameof(OnlyOneXmlFileUpToMbIsSupported)];
        }

        #endregion Basic
    }
}
