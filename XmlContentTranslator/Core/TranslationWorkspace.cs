using System.Collections.Generic;
using System.Xml;

namespace XmlContentTranslator.Core
{
    /// <summary>
    /// Translation workspace state.
    /// <para>Состояние рабочей области перевода.</para>
    /// </summary>
    public sealed class TranslationWorkspace
    {
        #region Variable

        /// <summary>
        /// Gets or sets the source XML document.
        /// <para>Получает или задает исходный XML-документ.</para>
        /// </summary>
        public XmlDocument SourceDocument { get; init; } = new XmlDocument();

        /// <summary>
        /// Gets or sets the target XML document.
        /// <para>Получает или задает целевой XML-документ.</para>
        /// </summary>
        public XmlDocument? TargetDocument { get; set; }

        /// <summary>
        /// Gets or sets the source file path.
        /// <para>Получает или задает путь к исходному файлу.</para>
        /// </summary>
        public string? SourceFilePath { get; set; }

        /// <summary>
        /// Gets or sets the target file path.
        /// <para>Получает или задает путь к целевому файлу.</para>
        /// </summary>
        public string? TargetFilePath { get; set; }

        /// <summary>
        /// Gets or sets the source language name.
        /// <para>Получает или задает имя исходного языка.</para>
        /// </summary>
        public string SourceLanguageName { get; set; } = "Language 1";

        /// <summary>
        /// Gets or sets the target language name.
        /// <para>Получает или задает имя целевого языка.</para>
        /// </summary>
        public string TargetLanguageName { get; set; } = "Language 2";

        /// <summary>
        /// Gets or sets a value indicating whether the document is in SCADA dictionary format.
        /// <para>Получает или задает признак формата словаря SCADA.</para>
        /// </summary>
        public bool IsScadaFormat { get; set; }

        /// <summary>
        /// Gets translation entries.
        /// <para>Получает элементы перевода.</para>
        /// </summary>
        public List<TranslationEntry> Entries { get; } = new List<TranslationEntry>();

        /// <summary>
        /// Gets translation entries indexed by path.
        /// <para>Получает элементы перевода, индексированные по пути.</para>
        /// </summary>
        public Dictionary<string, TranslationEntry> EntriesByPath { get; } =
            new Dictionary<string, TranslationEntry>(StringComparer.OrdinalIgnoreCase);

        #endregion Variable
    }
}
