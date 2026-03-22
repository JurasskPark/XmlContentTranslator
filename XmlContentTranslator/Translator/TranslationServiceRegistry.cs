using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Registry of built-in translation services.
    /// <para>Реестр встроенных сервисов перевода.</para>
    /// </summary>
    public static class TranslationServiceRegistry
    {
        #region Variable

        private static readonly Dictionary<string, Func<ITranslator>> Factories =
            new Dictionary<string, Func<ITranslator>>(StringComparer.OrdinalIgnoreCase)
            {
                ["GoogleWeb"] = static () => new GoogleTranslator1(),
                ["YandexWeb"] = static () => new YandexWebTranslator()
            };

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets built-in service names.
        /// <para>Получает имена встроенных сервисов.</para>
        /// </summary>
        public static IReadOnlyCollection<string> BuiltInNames => Factories.Keys.ToArray();

        #endregion Property

        #region Basic

        /// <summary>
        /// Creates a translator by name.
        /// <para>Создает переводчик по имени.</para>
        /// </summary>
        public static ITranslator Create(string? name)
        {
            if (!string.IsNullOrWhiteSpace(name) && Factories.TryGetValue(name.Trim(), out Func<ITranslator>? factory))
            {
                return factory();
            }

            return new GoogleTranslator1();
        }

        /// <summary>
        /// Normalizes service names from a text list.
        /// <para>Нормализует имена сервисов из текстового списка.</para>
        /// </summary>
        public static List<string> NormalizeServices(string? csv)
        {
            List<string> parsed = (csv ?? string.Empty)
                .Split(new[] { ',', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(x => Factories.ContainsKey(x))
                .ToList();

            if (parsed.Count == 0)
            {
                parsed.Add("GoogleWeb");
                parsed.Add("YandexWeb");
            }

            return parsed;
        }

        #endregion Basic
    }
}
