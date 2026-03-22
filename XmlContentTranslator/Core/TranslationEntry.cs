using System.Xml;

namespace XmlContentTranslator.Core
{
    /// <summary>
    /// Translation entry within the workspace.
    /// <para>Элемент перевода внутри рабочей области.</para>
    /// </summary>
    public sealed class TranslationEntry
    {
        #region Variable

        /// <summary>
        /// Gets or sets the unique node path.
        /// <para>Получает или задает уникальный путь узла.</para>
        /// </summary>
        public required string Path { get; init; }

        /// <summary>
        /// Gets or sets the display key.
        /// <para>Получает или задает отображаемый ключ.</para>
        /// </summary>
        public required string DisplayKey { get; init; }

        /// <summary>
        /// Gets or sets the source text.
        /// <para>Получает или задает исходный текст.</para>
        /// </summary>
        public required string SourceText { get; set; }

        /// <summary>
        /// Gets or sets the translated text.
        /// <para>Получает или задает переведенный текст.</para>
        /// </summary>
        public string TargetText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the source XML node.
        /// <para>Получает или задает исходный XML-узел.</para>
        /// </summary>
        public required XmlNode SourceNode { get; init; }

        /// <summary>
        /// Gets or sets the target XML node.
        /// <para>Получает или задает целевой XML-узел.</para>
        /// </summary>
        public XmlNode? TargetNode { get; set; }

        /// <summary>
        /// Gets or sets the parent node path.
        /// <para>Получает или задает путь родительского узла.</para>
        /// </summary>
        public required string ParentPath { get; init; }

        #endregion Variable
    }
}
