using DebugerLog;
using Lang;
using System.Xml;
using Xml.Shared;

namespace ProjectSettings
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию проекта.</para>
    /// </summary>
    [Serializable]
    public abstract class ProjectConfig
    {
        #region Variable

        public string FromLanguage { get; set; } = string.Empty;
        public string ToLanguage { get; set; } = string.Empty;
        public string TranslationService { get; set; } = "GoogleWeb";
        public string TranslationServices { get; set; } = "GoogleWeb,YandexWeb";

        /// <summary>
        /// Gets the debugger settings.
        /// <para>Получает настройки отладчика.</para>
        /// </summary>
        public DebugerSettings DebugerSettings { get; set; } = new DebugerSettings();

        /// <summary>
        /// Gets or sets the language.
        /// <para>Получает или задает язык.</para>
        /// </summary>
        public bool LanguageIsRussian { get; set; }

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public ProjectConfig()
        {
            SetToDefault();
        }

        /// <summary>
        /// Sets the default values.
        /// <para>Устанавливает значения по умолчанию.</para>
        /// </summary>
#pragma warning disable CS0114
        private void SetToDefault()
#pragma warning restore CS0114
        {
            FromLanguage = string.Empty;
            ToLanguage = string.Empty;
            TranslationService = "GoogleWeb";
            TranslationServices = "GoogleWeb,YandexWeb";
            DebugerSettings = new DebugerSettings();
            LanguageIsRussian = false;
        }

        /// <summary>
        /// Loads the configuration from the specified file.
        /// <para>Загружает конфигурацию из указанного файла.</para>
        /// </summary>
#pragma warning disable CS0108
        public bool Load(string fileName, out string errMsg)
#pragma warning restore CS0108
        {
            SetToDefault();

            try
            {
                if (!File.Exists(fileName))
                {
                    try
                    {
                        Save(fileName, out errMsg);
                    }
                    catch
                    {
                        throw new FileNotFoundException(string.Format(CommonPhrases.NamedFileNotFound, fileName));
                    }
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlElement? rootElem = xmlDoc.DocumentElement;
                if (rootElem == null)
                {
                    throw new InvalidOperationException("Project configuration XML does not contain a root element.");
                }

                try { FromLanguage = rootElem.GetChildAsString("FromLanguage"); } catch { FromLanguage = string.Empty; }
                try { ToLanguage = rootElem.GetChildAsString("ToLanguage"); } catch { ToLanguage = string.Empty; }
                try { TranslationService = rootElem.GetChildAsString("TranslationService"); } catch { TranslationService = "GoogleWeb"; }
                try { TranslationServices = rootElem.GetChildAsString("TranslationServices"); } catch { TranslationServices = "GoogleWeb,YandexWeb"; }

                try
                {
                    XmlNode? debugNode = rootElem.SelectSingleNode("DebugerSettings");
                    if (debugNode != null)
                    {
                        DebugerSettings.LoadFromXml(debugNode);
                    }
                    else
                    {
                        DebugerSettings = new DebugerSettings();
                    }
                }
                catch
                {
                    DebugerSettings = new DebugerSettings();
                }

                try { LanguageIsRussian = rootElem.GetChildAsBool("LanguageIsRussian"); } catch { LanguageIsRussian = false; }

                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Saves the configuration to the specified file.
        /// <para>Сохраняет конфигурацию в указанный файл.</para>
        /// </summary>
#pragma warning disable CS0108
        public bool Save(string fileName, out string errMsg)
#pragma warning restore CS0108
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("Config");
                xmlDoc.AppendChild(rootElem);

                try { rootElem.AppendElem("FromLanguage", FromLanguage); } catch { }
                try { rootElem.AppendElem("ToLanguage", ToLanguage); } catch { }
                try { rootElem.AppendElem("TranslationService", TranslationService); } catch { }
                try { rootElem.AppendElem("TranslationServices", TranslationServices); } catch { }
                try { DebugerSettings.SaveToXml(rootElem.AppendElem("DebugerSettings")); } catch { }
                try { rootElem.AppendElem("LanguageIsRussian", LanguageIsRussian); } catch { }

                xmlDoc.Save(fileName);
                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        #endregion Basic
    }
}
