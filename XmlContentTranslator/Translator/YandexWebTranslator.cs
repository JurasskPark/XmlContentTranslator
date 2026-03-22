using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Yandex web translator based on the current web endpoint contract.
    /// <para>Yandex web переводчик на основе текущего контракта веб-точки доступа.</para>
    /// </summary>
    public class YandexWebTranslator : ITranslator
    {
        #region Variable

        private const string ApiUrl = "https://translate.yandex.net/api/v1/tr.json";
        private const string DefaultUserAgent = "ru.yandex.translate/3.20.2024";

        private static readonly HttpClient client;
        private static Guid ucid = Guid.NewGuid();
        private static DateTime ucidExpiresUtc = DateTime.MinValue;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes static resources.
        /// <para>Инициализирует статические ресурсы.</para>
        /// </summary>
        static YandexWebTranslator()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(20)
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd(DefaultUserAgent);
        }

        /// <summary>
        /// Gets supported translation pairs.
        /// <para>Получает поддерживаемые языковые пары.</para>
        /// </summary>
        public List<TranslationPair> GetTranslationPairs()
        {
            return new GoogleTranslator1().GetTranslationPairs();
        }

        /// <summary>
        /// Gets the translator name.
        /// <para>Получает имя переводчика.</para>
        /// </summary>
        public string GetName()
        {
            return "Yandex web";
        }

        /// <summary>
        /// Gets the translator URL.
        /// <para>Получает URL переводчика.</para>
        /// </summary>
        public string GetUrl()
        {
            return "https://translate.yandex.ru/";
        }

        /// <summary>
        /// Translates paragraphs using the Yandex endpoint.
        /// <para>Переводит абзацы через точку доступа Yandex.</para>
        /// </summary>
        public List<string> Translate(string sourceLanguage, string targetLanguage, List<string> paragraphs, StringBuilder log)
        {
            List<string> results = new List<string>(paragraphs.Count);
            string query = $"?ucid={GetOrUpdateUcid():N}&srv=android&format=text";

            foreach (string paragraph in paragraphs)
            {
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    { "text", paragraph },
                    { "lang", $"{PatchLanguageCode(sourceLanguage)}-{PatchLanguageCode(targetLanguage)}" }
                };

                using FormUrlEncodedContent content = new FormUrlEncodedContent(data);
                using HttpResponseMessage response = client.PostAsync($"{ApiUrl}/translate{query}", content).GetAwaiter().GetResult();
                string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                using JsonDocument doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("code", out JsonElement codeElement) &&
                    codeElement.TryGetInt32(out int statusCode) &&
                    statusCode != (int)HttpStatusCode.OK)
                {
                    string message = doc.RootElement.TryGetProperty("message", out JsonElement messageElement)
                        ? messageElement.GetString() ?? "Unexpected Yandex error."
                        : "Unexpected Yandex error.";
                    throw new HttpRequestException(message);
                }

                if (!doc.RootElement.TryGetProperty("text", out JsonElement textArray) ||
                    textArray.ValueKind != JsonValueKind.Array)
                {
                    throw new InvalidOperationException("Unexpected Yandex response format.");
                }

                string translated = string.Join(
                    Environment.NewLine,
                    textArray.EnumerateArray().Select(x => x.GetString() ?? string.Empty));
                results.Add(translated);
            }

            return results;
        }

        /// <summary>
        /// Gets or refreshes the UCID required by Yandex.
        /// <para>Получает или обновляет UCID, требуемый Yandex.</para>
        /// </summary>
        private static Guid GetOrUpdateUcid()
        {
            if (DateTime.UtcNow >= ucidExpiresUtc)
            {
                ucid = Guid.NewGuid();
                ucidExpiresUtc = DateTime.UtcNow.AddMinutes(6);
            }

            return ucid;
        }

        /// <summary>
        /// Applies Yandex-specific language code mapping.
        /// <para>Применяет Yandex-специфичное сопоставление кодов языков.</para>
        /// </summary>
        private static string PatchLanguageCode(string languageCode)
        {
            return languageCode switch
            {
                "pt-PT" => "pt",
                "pt" => "pt-BR",
                "zh-CN" => "zh",
                _ => languageCode
            };
        }

        #endregion Basic
    }
}
