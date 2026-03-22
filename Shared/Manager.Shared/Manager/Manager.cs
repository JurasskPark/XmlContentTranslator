using ProjectSettings;

namespace ManagerAssistant
{
    /// <summary>
    /// Represents a manager.
    /// <para>Представляет менеджер.</para>
    /// </summary>
    public static class Manager
    {
        #region Property

        /// <summary>
        /// Gets or sets a value indicating whether the application is running as a DLL.
        /// </summary>
        public static bool IsDll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log writing is enabled.
        /// </summary>
        public static bool LogWrite { get; set; }

        /// <summary>
        /// Gets or sets the path to the log directory.
        /// </summary>
        public static string LogPath { get; set; } = ApplicationPath.LogDir;

        /// <summary>
        /// Gets or sets the number of log retention days.
        /// </summary>
        public static int LogDays { get; set; }

        /// <summary>
        /// Gets or sets the path to the project file.
        /// </summary>
        public static string PathProject { get; set; } = string.Empty;

        #endregion Property
    }
}
