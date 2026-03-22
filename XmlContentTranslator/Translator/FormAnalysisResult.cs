namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Result of WinForms designer analysis.
    /// <para>Результат анализа WinForms designer-файла.</para>
    /// </summary>
    public class FormAnalysisResult
    {
        #region Variable

        /// <summary>
        /// Gets or sets the file name.
        /// <para>Получает или задает имя файла.</para>
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full file path.
        /// <para>Получает или задает полный путь к файлу.</para>
        /// </summary>
        public string FullPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the namespace.
        /// <para>Получает или задает пространство имен.</para>
        /// </summary>
        public string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the class name.
        /// <para>Получает или задает имя класса.</para>
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full class name.
        /// <para>Получает или задает полное имя класса.</para>
        /// </summary>
        public string FullClassName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the extracted properties.
        /// <para>Получает или задает извлеченные свойства.</para>
        /// </summary>
        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();

        #endregion Variable
    }
}
