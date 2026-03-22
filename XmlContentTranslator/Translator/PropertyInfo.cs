namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Extracted property information.
    /// <para>Информация об извлеченном свойстве.</para>
    /// </summary>
    public class PropertyInfo
    {
        #region Variable

        /// <summary>
        /// Gets or sets the control name.
        /// <para>Получает или задает имя элемента управления.</para>
        /// </summary>
        public string ControlName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the property name.
        /// <para>Получает или задает имя свойства.</para>
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the property value.
        /// <para>Получает или задает значение свойства.</para>
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parent menu name.
        /// <para>Получает или задает имя родительского меню.</para>
        /// </summary>
        public string ParentMenu { get; set; } = string.Empty;

        #endregion Variable
    }
}
