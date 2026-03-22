using System;
using System.Collections.Generic;
using System.Text;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Text formatting helper.
    /// <para>Вспомогательный класс форматирования текста.</para>
    /// </summary>
    public class Formatting
    {
        #region Variable

        private bool Italic { get; set; }
        private bool ItalicTwoLines { get; set; }
        private string StartTags { get; set; } = string.Empty;
        private bool AutoBreak { get; set; }
        private bool SquareBrackets { get; set; }
        private bool SquareBracketsUppercase { get; set; }
        private int NumberOfLines { get; set; }

        #endregion Variable

        #region Basic

        /// <summary>
        /// Extracts formatting markers and returns trimmed text.
        /// <para>Извлекает маркеры форматирования и возвращает обрезанный текст.</para>
        /// </summary>
        public string SetTagsAndReturnTrimmed(string text, string source)
        {
            text = text.Trim();

            if (text.StartsWith("{\\", StringComparison.Ordinal))
            {
                int endIndex = text.IndexOf('}');
                if (endIndex > 0)
                {
                    StartTags = text.Substring(0, endIndex + 1);
                    text = text.Remove(0, endIndex + 1).Trim();
                }
            }

            if (text.StartsWith("<i>", StringComparison.Ordinal) &&
                text.EndsWith("</i>", StringComparison.Ordinal) &&
                text.Contains("</i>" + Environment.NewLine + "<i>", StringComparison.Ordinal) &&
                Utilities.GetNumberOfLines(text) == 2 &&
                Utilities.CountTagInText(text, "<i>") == 1)
            {
                ItalicTwoLines = true;
                text = HtmlUtil.RemoveOpenCloseTags(text, HtmlUtil.TagItalic);
            }
            else if (text.StartsWith("<i>", StringComparison.Ordinal) &&
                     text.EndsWith("</i>", StringComparison.Ordinal) &&
                     Utilities.CountTagInText(text, "<i>") == 1)
            {
                Italic = true;
                text = text.Substring(3, text.Length - 7);
            }

            List<string> allowedLanguages = new List<string> { "en", "da", "nl", "de", "sv", "nb", "fr", "it" };
            if (allowedLanguages.Contains(source))
            {
                string[] lines = HtmlUtil.RemoveHtmlTags(text).SplitToLines();
                if (lines.Length == 2 &&
                    !string.IsNullOrEmpty(lines[0]) &&
                    !string.IsNullOrEmpty(lines[1]) &&
                    char.IsLetterOrDigit(lines[0][lines[0].Length - 1]) &&
                    char.IsLower(lines[1][0]))
                {
                    text = string.Join(" ", text.SplitToLines()).Replace("  ", " ", StringComparison.Ordinal);
                    AutoBreak = true;
                }
            }

            if (text.StartsWith("[", StringComparison.Ordinal) &&
                text.EndsWith("]", StringComparison.Ordinal) &&
                Utilities.GetNumberOfLines(text) == 1 &&
                Utilities.CountTagInText(text, "[") == 1 &&
                Utilities.CountTagInText(text, "]") == 1)
            {
                if (text == text.ToUpperInvariant())
                {
                    SquareBracketsUppercase = true;
                }
                else
                {
                    SquareBrackets = true;
                }

                text = text.Replace("[", string.Empty, StringComparison.Ordinal)
                    .Replace("]", string.Empty, StringComparison.Ordinal);
            }

            return text.Trim();
        }

        /// <summary>
        /// Reapplies previously extracted formatting.
        /// <para>Повторно применяет ранее извлеченное форматирование.</para>
        /// </summary>
        public string ReAddFormatting(string text)
        {
            if (AutoBreak)
            {
                text = Utilities.AutoBreakLine(text);
            }

            if (SquareBracketsUppercase)
            {
                text = "[" + text.ToUpperInvariant().Trim() + "]";
            }
            else if (SquareBrackets)
            {
                text = "[" + text.Trim() + "]";
            }

            if (ItalicTwoLines)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string line in text.SplitToLines())
                {
                    sb.AppendLine("<i>" + line + "</i>");
                }

                text = sb.ToString().Trim();
            }
            else if (Italic)
            {
                text = "<i>" + text + "</i>";
            }

            text = StartTags + text;
            return text;
        }

        /// <summary>
        /// Removes line breaks from text.
        /// <para>Удаляет разрывы строк из текста.</para>
        /// </summary>
        public string Unbreak(string text, string source)
        {
            NumberOfLines = source.SplitToLines().Length;
            return text.Replace(Environment.NewLine, " ", StringComparison.Ordinal)
                .Replace("  ", " ", StringComparison.Ordinal);
        }

        /// <summary>
        /// Restores line breaks based on the saved line count.
        /// <para>Восстанавливает разрывы строк по сохраненному количеству строк.</para>
        /// </summary>
        public string Rebreak(string text)
        {
            if (NumberOfLines == 1)
            {
                return text;
            }

            return Utilities.AutoBreakLine(text);
        }

        #endregion Basic
    }
}
