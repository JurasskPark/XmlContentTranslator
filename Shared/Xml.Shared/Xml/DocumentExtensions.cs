using System.Xml;
using System.Xml.Linq;

namespace Xml.Shared
{
    /// <summary>
    /// Provides conversion helpers between <see cref="XmlDocument"/> and <see cref="XDocument"/>.
    /// <para>Предоставляет вспомогательные методы преобразования между <see cref="XmlDocument"/> и <see cref="XDocument"/>.</para>
    /// </summary>
    public static class DocumentExtensions
    {
        #region Basic

        /// <summary>
        /// Converts <see cref="XDocument"/> to <see cref="XmlDocument"/>.
        /// </summary>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();

            using (XmlReader xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

        /// <summary>
        /// Converts <see cref="XmlDocument"/> to <see cref="XDocument"/>.
        /// </summary>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using XmlNodeReader nodeReader = new XmlNodeReader(xmlDocument);
            nodeReader.MoveToContent();
            return XDocument.Load(nodeReader);
        }

        #endregion Basic
    }
}
