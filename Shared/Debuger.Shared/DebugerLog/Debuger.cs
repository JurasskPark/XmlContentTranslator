using FileOperations;
using ManagerAssistant;
using ProjectSettings;

namespace DebugerLog
{
    /// <summary>
    /// Recording logs.
    /// <para>Запись логов.</para>
    /// </summary>
    public class Debuger
    {
        #region Variable

        public static bool isDll;
        public static bool logWrite;
        public static int logDays;
        public static string logPath = string.Empty;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Debuger()
        {
            isDll = Manager.IsDll;
            logWrite = Manager.LogWrite;
            logDays = Manager.LogDays;
            logPath = Manager.LogPath;
        }

        /// <summary>
        /// Records log message.
        /// </summary>
        public static void Log(string text)
        {
            try
            {
                if (Manager.IsDll)
                {
                    if (Manager.LogWrite)
                    {
                        DebugerReturn debugerReturn = new DebugerReturn();
                        debugerReturn.Log(text);
                    }
                }
                else if (Manager.LogWrite)
                {
                    Log(Manager.LogPath, text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Records log message to the specified folder.
        /// </summary>
        public static void Log(string folder, string text)
        {
            try
            {
                DebugerReturn debugerReturn = new DebugerReturn();
                debugerReturn.Log(text);

                text = @$"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff}] {text}";
                Directory.CreateDirectory(folder);

                using StreamWriter streamWriter = File.AppendText(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
                streamWriter.WriteLine(text);
            }
            catch
            {
            }

            try
            {
                Clear(folder, Manager.LogDays);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Records log message without timestamp.
        /// </summary>
        public static void LogNoDateTime(string text)
        {
            try
            {
                if (Manager.IsDll)
                {
                    if (Manager.LogWrite)
                    {
                        DebugerReturn debugerReturn = new DebugerReturn();
                        debugerReturn.Log(text);
                    }
                }
                else if (Manager.LogWrite)
                {
                    LogNoDateTime(Manager.LogPath, text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Records log message without timestamp to the specified folder.
        /// </summary>
        public static void LogNoDateTime(string folder, string text)
        {
            try
            {
                DebugerReturn debugerReturn = new DebugerReturn();
                debugerReturn.Log(text);

                Directory.CreateDirectory(folder);
                using StreamWriter streamWriter = File.AppendText(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
                streamWriter.WriteLine(text);
            }
            catch
            {
            }

            try
            {
                Clear(folder, Manager.LogDays);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Clears expired log files.
        /// </summary>
        public static void Clear(string path, int days)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            try
            {
                days = days > 0 ? -days : days;
                DateTime findDateTime = DateTime.Now.AddDays(days);
                List<FilesDatabase> files = FileWithChanges.SearchFiles(path);

                for (int i = 0; i < files.Count; i++)
                {
                    FilesDatabase file = files[i];
                    if (file != null && file.LastTimeChanged < findDateTime)
                    {
                        string fullPathToUpper = file.PathFile.ToUpper();
                        const string templateFileName = ".log";

                        if (fullPathToUpper.Contains(templateFileName.ToUpper()))
                        {
                            try
                            {
                                File.Delete(fullPathToUpper);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion Basic
    }
}
