using System.Collections.Generic;
using System.Text;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Translation service contract.
    /// <para>Контракт сервиса перевода.</para>
    /// </summary>
    public interface ITranslator
    {
        #region Basic

        /// <summary>
        /// Gets supported translation pairs.
        /// <para>Получает поддерживаемые языковые пары.</para>
        /// </summary>
        List<TranslationPair> GetTranslationPairs();

        /// <summary>
        /// Gets the service name.
        /// <para>Получает имя сервиса.</para>
        /// </summary>
        string GetName();

        /// <summary>
        /// Gets the service URL.
        /// <para>Получает URL сервиса.</para>
        /// </summary>
        string GetUrl();

        /// <summary>
        /// Translates a collection of paragraphs.
        /// <para>Переводит коллекцию абзацев.</para>
        /// </summary>
        List<string> Translate(string sourceLanguage, string targetLanguage, List<string> paragraphs, StringBuilder log);

        #endregion Basic
    }
}
