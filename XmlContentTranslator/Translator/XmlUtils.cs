using System.Text;
using System.Xml;

namespace XmlContentTranslator
{
    /// <summary>
    /// XML helper methods.
    /// <para>Вспомогательные методы работы с XML.</para>
    /// </summary>
    public static class XmlUtils
    {
        #region Basic

        /// <summary>
        /// Determines whether the node contains text children.
        /// <para>Определяет, содержит ли узел текстовые дочерние узлы.</para>
        /// </summary>
        public static bool ContainsText(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Text)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether a node should be treated as a text node.
        /// <para>Определяет, следует ли рассматривать узел как текстовый.</para>
        /// </summary>
        public static bool IsTextNode(XmlNode node)
        {
            if (node.ChildNodes.Count == 1 && node.FirstChild != null && node.FirstChild.NodeType == XmlNodeType.Text)
            {
                return true;
            }

            return ContainsText(node);
        }

        /// <summary>
        /// Builds a node path.
        /// <para>Строит путь узла.</para>
        /// </summary>
        public static string BuildNodePath(XmlNode node)
        {
            StringBuilder sb = new StringBuilder();
            if (node.NodeType == XmlNodeType.Attribute)
            {
                XmlNode old = node;
                XmlElement? owner = (node as XmlAttribute)?.OwnerElement;
                if (owner != null)
                {
                    sb.Append(old.Name);
                    node = owner;
                }
            }

            sb.Insert(0, node.Name);
            while (node.ParentNode != null)
            {
                XmlNode parent = node.ParentNode;
                string index = GetNodeIndex(node);
                sb.Insert(0, parent.Name + index + "/");
                node = parent;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the attribute index string.
        /// <para>Получает строку индекса атрибута.</para>
        /// </summary>
        public static string GetAttributeIndex(XmlNode node, XmlNode child)
        {
            if (node.Attributes == null)
            {
                return string.Empty;
            }

            int nameCount = 0;
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name == child.Name)
                {
                    nameCount++;
                }
            }

            int i = 0;
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute == child)
                {
                    break;
                }

                i++;
            }

            if (i == 0 || nameCount < 2)
            {
                return string.Empty;
            }

            return string.Format("[{0}]", i);
        }

        /// <summary>
        /// Gets the sibling index string for a node.
        /// <para>Получает строку индекса узла среди соседей.</para>
        /// </summary>
        public static string GetNodeIndex(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.CDATA || node.ParentNode == null)
            {
                return string.Empty;
            }

            int i = 0;
            XmlNode parent = node.ParentNode;
            foreach (XmlNode sibling in parent.ChildNodes)
            {
                if (sibling == node)
                {
                    break;
                }

                if (sibling.NodeType == XmlNodeType.Comment || sibling.NodeType == XmlNodeType.CDATA)
                {
                    continue;
                }

                if (node.NodeType == XmlNodeType.Text || sibling.NodeType == XmlNodeType.Text)
                {
                    continue;
                }

                if (sibling.Name == node.Name && sibling.NamespaceURI == node.NamespaceURI)
                {
                    i++;
                }
            }

            if (i == 0)
            {
                return string.Empty;
            }

            return string.Format("[{0}]", i);
        }

        /// <summary>
        /// Determines whether the node is a parent element.
        /// <para>Определяет, является ли узел родительским элементом.</para>
        /// </summary>
        public static bool IsParentElement(XmlNode xnode)
        {
            return xnode.ChildNodes.Count > 0 &&
                   !IsTextNode(xnode) &&
                   xnode.NodeType != XmlNodeType.Comment &&
                   xnode.NodeType != XmlNodeType.CDATA;
        }

        #endregion Basic
    }
}
