using XmlContentTranslator.Forms;

namespace XmlContentTranslator
{
    /// <summary>
    /// Application entry point.
    /// <para>Точка входа приложения.</para>
    /// </summary>
    internal static class Program
    {
        #region Basic

        /// <summary>
        /// Main application entry point.
        /// <para>Основная точка входа приложения.</para>
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmStart());
        }

        #endregion Basic
    }
}
