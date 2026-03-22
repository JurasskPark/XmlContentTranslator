using System;
using System.Text;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// JSON text helper.
    /// <para>Вспомогательный класс работы с JSON-текстом.</para>
    /// </summary>
    public class Json
    {
        #region Basic

        /// <summary>
        /// Encodes text for JSON transport.
        /// <para>Кодирует текст для передачи в JSON.</para>
        /// </summary>
        public static string EncodeJsonText(string text)
        {
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '"':
                        sb.Append("\\\"");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString().Replace(Environment.NewLine, "<br />", StringComparison.Ordinal);
        }

        /// <summary>
        /// Decodes text received from JSON.
        /// <para>Декодирует текст, полученный из JSON.</para>
        /// </summary>
        public static string DecodeJsonText(string text)
        {
            text = text.Replace("<br />", Environment.NewLine, StringComparison.Ordinal);
            text = text.Replace("<br>", Environment.NewLine, StringComparison.Ordinal);
            text = text.Replace("<br/>", Environment.NewLine, StringComparison.Ordinal);
            text = text.Replace("\\n", Environment.NewLine, StringComparison.Ordinal);

            bool keepNext = false;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                if (c == '\\' && !keepNext)
                {
                    keepNext = true;
                }
                else
                {
                    sb.Append(c);
                    keepNext = false;
                }
            }

            return sb.ToString();
        }

        #endregion Basic
    }
}
