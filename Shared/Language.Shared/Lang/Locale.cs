using System.Globalization;
using System.Xml;

namespace Lang
{
    /// <summary>
    /// Provides information about a culture of the software package.
    /// <para>Предоставляет информацию о культуре программного комплекса.</para>
    /// </summary>
    public static class Locale
    {
        #region Variable

        /// <summary>
        /// The name of the English culture.
        /// <para>Имя английской культуры.</para>
        /// </summary>
        private const string EnglishCultureName = "en-GB";

        /// <summary>
        /// The name of the Russian culture.
        /// <para>Имя русской культуры.</para>
        /// </summary>
        private const string RussianCultureName = "ru-RU";

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets a value indicating whether Russian locale is selected.
        /// <para>Получает признак выбора русской локали.</para>
        /// </summary>
        public static bool IsRussian { get; private set; }

        /// <summary>
        /// Gets the default culture.
        /// <para>Получает культуру по умолчанию.</para>
        /// </summary>
        public static CultureInfo DefaultCulture { get; }

        /// <summary>
        /// Gets the culture of the software package.
        /// <para>Получает культуру программного комплекса.</para>
        /// </summary>
        public static CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the loaded dictionaries.
        /// <para>Получает загруженные словари.</para>
        /// </summary>
        public static Dictionary<string, LocaleDict> Dictionaries { get; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes the class.
        /// <para>Инициализирует класс.</para>
        /// </summary>
        static Locale()
        {
            IsRussian = CultureIsRussian(CultureInfo.CurrentCulture);
            DefaultCulture = Culture = CultureInfo.GetCultureInfo(IsRussian ? RussianCultureName : EnglishCultureName);
            Dictionaries = new Dictionary<string, LocaleDict>();
        }

        /// <summary>
        /// Checks that the specified culture is Russian.
        /// <para>Проверяет, является ли указанная культура русской.</para>
        /// </summary>
        private static bool CultureIsRussian(CultureInfo cultureInfo)
        {
            return cultureInfo != null &&
                   (cultureInfo.Name.Equals("ru", StringComparison.OrdinalIgnoreCase) ||
                    cultureInfo.Name.StartsWith("ru-", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets the dictionary file name depending on the selected culture.
        /// <para>Получает имя файла словаря в зависимости от выбранной культуры.</para>
        /// </summary>
        private static string GetDictFileName(string directory, string fileNamePrefix, string cultureName)
        {
            return Path.Combine(directory, fileNamePrefix + "." + cultureName + ".xml");
        }

        /// <summary>
        /// Sets the culture.
        /// <para>Устанавливает культуру.</para>
        /// </summary>
        public static void SetCulture(string cultureName)
        {
            try
            {
                Culture = string.IsNullOrEmpty(cultureName)
                    ? DefaultCulture
                    : CultureInfo.GetCultureInfo(cultureName);
            }
            catch
            {
                Culture = DefaultCulture;
            }
            finally
            {
                IsRussian = CultureIsRussian(Culture);
            }
        }

        /// <summary>
        /// Sets the culture to the English culture.
        /// <para>Устанавливает английскую культуру.</para>
        /// </summary>
        public static void SetCultureToEnglish()
        {
            Culture = CultureInfo.GetCultureInfo(EnglishCultureName);
            IsRussian = false;
        }

        /// <summary>
        /// Sets the culture to the default.
        /// <para>Устанавливает культуру по умолчанию.</para>
        /// </summary>
        public static void SetCultureToDefault()
        {
            Culture = DefaultCulture;
            IsRussian = CultureIsRussian(Culture);
        }

        /// <summary>
        /// Loads dictionaries from the specified file.
        /// <para>Загружает словари из указанного файла.</para>
        /// </summary>
        public static bool LoadDictionaries(string fileName, out string errMsg)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                XmlElement? rootElement = xmlDoc.DocumentElement;
                if (rootElement == null)
                {
                    throw new InvalidOperationException("Dictionary XML does not contain a root element.");
                }

                XmlNodeList? dictNodes = rootElement.SelectNodes("Dictionary");
                if (dictNodes == null)
                {
                    errMsg = string.Empty;
                    return true;
                }

                foreach (XmlElement dictElem in dictNodes)
                {
                    string dictKey = dictElem.GetAttribute("key");
                    if (!Dictionaries.TryGetValue(dictKey, out LocaleDict? dict))
                    {
                        dict = new LocaleDict(dictKey);
                        Dictionaries.Add(dictKey, dict);
                    }

                    XmlNodeList? phraseNodes = dictElem.SelectNodes("Phrase");
                    if (phraseNodes == null)
                    {
                        continue;
                    }

                    foreach (XmlElement phraseElem in phraseNodes)
                    {
                        string phraseKey = phraseElem.GetAttribute("key");
                        dict.Phrases[phraseKey] = phraseElem.InnerText;
                    }
                }

                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = string.Format(
                    IsRussian ?
                        "Ошибка при загрузке словарей из файла {0}: {1}" :
                        "Error loading dictionaries from file {0}: {1}",
                    fileName,
                    ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Loads dictionaries of the selected culture.
        /// <para>Загружает словари выбранной культуры.</para>
        /// </summary>
        public static bool LoadDictionaries(string directory, string fileNamePrefix, out string errMsg)
        {
            string fileName = GetDictFileName(directory, fileNamePrefix, Culture.Name);
            string fallbackFileName = GetDictFileName(directory, fileNamePrefix, DefaultCulture.Name);

            if (File.Exists(fileName))
            {
                return LoadDictionaries(fileName, out errMsg);
            }

            if (File.Exists(fallbackFileName))
            {
                if (LoadDictionaries(fallbackFileName, out errMsg))
                {
                    errMsg = string.Format(
                        IsRussian ?
                            "Файл словарей не найден и заменён файлом по умолчанию: {0}" :
                            "Dictionary file not found and replaced with default file: {0}",
                        fileName);
                }

                return false;
            }

            errMsg = string.Format(
                IsRussian ?
                    "Не найден файл словарей: {0}" :
                    "Dictionary file not found: {0}",
                fileName);
            return false;
        }

        /// <summary>
        /// Gets the dictionary by the specified key.
        /// <para>Получает словарь по указанному ключу.</para>
        /// </summary>
        public static LocaleDict GetDictionary(string key)
        {
            return Dictionaries.TryGetValue(key, out LocaleDict? dict) ? dict : new LocaleDict(key);
        }

        #endregion Basic
    }
}
