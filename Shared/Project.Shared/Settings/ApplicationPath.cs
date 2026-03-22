namespace ProjectSettings
{
    /// <summary>
    /// Application path helper.
    /// <para>Вспомогательный класс путей приложения.</para>
    /// </summary>
    public class ApplicationPath
    {
        #region Variable

        /// <summary>
        /// Get directory of executable file.
        /// <para>Получить директорию исполняемого файла.</para>
        /// </summary>
        public static string StartPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Get directory of language translations.
        /// <para>Получить директорию языковых переводов.</para>
        /// </summary>
        public static string LangDir = ResolveLangDir();

        /// <summary>
        /// Get log directory.
        /// <para>Получить директорию логов.</para>
        /// </summary>
        public static string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes the class.
        /// <para>Инициализирует класс.</para>
        /// </summary>
        static ApplicationPath()
        {
            EnsureDirectoryExists(LogDir);
        }

        /// <summary>
        /// Resolves the language file path.
        /// <para>Определяет путь к языковому файлу.</para>
        /// </summary>
        public static string ResolveLanguageFile(string code, string culture)
        {
            string fileName = $"{code}.{culture}.xml";
            string[] candidates =
            {
                Path.Combine(LangDir, fileName),
                Path.Combine(StartPath, "Lang", fileName),
                Path.Combine(Environment.CurrentDirectory, "Lang", fileName),
                Path.Combine(Environment.CurrentDirectory, "XmlContentTranslator", "Lang", fileName)
            };

            foreach (string candidate in candidates)
            {
                if (File.Exists(candidate))
                {
                    return candidate;
                }
            }

            return candidates[0];
        }

        /// <summary>
        /// Resolves the language directory.
        /// <para>Определяет директорию языков.</para>
        /// </summary>
        private static string ResolveLangDir()
        {
            string[] candidates =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lang"),
                Path.Combine(Environment.CurrentDirectory, "Lang"),
                Path.Combine(Environment.CurrentDirectory, "XmlContentTranslator", "Lang")
            };

            foreach (string candidate in candidates)
            {
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }
            }

            return candidates[0];
        }

        /// <summary>
        /// Ensures the specified directory exists.
        /// <para>Убеждается, что указанная директория существует.</para>
        /// </summary>
        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion Basic
    }
}
