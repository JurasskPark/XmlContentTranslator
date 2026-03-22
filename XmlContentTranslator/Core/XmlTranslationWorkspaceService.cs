using System.Text;
using System.Xml;

namespace XmlContentTranslator.Core
{
    /// <summary>
    /// Service for building and saving XML translation workspaces.
    /// <para>Сервис построения и сохранения рабочих областей перевода XML.</para>
    /// </summary>
    public sealed class XmlTranslationWorkspaceService
    {
        #region Basic

        /// <summary>
        /// Loads a source XML document into a translation workspace.
        /// <para>Загружает исходный XML-документ в рабочую область перевода.</para>
        /// </summary>
        public TranslationWorkspace LoadSource(string filePath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            TranslationWorkspace workspace = new TranslationWorkspace
            {
                SourceDocument = document,
                SourceFilePath = filePath,
                IsScadaFormat = IsScadaDictionary(document),
                SourceLanguageName = ResolveLanguageName(document, "Language 1")
            };

            if (document.DocumentElement != null)
            {
                BuildEntries(workspace, document.DocumentElement, string.Empty);
            }

            return workspace;
        }

        /// <summary>
        /// Loads a target XML document into an existing workspace.
        /// <para>Загружает целевой XML-документ в существующую рабочую область.</para>
        /// </summary>
        public void LoadTarget(TranslationWorkspace workspace, string filePath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            workspace.TargetDocument = document;
            workspace.TargetFilePath = filePath;
            workspace.TargetLanguageName = ResolveLanguageName(document, "Language 2");

            Dictionary<string, XmlNode> targetIndex = BuildTargetNodeIndex(document);
            foreach (TranslationEntry entry in workspace.Entries)
            {
                if (targetIndex.TryGetValue(entry.Path, out XmlNode? targetNode))
                {
                    entry.TargetNode = targetNode;
                    entry.TargetText = ReadNodeText(targetNode, workspace.IsScadaFormat);
                }
            }
        }

        /// <summary>
        /// Creates an empty target document from the source document.
        /// <para>Создает пустой целевой документ на основе исходного документа.</para>
        /// </summary>
        public void CreateEmptyTarget(TranslationWorkspace workspace)
        {
            string xml = workspace.SourceDocument.OuterXml;
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            workspace.TargetDocument = document;
            workspace.TargetFilePath = null;
            workspace.TargetLanguageName = "Language 2";

            Dictionary<string, XmlNode> targetIndex = BuildTargetNodeIndex(document);
            foreach (TranslationEntry entry in workspace.Entries)
            {
                if (!targetIndex.TryGetValue(entry.Path, out XmlNode? targetNode))
                {
                    continue;
                }

                entry.TargetNode = targetNode;
                entry.TargetText = string.Empty;
                WriteNodeText(targetNode, string.Empty);
            }
        }

        /// <summary>
        /// Saves the target document to file.
        /// <para>Сохраняет целевой документ в файл.</para>
        /// </summary>
        public void SaveTarget(TranslationWorkspace workspace, string filePath)
        {
            if (workspace.TargetDocument == null)
            {
                throw new InvalidOperationException("Target document is not initialized.");
            }

            foreach (TranslationEntry entry in workspace.Entries)
            {
                if (entry.TargetNode != null)
                {
                    WriteNodeText(entry.TargetNode, entry.TargetText);
                }
            }

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (StringWriter sw = new StringWriter())
            using (XmlWriter xw = XmlWriter.Create(sw, settings))
            {
                workspace.TargetDocument.Save(xw);

                string content = sw.ToString()
                    .Replace("<HelpFile></HelpFile>", "<HelpFile />", StringComparison.Ordinal)
                    .Replace("encoding=\"utf-16\"?", "encoding=\"utf-8\"?", StringComparison.Ordinal);

                File.WriteAllText(filePath, content, Encoding.UTF8);
                workspace.TargetFilePath = filePath;
            }
        }

        /// <summary>
        /// Resolves a language name from a document.
        /// <para>Определяет имя языка из документа.</para>
        /// </summary>
        public static string ResolveLanguageName(XmlDocument document, string fallback)
        {
            if (document.DocumentElement == null)
            {
                return fallback;
            }

            foreach (string attrName in new[] { "Name", "name", "lang" })
            {
                XmlAttribute? attribute = document.DocumentElement.Attributes?[attrName];
                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.InnerText))
                {
                    return attribute.InnerText;
                }
            }

            XmlNode? cultureNode = document.DocumentElement.SelectSingleNode("General/CultureName");
            if (cultureNode != null && !string.IsNullOrWhiteSpace(cultureNode.InnerText))
            {
                return cultureNode.InnerText;
            }

            return fallback;
        }

        /// <summary>
        /// Determines whether a document uses SCADA dictionary format.
        /// <para>Определяет, использует ли документ формат словаря SCADA.</para>
        /// </summary>
        private static bool IsScadaDictionary(XmlDocument document)
        {
            return document.DocumentElement?.Name.EndsWith("dictionaries", StringComparison.OrdinalIgnoreCase) == true &&
                   document.DocumentElement.SelectSingleNode("Dictionary/Phrase")?.Attributes?["key"] != null;
        }

        /// <summary>
        /// Builds an index of target nodes by path.
        /// <para>Строит индекс целевых узлов по пути.</para>
        /// </summary>
        private static Dictionary<string, XmlNode> BuildTargetNodeIndex(XmlDocument document)
        {
            Dictionary<string, XmlNode> index = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
            if (document.DocumentElement != null)
            {
                IndexNodes(document.DocumentElement, string.Empty, index);
            }

            return index;
        }

        /// <summary>
        /// Builds translation entries recursively.
        /// <para>Рекурсивно строит элементы перевода.</para>
        /// </summary>
        private void BuildEntries(TranslationWorkspace workspace, XmlNode node, string parentPath)
        {
            string nodePath = BuildNodePath(node, parentPath);
            AddAttributeEntries(workspace, node, nodePath);

            if (HasTranslatableText(node))
            {
                AddEntry(workspace, node, nodePath, parentPath);
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    BuildEntries(workspace, childNode, nodePath);
                }
            }
        }

        /// <summary>
        /// Indexes nodes recursively.
        /// <para>Индексирует узлы рекурсивно.</para>
        /// </summary>
        private static void IndexNodes(XmlNode node, string parentPath, IDictionary<string, XmlNode> index)
        {
            string nodePath = BuildNodePath(node, parentPath);
            index[nodePath] = node;

            if (node.Attributes != null)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    index[$"{nodePath}/@{attribute.Name}"] = attribute;
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    IndexNodes(childNode, nodePath, index);
                }
            }
        }

        /// <summary>
        /// Builds a stable node path.
        /// <para>Строит стабильный путь узла.</para>
        /// </summary>
        private static string BuildNodePath(XmlNode node, string parentPath)
        {
            if (node.NodeType != XmlNodeType.Element)
            {
                return parentPath;
            }

            string? key = node.Attributes?["key"]?.InnerText;
            if (!string.IsNullOrWhiteSpace(key))
            {
                return $"{parentPath}/{node.Name}[@key='{key}']";
            }

            int siblingIndex = 0;
            XmlNode? prev = node.PreviousSibling;
            while (prev != null)
            {
                if (prev.NodeType == XmlNodeType.Element && prev.Name == node.Name)
                {
                    siblingIndex++;
                }

                prev = prev.PreviousSibling;
            }

            return $"{parentPath}/{node.Name}[{siblingIndex}]";
        }

        /// <summary>
        /// Adds translatable attribute entries.
        /// <para>Добавляет переводимые атрибуты.</para>
        /// </summary>
        private static void AddAttributeEntries(TranslationWorkspace workspace, XmlNode node, string nodePath)
        {
            if (node.Attributes == null)
            {
                return;
            }

            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (string.Equals(attribute.Name, "key", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(attribute.Value))
                {
                    continue;
                }

                string path = $"{nodePath}/@{attribute.Name}";
                TranslationEntry entry = new TranslationEntry
                {
                    Path = path,
                    ParentPath = nodePath,
                    DisplayKey = $"{ResolveDisplayName(node)}.@{attribute.Name}",
                    SourceText = attribute.Value,
                    SourceNode = attribute
                };

                workspace.Entries.Add(entry);
                workspace.EntriesByPath[path] = entry;
            }
        }

        /// <summary>
        /// Adds a translatable node entry.
        /// <para>Добавляет переводимый узел.</para>
        /// </summary>
        private static void AddEntry(TranslationWorkspace workspace, XmlNode node, string nodePath, string parentPath)
        {
            TranslationEntry entry = new TranslationEntry
            {
                Path = nodePath,
                ParentPath = parentPath,
                DisplayKey = ResolveDisplayName(node),
                SourceText = ReadNodeText(node, workspace.IsScadaFormat),
                SourceNode = node
            };

            workspace.Entries.Add(entry);
            workspace.EntriesByPath[nodePath] = entry;
        }

        /// <summary>
        /// Resolves a display name for a node.
        /// <para>Определяет отображаемое имя узла.</para>
        /// </summary>
        private static string ResolveDisplayName(XmlNode node)
        {
            if (node.Attributes?["key"] != null)
            {
                return node.Attributes["key"]!.InnerText;
            }

            return node.Name;
        }

        /// <summary>
        /// Checks whether a node contains translatable text.
        /// <para>Проверяет, содержит ли узел переводимый текст.</para>
        /// </summary>
        private static bool HasTranslatableText(XmlNode node)
        {
            if (node.NodeType != XmlNodeType.Element)
            {
                return false;
            }

            if (node.ChildNodes.Cast<XmlNode>().Any(x => x.NodeType == XmlNodeType.Element))
            {
                return false;
            }

            return node.InnerText != null;
        }

        /// <summary>
        /// Reads text from a node according to the document format.
        /// <para>Считывает текст из узла с учетом формата документа.</para>
        /// </summary>
        private static string ReadNodeText(XmlNode node, bool isScadaFormat)
        {
            if (node.NodeType == XmlNodeType.Attribute)
            {
                return node.InnerText;
            }

            if (isScadaFormat && node.Name == "Phrase")
            {
                return node.InnerText;
            }

            return node.InnerXml;
        }

        /// <summary>
        /// Writes text to a node.
        /// <para>Записывает текст в узел.</para>
        /// </summary>
        private static void WriteNodeText(XmlNode node, string text)
        {
            if (node.NodeType == XmlNodeType.Attribute)
            {
                node.InnerText = text;
                return;
            }

            node.InnerText = text;
        }

        #endregion Basic
    }
}
