namespace XmlContentTranslator
{
    /// <summary>
    /// Form translator options.
    /// <para>Параметры переводчика форм.</para>
    /// </summary>
    public class FormTranslatorOptions
    {
        #region Variable

        /// <summary>
        /// Gets the ToolTip component to translate.
        /// <para>Получает компонент ToolTip для перевода.</para>
        /// </summary>
        public ToolTip? ToolTip { get; init; }

        /// <summary>
        /// Gets context menus to translate.
        /// <para>Получает контекстные меню для перевода.</para>
        /// </summary>
        public ContextMenuStrip[]? ContextMenus { get; init; }

        /// <summary>
        /// Gets a value indicating whether user controls should be skipped.
        /// <para>Получает признак пропуска пользовательских контролов.</para>
        /// </summary>
        public bool SkipUserControls { get; init; } = true;

        #endregion Variable
    }
}
