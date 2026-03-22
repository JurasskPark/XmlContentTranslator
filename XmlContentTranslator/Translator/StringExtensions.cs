using System.Globalization;
using System.Text;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// String extension helpers.
    /// <para>Вспомогательные строковые extension-методы.</para>
    /// </summary>
    public static class StringExtensions
    {
        #region Basic

        /// <summary>
        /// Checks whether a line starts with an HTML tag.
        /// <para>Проверяет, начинается ли строка с HTML-тега.</para>
        /// </summary>
        public static bool LineStartsWithHtmlTag(this string text, bool threeLengthTag, bool includeFont = false)
        {
            if (text == null || (!threeLengthTag && !includeFont))
            {
                return false;
            }

            return StartsWithHtmlTag(text, threeLengthTag, includeFont);
        }

        /// <summary>
        /// Checks whether a line ends with an HTML tag.
        /// <para>Проверяет, заканчивается ли строка HTML-тегом.</para>
        /// </summary>
        public static bool LineEndsWithHtmlTag(this string text, bool threeLengthTag, bool includeFont = false)
        {
            if (text == null)
            {
                return false;
            }

            int len = text.Length;
            if (len < 6 || text[len - 1] != '>')
            {
                return false;
            }

            if (threeLengthTag && len > 3 && text[len - 4] == '<' && text[len - 3] == '/')
            {
                return true;
            }

            if (includeFont && len > 8 && text[len - 7] == '<' && text[len - 6] == '/')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the line after a line break starts with an HTML tag.
        /// <para>Проверяет, начинается ли строка после перевода строки с HTML-тега.</para>
        /// </summary>
        public static bool LineBreakStartsWithHtmlTag(this string text, bool threeLengthTag, bool includeFont = false)
        {
            if (text == null || (!threeLengthTag && !includeFont))
            {
                return false;
            }

            int newLineIdx = text.IndexOf(Environment.NewLine, StringComparison.Ordinal);
            if (newLineIdx < 0 || text.Length < newLineIdx + 5)
            {
                return false;
            }

            text = text.Substring(newLineIdx + 2);
            return StartsWithHtmlTag(text, threeLengthTag, includeFont);
        }

        /// <summary>
        /// Checks whether text starts with a supported HTML tag.
        /// <para>Проверяет, начинается ли текст с поддерживаемого HTML-тега.</para>
        /// </summary>
        private static bool StartsWithHtmlTag(string text, bool threeLengthTag, bool includeFont)
        {
            if (threeLengthTag && text.Length >= 3 && text[0] == '<' && text[2] == '>' &&
                (text[1] == 'i' || text[1] == 'I' || text[1] == 'u' || text[1] == 'U' || text[1] == 'b' || text[1] == 'B'))
            {
                return true;
            }

            if (includeFont && text.Length > 5 && text.StartsWith("<font", StringComparison.OrdinalIgnoreCase))
            {
                return text.IndexOf('>', 5) >= 5;
            }

            return false;
        }

        /// <summary>
        /// Checks whether a string starts with a specific character.
        /// <para>Проверяет, начинается ли строка с указанного символа.</para>
        /// </summary>
        public static bool StartsWith(this string s, char c)
        {
            return s.Length > 0 && s[0] == c;
        }

        /// <summary>
        /// Checks whether a StringBuilder starts with a specific character.
        /// <para>Проверяет, начинается ли StringBuilder с указанного символа.</para>
        /// </summary>
        public static bool StartsWith(this StringBuilder sb, char c)
        {
            return sb.Length > 0 && sb[0] == c;
        }

        /// <summary>
        /// Checks whether a string ends with a specific character.
        /// <para>Проверяет, заканчивается ли строка указанным символом.</para>
        /// </summary>
        public static bool EndsWith(this string s, char c)
        {
            return s.Length > 0 && s[s.Length - 1] == c;
        }

        /// <summary>
        /// Checks whether a StringBuilder ends with a specific character.
        /// <para>Проверяет, заканчивается ли StringBuilder указанным символом.</para>
        /// </summary>
        public static bool EndsWith(this StringBuilder sb, char c)
        {
            return sb.Length > 0 && sb[sb.Length - 1] == c;
        }

        /// <summary>
        /// Checks whether a string contains a character.
        /// <para>Проверяет, содержит ли строка символ.</para>
        /// </summary>
        public static bool Contains(this string source, char value)
        {
            return source.IndexOf(value) >= 0;
        }

        /// <summary>
        /// Checks whether a string contains any of the specified characters.
        /// <para>Проверяет, содержит ли строка любой из указанных символов.</para>
        /// </summary>
        public static bool Contains(this string source, char[] value)
        {
            return source.IndexOfAny(value) >= 0;
        }

        /// <summary>
        /// Checks whether a string contains another string using a comparison mode.
        /// <para>Проверяет, содержит ли строка подстроку с указанным режимом сравнения.</para>
        /// </summary>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Splits text to lines.
        /// <para>Разбивает текст на строки.</para>
        /// </summary>
        public static string[] SplitToLines(this string source)
        {
            return source.Replace("\r\r\n", "\n", StringComparison.Ordinal)
                .Replace("\r\n", "\n", StringComparison.Ordinal)
                .Replace('\r', '\n')
                .Replace('\u2028', '\n')
                .Split('\n');
        }

        /// <summary>
        /// Counts words in text without HTML tags.
        /// <para>Подсчитывает слова в тексте без HTML-тегов.</para>
        /// </summary>
        public static int CountWords(this string source)
        {
            return HtmlUtil.RemoveHtmlTags(source, true)
                .Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;
        }

        /// <summary>
        /// Finds the index of a pattern using a fast scan.
        /// <para>Находит индекс шаблона быстрым проходом.</para>
        /// </summary>
        public static int FastIndexOf(this string source, string pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException();
            }

            if (pattern.Length == 0)
            {
                return 0;
            }

            if (pattern.Length == 1)
            {
                return source.IndexOf(pattern[0]);
            }

            int limit = source.Length - pattern.Length + 1;
            if (limit < 1)
            {
                return -1;
            }

            char c0 = pattern[0];
            char c1 = pattern[1];
            int first = source.IndexOf(c0, 0, limit);
            while (first != -1)
            {
                if (source[first + 1] != c1)
                {
                    first = source.IndexOf(c0, ++first, limit - first);
                    continue;
                }

                bool found = true;
                for (int j = 2; j < pattern.Length; j++)
                {
                    if (source[first + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return first;
                }

                first = source.IndexOf(c0, ++first, limit - first);
            }

            return -1;
        }

        /// <summary>
        /// Finds the first occurrence of any specified word.
        /// <para>Находит первое вхождение любого из указанных слов.</para>
        /// </summary>
        public static int IndexOfAny(this string s, string[] words, StringComparison comparisonType)
        {
            if (words == null || string.IsNullOrEmpty(s))
            {
                return -1;
            }

            for (int i = 0; i < words.Length; i++)
            {
                int idx = s.IndexOf(words[i], comparisonType);
                if (idx >= 0)
                {
                    return idx;
                }
            }

            return -1;
        }

        /// <summary>
        /// Removes duplicate spaces and spaces near line breaks.
        /// <para>Удаляет лишние пробелы и пробелы рядом с переводами строк.</para>
        /// </summary>
        public static string FixExtraSpaces(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            int len = s.Length;
            int k = -1;
            for (int i = len - 1; i >= 0; i--)
            {
                char ch = s[i];
                if (k < 2)
                {
                    if (ch == 0x20)
                    {
                        k = i + 1;
                    }
                }
                else if (ch != 0x20)
                {
                    if (k - (i + 1) > 1)
                    {
                        s = s.Remove(i + 1, k - (i + 2));
                    }

                    if ((ch == '\n' || ch == '\r') && i + 1 < s.Length && s[i + 1] == 0x20)
                    {
                        s = s.Remove(i + 1, 1);
                    }

                    k = -1;
                }

                if (ch == 0x20 && i + 1 < s.Length && (s[i + 1] == '\n' || s[i + 1] == '\r'))
                {
                    s = s.Remove(i, 1);
                }
            }

            return s;
        }

        /// <summary>
        /// Determines whether a string contains letters.
        /// <para>Определяет, содержит ли строка буквы.</para>
        /// </summary>
        public static bool ContainsLetter(this string s)
        {
            if (s != null)
            {
                foreach (int index in StringInfo.ParseCombiningCharacters(s))
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s, index);
                    if (uc == UnicodeCategory.LowercaseLetter ||
                        uc == UnicodeCategory.UppercaseLetter ||
                        uc == UnicodeCategory.TitlecaseLetter ||
                        uc == UnicodeCategory.ModifierLetter ||
                        uc == UnicodeCategory.OtherLetter)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes control characters.
        /// <para>Удаляет управляющие символы.</para>
        /// </summary>
        public static string RemoveControlCharacters(this string s)
        {
            int max = s.Length;
            char[] newStr = new char[max];
            int newIdx = 0;
            for (int index = 0; index < max; index++)
            {
                char ch = s[index];
                if (!char.IsControl(ch))
                {
                    newStr[newIdx++] = ch;
                }
            }

            return new string(newStr, 0, newIdx);
        }

        /// <summary>
        /// Removes control characters except whitespace control characters.
        /// <para>Удаляет управляющие символы, кроме пробельных управляющих символов.</para>
        /// </summary>
        public static string RemoveControlCharactersButWhiteSpace(this string s)
        {
            int max = s.Length;
            char[] newStr = new char[max];
            int newIdx = 0;
            for (int index = 0; index < max; index++)
            {
                char ch = s[index];
                if (!char.IsControl(ch) || ch == '\u000d' || ch == '\u000a' || ch == '\u0009')
                {
                    newStr[newIdx++] = ch;
                }
            }

            return new string(newStr, 0, newIdx);
        }

        /// <summary>
        /// Capitalizes the first text element.
        /// <para>Делает заглавным первый текстовый элемент.</para>
        /// </summary>
        public static string CapitalizeFirstLetter(this string s, CultureInfo? ci = null)
        {
            StringInfo si = new StringInfo(s);
            ci ??= CultureInfo.CurrentCulture;
            if (si.LengthInTextElements > 0)
            {
                s = si.SubstringByTextElements(0, 1).ToUpper(ci);
            }

            if (si.LengthInTextElements > 1)
            {
                s += si.SubstringByTextElements(1);
            }

            return s;
        }

        /// <summary>
        /// Converts text to full RTF document.
        /// <para>Преобразует текст в полный RTF-документ.</para>
        /// </summary>
        public static string ToRtf(this string value)
        {
            return @"{\rtf1\ansi\ansicpg1252\deff0{\fonttbl\f0\fswiss Helvetica;}\f0\pard " +
                   value.ToRtfPart() +
                   @"\par" + Environment.NewLine + "}";
        }

        /// <summary>
        /// Converts text to an RTF fragment.
        /// <para>Преобразует текст в фрагмент RTF.</para>
        /// </summary>
        public static string ToRtfPart(this string value)
        {
            StringBuilder backslashed = new StringBuilder(value);
            backslashed.Replace(@"\", @"\\");
            backslashed.Replace(@"{", @"\{");
            backslashed.Replace(@"}", @"\}");
            backslashed.Replace(Environment.NewLine, @"\par" + Environment.NewLine);

            StringBuilder sb = new StringBuilder();
            foreach (char character in backslashed.ToString())
            {
                if (character <= 0x7f)
                {
                    sb.Append(character);
                }
                else
                {
                    sb.Append("\\u" + Convert.ToUInt32(character) + "?");
                }
            }

            return sb.ToString();
        }

        #endregion Basic
    }
}
