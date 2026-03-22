using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XmlContentTranslator.Translator
{
    /// <summary>
    /// Google translation provider based on the legacy public endpoint.
    /// <para>Провайдер перевода Google на основе устаревшей публичной точки входа.</para>
    /// </summary>
    public class GoogleTranslator1 : ITranslator
    {
        #region Variable

        private const char SplitChar = '\n';

        #endregion Variable

        #region Basic

        /// <summary>
        /// Gets supported translation pairs.
        /// <para>Получает поддерживаемые языковые пары.</para>
        /// </summary>
        public List<TranslationPair> GetTranslationPairs()
        {
            return new List<TranslationPair>
            {
                new TranslationPair("AFRIKAANS", "af"),
                new TranslationPair("ALBANIAN", "sq"),
                new TranslationPair("AMHARIC", "am"),
                new TranslationPair("ARABIC", "ar"),
                new TranslationPair("ARMENIAN", "hy"),
                new TranslationPair("AZERBAIJANI", "az"),
                new TranslationPair("BASQUE", "eu"),
                new TranslationPair("BELARUSIAN", "be"),
                new TranslationPair("BENGALI", "bn"),
                new TranslationPair("BOSNIAN", "bs"),
                new TranslationPair("BULGARIAN", "bg"),
                new TranslationPair("BURMESE", "my"),
                new TranslationPair("CATALAN", "ca"),
                new TranslationPair("CEBUANO", "ceb"),
                new TranslationPair("CHICHEWA", "ny"),
                new TranslationPair("CHINESE", "zh"),
                new TranslationPair("CHINESE_SIMPLIFIED", "zh-CN"),
                new TranslationPair("CHINESE_TRADITIONAL", "zh-TW"),
                new TranslationPair("CORSICAN", "co"),
                new TranslationPair("CROATIAN", "hr"),
                new TranslationPair("CZECH", "cs"),
                new TranslationPair("DANISH", "da"),
                new TranslationPair("DUTCH", "nl"),
                new TranslationPair("ENGLISH", "en"),
                new TranslationPair("ESPERANTO", "eo"),
                new TranslationPair("ESTONIAN", "et"),
                new TranslationPair("FILIPINO", "tl"),
                new TranslationPair("FINNISH", "fi"),
                new TranslationPair("FRENCH", "fr"),
                new TranslationPair("FRISIAN", "fy"),
                new TranslationPair("GALICIAN", "gl"),
                new TranslationPair("GEORGIAN", "ka"),
                new TranslationPair("GERMAN", "de"),
                new TranslationPair("GREEK", "el"),
                new TranslationPair("GUJARATI", "gu"),
                new TranslationPair("HAITIAN CREOLE", "ht"),
                new TranslationPair("HAUSA", "ha"),
                new TranslationPair("HAWAIIAN", "haw"),
                new TranslationPair("HEBREW", "iw"),
                new TranslationPair("HINDI", "hi"),
                new TranslationPair("HMOUNG", "hmn"),
                new TranslationPair("HUNGARIAN", "hu"),
                new TranslationPair("ICELANDIC", "is"),
                new TranslationPair("IGBO", "ig"),
                new TranslationPair("INDONESIAN", "id"),
                new TranslationPair("IRISH", "ga"),
                new TranslationPair("ITALIAN", "it"),
                new TranslationPair("JAPANESE", "ja"),
                new TranslationPair("JAVANESE", "jw"),
                new TranslationPair("KANNADA", "kn"),
                new TranslationPair("KAZAKH", "kk"),
                new TranslationPair("KHMER", "km"),
                new TranslationPair("KOREAN", "ko"),
                new TranslationPair("KURDISH", "ku"),
                new TranslationPair("KYRGYZ", "ky"),
                new TranslationPair("LAO", "lo"),
                new TranslationPair("LATIN", "la"),
                new TranslationPair("LATVIAN", "lv"),
                new TranslationPair("LITHUANIAN", "lt"),
                new TranslationPair("LUXEMBOURGISH", "lb"),
                new TranslationPair("MACEDONIAN", "mk"),
                new TranslationPair("MALAY", "ms"),
                new TranslationPair("MALAGASY", "mg"),
                new TranslationPair("MALAYALAM", "ml"),
                new TranslationPair("MALTESE", "mt"),
                new TranslationPair("MAORI", "mi"),
                new TranslationPair("MARATHI", "mr"),
                new TranslationPair("MONGOLIAN", "mn"),
                new TranslationPair("MYANMAR", "my"),
                new TranslationPair("NEPALI", "ne"),
                new TranslationPair("NORWEGIAN", "no"),
                new TranslationPair("PASHTO", "ps"),
                new TranslationPair("PERSIAN", "fa"),
                new TranslationPair("POLISH", "pl"),
                new TranslationPair("PORTUGUESE", "pt"),
                new TranslationPair("PUNJABI", "pa"),
                new TranslationPair("ROMANIAN", "ro"),
                new TranslationPair("ROMANJI", "romanji"),
                new TranslationPair("RUSSIAN", "ru"),
                new TranslationPair("SAMOAN", "sm"),
                new TranslationPair("SCOTS GAELIC", "gd"),
                new TranslationPair("SERBIAN", "sr"),
                new TranslationPair("SESOTHO", "st"),
                new TranslationPair("SHONA", "sn"),
                new TranslationPair("SINDHI", "sd"),
                new TranslationPair("SINHALA", "si"),
                new TranslationPair("SLOVAK", "sk"),
                new TranslationPair("SLOVENIAN", "sl"),
                new TranslationPair("SOMALI", "so"),
                new TranslationPair("SPANISH", "es"),
                new TranslationPair("SUNDANESE", "su"),
                new TranslationPair("SWAHILI", "sw"),
                new TranslationPair("SWEDISH", "sv"),
                new TranslationPair("TAJIK", "tg"),
                new TranslationPair("TAMIL", "ta"),
                new TranslationPair("TELUGU", "te"),
                new TranslationPair("THAI", "th"),
                new TranslationPair("TURKISH", "tr"),
                new TranslationPair("UKRAINIAN", "uk"),
                new TranslationPair("URDU", "ur"),
                new TranslationPair("UZBEK", "uz"),
                new TranslationPair("VIETNAMESE", "vi"),
                new TranslationPair("WELSH", "cy"),
                new TranslationPair("XHOSA", "xh"),
                new TranslationPair("YIDDISH", "yi"),
                new TranslationPair("YORUBA", "yo"),
                new TranslationPair("ZULU", "zu")
            };
        }

        /// <summary>
        /// Gets the translator name.
        /// <para>Получает имя переводчика.</para>
        /// </summary>
        public string GetName()
        {
            return "Google translate (old)";
        }

        /// <summary>
        /// Gets the translator URL.
        /// <para>Получает URL переводчика.</para>
        /// </summary>
        public string GetUrl()
        {
            return "https://translate.google.com/";
        }

        /// <summary>
        /// Translates paragraphs using the Google endpoint.
        /// <para>Переводит абзацы через точку доступа Google.</para>
        /// </summary>
        public List<string> Translate(string sourceLanguage, string targetLanguage, List<string> paragraphs, StringBuilder log)
        {
            StringBuilder input = new StringBuilder();
            Formatting[] formattings = new Formatting[paragraphs.Count];
            for (int index = 0; index < paragraphs.Count; index++)
            {
                string paragraph = paragraphs[index];
                Formatting formatting = new Formatting();
                formattings[index] = formatting;
                if (input.Length > 0)
                {
                    input.Append(" " + SplitChar + " ");
                }

                string text = formatting.SetTagsAndReturnTrimmed(
                    TranslationHelper.PreTranslate(paragraph.Replace(SplitChar.ToString(), string.Empty, StringComparison.Ordinal), sourceLanguage),
                    sourceLanguage);
                text = formatting.Unbreak(text, paragraph);
                input.Append(text);
            }

            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLanguage}&tl={targetLanguage}&dt=t&q={Uri.EscapeDataString(input.ToString())}";
            string result = HttpTranslationClient.GetString(url).Trim();

            StringBuilder sbAll = new StringBuilder();
            int count = 0;
            int i = 1;
            int level = result.StartsWith("[", StringComparison.Ordinal) ? 1 : 0;
            while (i < result.Length - 1)
            {
                StringBuilder sb = new StringBuilder();
                bool start = false;
                for (; i < result.Length - 1; i++)
                {
                    char c = result[i];
                    if (start)
                    {
                        if (c == '"' && result[i - 1] != '\\')
                        {
                            count++;
                            if (count % 2 == 1 && level > 2 && level < 5)
                            {
                                sbAll.Append(" " + sb);
                            }

                            i++;
                            break;
                        }

                        sb.Append(c);
                    }
                    else if (c == '"')
                    {
                        start = true;
                    }
                    else if (c == '[')
                    {
                        level++;
                    }
                    else if (c == ']')
                    {
                        level--;
                    }
                }
            }

            string res = sbAll.ToString().Trim();
            res = Regex.Unescape(res);
            List<string> lines = res.SplitToLines().ToList();
            List<string> resultList = new List<string>();
            for (int index = 0; index < lines.Count; index++)
            {
                string line = lines[index];
                string s = Json.DecodeJsonText(line);
                s = string.Join(Environment.NewLine, s.SplitToLines());
                s = TranslationHelper.PostTranslate(s, targetLanguage);
                s = s.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine, StringComparison.Ordinal);
                s = s.Replace(Environment.NewLine + " ", Environment.NewLine, StringComparison.Ordinal);
                s = s.Replace(Environment.NewLine + " ", Environment.NewLine, StringComparison.Ordinal);
                s = s.Replace(" " + Environment.NewLine, Environment.NewLine, StringComparison.Ordinal);
                s = s.Replace(" " + Environment.NewLine, Environment.NewLine, StringComparison.Ordinal).Trim();
                if (formattings.Length > index)
                {
                    s = formattings[index].ReAddFormatting(s);
                    s = formattings[index].Rebreak(s);
                }

                resultList.Add(s);
            }

            if (resultList.Count > paragraphs.Count)
            {
                List<string> trimmedList = resultList.Where(p => !string.IsNullOrEmpty(p)).ToList();
                if (trimmedList.Count == paragraphs.Count)
                {
                    return trimmedList;
                }
            }

            if (resultList.Count < paragraphs.Count)
            {
                List<string> splitList = SplitMergedLines(resultList, paragraphs);
                if (splitList.Count == paragraphs.Count)
                {
                    return splitList;
                }
            }

            return resultList;
        }

        /// <summary>
        /// Attempts to split merged translated lines.
        /// <para>Пытается разделить объединенные переведенные строки.</para>
        /// </summary>
        private static List<string> SplitMergedLines(List<string> input, List<string> paragraphs)
        {
            int hits = 0;
            List<string> results = new List<string>();
            for (int index = 0; index < input.Count; index++)
            {
                string line = input[index];
                string text = paragraphs[index];
                int badPoints = 0;
                if (text.StartsWith("[", StringComparison.Ordinal) && !line.StartsWith("[", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.StartsWith("-", StringComparison.Ordinal) && !line.StartsWith("-", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.Length > 0 && char.IsUpper(text[0]) && line.Length > 0 && !char.IsUpper(line[0]))
                {
                    badPoints++;
                }

                if (text.EndsWith(".", StringComparison.Ordinal) && !line.EndsWith(".", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.EndsWith("!", StringComparison.Ordinal) && !line.EndsWith("!", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.EndsWith("?", StringComparison.Ordinal) && !line.EndsWith("?", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.EndsWith(",", StringComparison.Ordinal) && !line.EndsWith(",", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                if (text.EndsWith(":", StringComparison.Ordinal) && !line.EndsWith(":", StringComparison.Ordinal))
                {
                    badPoints++;
                }

                bool added = false;
                if (badPoints > 0 && hits + input.Count < paragraphs.Count)
                {
                    double percent = line.Length * 100.0 / text.Length;
                    if (percent > 150)
                    {
                        string[] temp = Utilities.AutoBreakLine(line).SplitToLines();
                        if (temp.Length == 2)
                        {
                            hits++;
                            results.Add(temp[0]);
                            results.Add(temp[1]);
                            added = true;
                        }
                    }
                }

                if (!added)
                {
                    results.Add(line);
                }
            }

            if (results.Count == paragraphs.Count)
            {
                return results;
            }

            return input;
        }

        #endregion Basic
    }
}
