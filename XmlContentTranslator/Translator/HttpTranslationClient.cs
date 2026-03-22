using System.Net;
using System.Net.Http;
using System.Threading;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Shared HTTP client for translation services.
    /// <para>Общий HTTP-клиент для сервисов перевода.</para>
    /// </summary>
    internal static class HttpTranslationClient
    {
        #region Variable

        private static readonly HttpClient Client;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes static resources.
        /// <para>Инициализирует статические ресурсы.</para>
        /// </summary>
        static HttpTranslationClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            Client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(20)
            };
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("XmlContentTranslator/1.0");
        }

        /// <summary>
        /// Gets string content from a URL with retries.
        /// <para>Получает строковое содержимое по URL с повторными попытками.</para>
        /// </summary>
        public static string GetString(string url, int maxAttempts = 3)
        {
            Exception? lastError = null;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    return Client.GetStringAsync(url).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    lastError = ex;
                    if (attempt == maxAttempts)
                    {
                        break;
                    }

                    Thread.Sleep(300 * attempt);
                }
            }

            throw new HttpRequestException($"Failed to call translation endpoint after {maxAttempts} attempts.", lastError);
        }

        #endregion Basic
    }
}
