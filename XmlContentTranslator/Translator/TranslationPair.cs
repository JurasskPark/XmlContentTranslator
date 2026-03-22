namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Translation language pair item.
    /// <para>Элемент языковой пары перевода.</para>
    /// </summary>
    public class TranslationPair
    {
        #region Variable

        /// <summary>
        /// Gets or sets the display name.
        /// <para>Получает или задает отображаемое имя.</para>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the language code.
        /// <para>Получает или задает код языка.</para>
        /// </summary>
        public string Code { get; set; } = string.Empty;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public TranslationPair()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public TranslationPair(string name, string code)
        {
            Name = name;
            Code = code;
        }

        /// <summary>
        /// Returns the display name.
        /// <para>Возвращает отображаемое имя.</para>
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion Basic
    }
}
