using ProjectSettings;
using System.Xml;
using Xml.Shared;

namespace DebugerLog
{
    /// <summary>
    /// Logging settings.
    /// <para>Настройки логирования.</para>
    /// </summary>
    public class DebugerSettings
    {
        #region Property

        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled.
        /// </summary>
        public bool LogWrite { get; set; }

        /// <summary>
        /// Gets or sets how many days to store logs.
        /// </summary>
        public int LogDays { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DebugerSettings()
        {
            LogPath = ApplicationPath.LogDir;
            LogWrite = true;
            LogDays = 7;
        }

        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            LogPath = xmlNode.GetChildAsString("LogPath");
            LogWrite = xmlNode.GetChildAsBool("LogWrite");
            LogDays = xmlNode.GetChildAsInt("LogDays");
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("LogPath", LogPath);
            xmlElem.AppendElem("LogWrite", LogWrite);
            xmlElem.AppendElem("LogDays", LogDays);
        }

        #endregion Basic
    }
}
