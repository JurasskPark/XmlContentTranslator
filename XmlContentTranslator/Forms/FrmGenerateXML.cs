using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XmlContentTranslator.Translator;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Form for converting design file to XML.
    /// <para>Форма конвертирования файл дизайна в XML.</para>
    /// </summary>
    public partial class FrmGenerateXML : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmGenerateXML()
        {
            InitializeComponent();
            Translate();
        }

        #region Variable
        private List<string> designerFiles = new List<string>();                    // list of files with design content
        private string nameDictionaries = string.Empty;                             // names of dictionaries
        private string nameDriver = string.Empty;                                   // driver name

        private const string OptionalThisPattern = @"(?:this\.)?";
        private const string ToolStripItemArrayPattern = @"(?:System\.Windows\.Forms\.)?ToolStripItem\[\]";

        #endregion Variable

        #region Control

        #region Menu

        #region Import From File

        /// <summary>
        /// Import from file.
        /// <para>Импорт из файла.</para>
        /// </summary>
        private void mnuImportFromFile_Click(object sender, EventArgs e)
        {
            ImportFromFile();
            Proccess();
        }

        /// <summary>
        /// Import from file.
        /// <para>Импорт из файла.</para>
        /// </summary>
        private void ImportFromFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = ClientPhrases.TitleDesignerFiles;
                ofd.Filter = ClientPhrases.FilterDesignerFiles;
                ofd.FilterIndex = 1;
                ofd.Multiselect = true;
                ofd.CheckFileExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    designerFiles.Clear();
                    designerFiles.AddRange(ofd.FileNames);
                }
            }
        }

        #endregion Import From File

        #region Import From Folder
        /// <summary>
        /// Import from folder.
        /// <para>Импорт из папки.</para>
        /// </summary>
        private void mnuImportFromFolder_Click(object sender, EventArgs e)
        {
            ImportFromFolder();
            Proccess();
        }

        /// <summary>
        /// Import from folder.
        /// <para>Импорт из папки.</para>
        /// </summary>
        private void ImportFromFolder()
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    designerFiles.Clear();
                    designerFiles = Directory.GetFiles(ofd.SelectedPath, "*.Designer.cs", SearchOption.AllDirectories).ToList();
                }
            }
        }
        #endregion Import From Folder

        #region Save / SaveAs
        /// <summary>
        /// Save.
        /// <para>Сохранить.</para>
        /// </summary>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveXml();
        }

        /// <summary>
        /// Save as.
        /// <para>Сохранить как.</para>
        /// </summary>
        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveXml(true);
        }

        /// <summary>
        /// Save XML.
        /// <para>Сохранить XML.</para>
        /// </summary>
        private void SaveXml(bool showDialog = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtResult.Text))
                {
                    MessageBox.Show(ClientPhrases.NoDataToSave, "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string filePath;

                if (showDialog)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Title = ClientPhrases.TitleXMLFiles;
                        sfd.Filter = ClientPhrases.FilterXMLFiles;
                        sfd.FilterIndex = 1;
                        sfd.DefaultExt = "xml";
                        sfd.FileName = $"Localization_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
                        sfd.InitialDirectory = Application.StartupPath;

                        sfd.OverwritePrompt = true;
                        sfd.AddExtension = true;

                        if (sfd.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        filePath = sfd.FileName;
                    }
                }
                else
                {
                    string fileName = $"Localization_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
                    string folderPath = Path.Combine(Application.StartupPath, "Localization");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filePath = Path.Combine(folderPath, fileName);
                }

                using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    writer.Write(txtResult.Text);
                }

                // additionally: copy the path to the clipboard
                Clipboard.SetText(filePath);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }
        #endregion Save / SaveAs

        #endregion Menu

        #region TextBox
        /// <summary>
        /// Getting the name of the dictionaries.
        /// <para>Получение название словарей.</para>
        /// </summary>
        private void txtNameDictionaries_TextChanged(object sender, EventArgs e)
        {
            nameDictionaries = txtNameDictionaries.Text.Trim();
        }

        /// <summary>
        /// Getting the driver name.
        /// <para>Получение название драйвера.</para>
        /// </summary>
        private void txtNameDriver_TextChanged(object sender, EventArgs e)
        {
            nameDriver = txtNameDriver.Text.Trim();
        }

        #endregion TextBox

        #endregion Control

        #region Basic
        /// <summary>
        /// Translates the shell form.
        /// <para>Переводит форму оболочки.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// The process of preparing and processing files.
        /// <para>Процесс подготовки и обработки файлов.</para>
        /// </summary>
        private void Proccess()
        {
            try
            {
                if (designerFiles.Count > 0)
                {
                    var results = new List<FormAnalysisResult>();
                    foreach (var file in designerFiles)
                    {
                        var result = ProcessDesignerFile(file);
                        if (result != null)
                        {
                            results.Add(result);
                        }
                    }

                    GenerateXml(results);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// The process of preparing and processing a design file.
        /// <para>Процесс подготовки и обработки дизайнерского файла.</para>
        /// </summary>
        private FormAnalysisResult ProcessDesignerFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath, Encoding.UTF8);

                // finding a namespace and class name
                var namespaceMatch = Regex.Match(content, @"namespace\s+([^\s{]+)");
                var classMatch = Regex.Match(content, @"partial\s+class\s+([^\s{]+)");

                if (!namespaceMatch.Success || !classMatch.Success)
                {
                    return new FormAnalysisResult();
                }

                var namespaceName = namespaceMatch.Groups[1].Value.Trim();
                var className = classMatch.Groups[1].Value.Trim();
                var fullClassName = $"{namespaceName}.{className}";

                var result = new FormAnalysisResult
                {
                    FileName = Path.GetFileName(filePath),
                    FullPath = filePath,
                    Namespace = namespaceName,
                    ClassName = className,
                    FullClassName = fullClassName,
                    Properties = new List<PropertyInfo>()
                };

                // first, we find all the control declarations
                var controlDeclarations = new HashSet<string>();
                var fullDeclarationMatches = Regex.Matches(content,
                    @"private\s+System\.Windows\.Forms\.(\w+)\s+(?<controlName>\w+);",
                    RegexOptions.Multiline);

                foreach (Match match in fullDeclarationMatches)
                {
                    controlDeclarations.Add(match.Groups["controlName"].Value);
                }

                var shortDeclarationMatches = Regex.Matches(content,
                    @"private\s+(?!System\.Windows\.Forms\.)(\w+)\s+(?<controlName>\w+);",
                    RegexOptions.Multiline);

                foreach (Match match in shortDeclarationMatches)
                {
                    var controlType = match.Groups[1].Value;
                    // Исключаем типы, которые не являются контролами (int, string и т.д.)
                    if (IsLikelyControlType(controlType))
                    {
                        controlDeclarations.Add(match.Groups["controlName"].Value);
                    }
                }

                // text properties - include even empty ones
                var textMatches = Regex.Matches(content,
                    @"(?<controlName>\w+)\.Text\s*=\s*""(?<textValue>[^""]*)"";",
                    RegexOptions.Multiline);

                var processedControls = new HashSet<string>();

                foreach (Match match in textMatches)
                {
                    var controlName = match.Groups["controlName"].Value;
                    var textValue = match.Groups["textValue"].Value;

                    // we even add empty lines, but only for controls that are in ads
                    if (controlDeclarations.Contains(controlName) || IsLikelyControl(controlName))
                    {
                        result.Properties.Add(new PropertyInfo
                        {
                            ControlName = controlName,
                            PropertyName = "Text",
                            Value = textValue
                        });
                        processedControls.Add(controlName);
                    }
                }

                // find controls that have been declared but do not have a Text setting in InitializeComponent
                foreach (var controlName in controlDeclarations)
                {
                    if (!processedControls.Contains(controlName) &&
                        IsTextControl(controlName, content)) // checking if a control can have text
                    {
                        // add with an empty line for localization
                        result.Properties.Add(new PropertyInfo
                        {
                            ControlName = controlName,
                            PropertyName = "Text",
                            Value = ""
                        });
                    }
                }

                // toolTipText properties
                var tooltipMatches = Regex.Matches(content,
                    @"(?<controlName>\w+)\.ToolTipText\s*=\s*""(?<textValue>[^""]*)"";",  // Убрали + после [^""]
                    RegexOptions.Multiline);

                foreach (Match match in tooltipMatches)
                {
                    var controlName = match.Groups["controlName"].Value;
                    var textValue = match.Groups["textValue"].Value;

                    // add with an empty line for localization
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = controlName,
                        PropertyName = "ToolTipText",
                        Value = textValue
                    });
                }

                // extract comboBox items
                var comboBoxMatches = Regex.Matches(content,
                    @"this\.(?<controlName>\w+ComboBox)\.[\s\w]*?(?<items>Items\.AddRange\(new object\[\] \{.*?\}\))",
                    RegexOptions.Singleline);

                foreach (Match match in comboBoxMatches)
                {
                    var controlName = match.Groups["controlName"].Value;
                    var itemsString = match.Groups["items"].Value;

                    // extract individual items
                    var itemMatches = Regex.Matches(itemsString, @"""(?<item>[^""]+)""");
                    int itemIndex = 0;

                    foreach (Match itemMatch in itemMatches)
                    {
                        var itemValue = itemMatch.Groups["item"].Value;
                        if (!string.IsNullOrWhiteSpace(itemValue))
                        {
                            result.Properties.Add(new PropertyInfo
                            {
                                ControlName = controlName,
                                PropertyName = $"Items[{itemIndex}]",
                                Value = itemValue
                            });
                            itemIndex++;
                        }
                    }
                }

                AddColumnProperties(content, result);

                // menu
                ExtractMenuItems(content, result);

                return result;
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return new FormAnalysisResult();
            }
        }

        /// <summary>
        /// Add Column Properties.
        /// <para>Добавление столбца с параметрами.</para>>
        /// </summary>
        private static void AddColumnProperties(string content, FormAnalysisResult result)
        {
            var analysis = DesignerColumnsAnalyzer.Analyze(content);

            foreach (var pair in analysis.ListViewHeaderTexts)
            {
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    var header = pair.Value[i];
                    if (string.IsNullOrWhiteSpace(header))
                    {
                        continue;
                    }

                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = pair.Key,
                        PropertyName = $"Columns[{i}].Text",
                        Value = header
                    });
                }
            }

            foreach (var pair in analysis.DataGridViewHeaderTexts)
            {
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    var header = pair.Value[i];
                    if (string.IsNullOrWhiteSpace(header))
                    {
                        continue;
                    }

                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = pair.Key,
                        PropertyName = $"Columns[{i}].HeaderText",
                        Value = header
                    });
                }
            }
        }

        /// <summary>
        /// Check if type name is likely a control type.
        /// <para>Проверка, является ли имя типа вероятно типом контрола.</para>
        /// </summary>
        private bool IsLikelyControlType(string typeName)
        {
            var controlTypes = new[] {
                "Button", "TextBox", "Label", "CheckBox", "RadioButton",
                "GroupBox", "Panel", "ListBox", "ComboBox", "ListView",
                "TreeView", "DataGridView", "PictureBox", "RichTextBox",
                "DateTimePicker", "MonthCalendar", "MaskedTextBox",
                "NumericUpDown", "ProgressBar", "TrackBar"
            };
            return controlTypes.Contains(typeName);
        }

        /// <summary>
        /// Extracting menu items.
        /// <para>Извлечение элементов меню.</para>
        /// </summary>
        private void ExtractMenuItems(string content, FormAnalysisResult result)
        {
            try
            {
                var menuStrips = new Dictionary<string, string>();

                var menuStripDeclarations = Regex.Matches(content,
                    @"private\s+(?:System\.Windows\.Forms\.)?(?<type>MenuStrip|ContextMenuStrip|ToolStrip)\s+(?<name>\w+);",
                    RegexOptions.Multiline);

                foreach (Match match in menuStripDeclarations)
                {
                    menuStrips[match.Groups["name"].Value] = match.Groups["type"].Value;
                }

                foreach (var menuStrip in menuStrips.Keys)
                {
                    var itemsMatch = Regex.Match(content,
                        $@"{OptionalThisPattern}{menuStrip}\.Items\.AddRange\(new {ToolStripItemArrayPattern}\s*\{{\s*(?<items>.*?)\s*\}}\);",
                        RegexOptions.Singleline);

                    if (itemsMatch.Success)
                    {
                        var itemsContent = itemsMatch.Groups["items"].Value;
                        var itemNameMatches = Regex.Matches(itemsContent, $@"{OptionalThisPattern}(?<itemName>\w+)");

                        foreach (Match itemMatch in itemNameMatches)
                        {
                            var itemName = itemMatch.Groups["itemName"].Value;
                            ExtractMenuItemProperties(content, itemName, result);
                        }
                    }
                }

                var menuItemDeclarations = new HashSet<string>();
                var declarationMatches = Regex.Matches(content,
                    @"private\s+(?:System\.Windows\.Forms\.)?(?:ToolStripMenuItem|ToolStripDropDownButton)\s+(?<itemName>\w+);",
                    RegexOptions.Multiline);

                foreach (Match match in declarationMatches)
                {
                    menuItemDeclarations.Add(match.Groups["itemName"].Value);
                }

                foreach (var itemName in menuItemDeclarations)
                {
                    ExtractMenuItemProperties(content, itemName, result);
                }

                foreach (var itemName in menuItemDeclarations)
                {
                    FindNestedMenuItems(content, itemName, result);
                }

                var textAssignments = Regex.Matches(content,
                    @"(?<itemName>\w+)\.Text\s*=\s*""(?<text>[^""]*)"";",
                    RegexOptions.Multiline);

                foreach (Match match in textAssignments)
                {
                    var itemName = match.Groups["itemName"].Value;
                    var textValue = match.Groups["text"].Value;

                    // Пропускаем обычные контролы (Label, Button и т.д.)
                    if (IsLikelyMenuItem(itemName, content))
                    {
                        // Проверяем, не добавили ли мы уже этот элемент
                        if (!result.Properties.Any(p => p.ControlName == itemName && p.PropertyName == "Text"))
                        {
                            result.Properties.Add(new PropertyInfo
                            {
                                ControlName = itemName,
                                PropertyName = "Text",
                                Value = textValue
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Extracting properties of menu items.
        /// <para>Извлечение свойств элементов меню.</para>
        /// </summary>
        private void ExtractMenuItemProperties(string content, string itemName, FormAnalysisResult result)
        {
            var textMatch = Regex.Match(content,
                $@"{itemName}\.Text\s*=\s*""(?<text>[^""]*)"";",
                RegexOptions.Multiline);

            if (textMatch.Success)
            {
                var textValue = textMatch.Groups["text"].Value;

                if (!result.Properties.Any(p => p.ControlName == itemName && p.PropertyName == "Text"))
                {
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = itemName,
                        PropertyName = "Text",
                        Value = textValue
                    });
                }
            }
            else
            {
                if (!result.Properties.Any(p => p.ControlName == itemName && p.PropertyName == "Text"))
                {
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = itemName,
                        PropertyName = "Text",
                        Value = ""
                    });
                }
            }

            var tooltipMatch = Regex.Match(content,
                $@"{itemName}\.ToolTipText\s*=\s*""(?<tooltip>[^""]*)"";",
                RegexOptions.Multiline);

            if (tooltipMatch.Success)
            {
                var tooltipValue = tooltipMatch.Groups["tooltip"].Value;
                result.Properties.Add(new PropertyInfo
                {
                    ControlName = itemName,
                    PropertyName = "ToolTipText",
                    Value = tooltipValue
                });
            }
        }

        /// <summary>
        /// Find for nested menu items.
        /// <para>Поиск вложенных пунктов меню.</para>
        /// </summary>
        private void FindNestedMenuItems(string content, string parentItemName, FormAnalysisResult result)
        {
            var dropDownMatch = Regex.Match(content,
                $@"{OptionalThisPattern}{parentItemName}\.DropDownItems\.AddRange\(new {ToolStripItemArrayPattern}\s*\{{\s*(?<items>.*?)\s*\}}\);",
                RegexOptions.Singleline);

            if (dropDownMatch.Success)
            {
                var itemsContent = dropDownMatch.Groups["items"].Value;
                var itemNameMatches = Regex.Matches(itemsContent, $@"{OptionalThisPattern}(?<itemName>\w+)");

                foreach (Match itemMatch in itemNameMatches)
                {
                    var itemName = itemMatch.Groups["itemName"].Value;

                    ExtractMenuItemProperties(content, itemName, result);
                    FindNestedMenuItems(content, itemName, result);
                }
            }
        }

        /// <summary>
        /// Find the menu by the name template.
        /// <para>Поиск меню по шаблону названия.</para>
        /// </summary>
        private bool IsLikelyMenuItem(string itemName, string content)
        {
            var declarationMatch = Regex.IsMatch(content,
                $@"private\s+(?:System\.Windows\.Forms\.)?(?:ToolStripMenuItem|ToolStripDropDownButton)\s+{itemName};",
                RegexOptions.Multiline);

            if (declarationMatch)
            {
                return true;
            }

            if (itemName.StartsWith("tol") ||
                itemName.StartsWith("mnu") ||
                itemName.StartsWith("menu") ||
                itemName.StartsWith("tsmi") ||
                itemName.StartsWith("toolStripMenuItem"))
            {
                return true;
            }

            var usedInMenuContext = Regex.IsMatch(content,
                $@"(?:MenuStrip|ContextMenuStrip|DropDownItems).*?{itemName}",
                RegexOptions.Singleline);

            return usedInMenuContext;
        }


        /// <summary>
        /// Extract single nenu item.
        /// <para>Извлечение отдельного элемента меню.</para>
        /// </summary>
        private void ExtractSingleMenuItem(string content, string itemName, string parentMenuName, FormAnalysisResult result)
        {
            try
            {
                var declarationMatch = Regex.Match(content,
                    $@"private\s+System\.Windows\.Forms\.ToolStripMenuItem\s+{itemName};",
                    RegexOptions.Multiline);

                if (!declarationMatch.Success)
                {
                    return;
                }

                var textMatch = Regex.Match(content,
                    $@"this\.{itemName}\.Text\s*=\s*""(?<text>[^""]*)"";",
                    RegexOptions.Multiline);

                if (textMatch.Success)
                {
                    var textValue = textMatch.Groups["text"].Value;
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = itemName,
                        PropertyName = "Text",
                        Value = textValue,
                        ParentMenu = parentMenuName
                    });
                }
                else
                {
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = itemName,
                        PropertyName = "Text",
                        Value = "",
                        ParentMenu = parentMenuName
                    });
                }

                var tooltipMatch = Regex.Match(content,
                    $@"this\.{itemName}\.ToolTipText\s*=\s*""(?<tooltip>[^""]*)"";",
                    RegexOptions.Multiline);

                if (tooltipMatch.Success)
                {
                    var tooltipValue = tooltipMatch.Groups["tooltip"].Value;
                    result.Properties.Add(new PropertyInfo
                    {
                        ControlName = itemName,
                        PropertyName = "ToolTipText",
                        Value = tooltipValue,
                        ParentMenu = parentMenuName
                    });
                }

                // search for nested submenus
                var dropDownItemsMatch = Regex.Match(content,
                    $@"{OptionalThisPattern}{itemName}\.DropDownItems\.AddRange\(new {ToolStripItemArrayPattern}\s*\{{\s*(?<items>.*?)\s*\}}\);",
                    RegexOptions.Singleline);

                if (dropDownItemsMatch.Success)
                {
                    var subItemsContent = dropDownItemsMatch.Groups["items"].Value;
                    var subItemMatches = Regex.Matches(subItemsContent, $@"{OptionalThisPattern}(?<subItemName>\w+)");

                    foreach (Match subItemMatch in subItemMatches)
                    {
                        var subItemName = subItemMatch.Groups["subItemName"].Value;
                        ExtractSingleMenuItem(content, subItemName, parentMenuName, result);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error extracting single menu item {itemName}: {ex.Message}");
            }
        }


        /// <summary>
        /// Checking control.
        /// <para>Проверка контрола.</para>
        /// </summary>
        private bool IsTextControl(string controlName, string content)
        {
            // check the control type by its declaration
            var fullTypeMatch = Regex.Match(content,
                $@"private\s+System\.Windows\.Forms\.(\w+)\s+{controlName};");

            if (fullTypeMatch.Success)
            {
                var controlType = fullTypeMatch.Groups[1].Value;
                return IsTextControlType(controlType);
            }

            var shortTypeMatch = Regex.Match(content,
                $@"private\s+(\w+)\s+{controlName};");

            if (shortTypeMatch.Success)
            {
                var controlType = shortTypeMatch.Groups[1].Value;
                return IsTextControlType(controlType);
            }

            // if you haven't found the ad, check it by prefix.
            return IsLikelyControl(controlName);
        }

        /// <summary>
        /// Check if control type can have text.
        /// <para>Проверка, может ли тип контрола иметь текст.</para>
        /// </summary>
        private bool IsTextControlType(string controlType)
        {
            var textControlTypes = new[] {
                "Label", "Button", "TextBox", "CheckBox",
                "RadioButton", "LinkLabel", "GroupBox", "ListView",
                "TreeView", "DataGridView", "ComboBox",
                "RichTextBox", "ListBox", "CheckedListBox",
                "DateTimePicker", "MonthCalendar", "MaskedTextBox"
            };
            return textControlTypes.Contains(controlType);
        }

        /// <summary>
        /// Check by naming pattern.
        /// <para>Мы проверяем по шаблону именования.</para>
        /// </summary>
        private bool IsLikelyControl(string name)
        {
            // check by naming pattern (prefixes lbl, btn, txt, etc.)
            return name.StartsWith("lbl") ||
                      name.StartsWith("btn") ||
                      name.StartsWith("txt") ||
                      name.StartsWith("chk") ||
                      name.StartsWith("ckb") ||
                      name.StartsWith("rdo") ||
                      name.StartsWith("rdb") ||
                      name.StartsWith("grp") ||
                      name.StartsWith("gpb") ||
                      name.StartsWith("lnk");
        }

        /// <summary>
        /// Generating an XML file from the search results.
        /// <para>Генерация из результатов нахождения XML файла.</para>
        /// </summary>
        private void GenerateXml(List<FormAnalysisResult> results)
        {
            try
            {
                string nameRoot = $@"{nameDictionaries}Dictionaries";
                var root = new XElement(nameRoot);

                foreach (var result in results.Where(r => r.Properties.Any()))
                {
                    var dictionaryElement = new XElement("Dictionary",
                        new XAttribute("key", $"{nameDriver}{result.ClassName}"));

                    // add an element for the form title (this)
                    dictionaryElement.Add(new XElement("Phrase",
                        new XAttribute("key", "this"),
                        result.ClassName));

                    // add all found properties
                    foreach (var property in result.Properties)
                    {
                        var key = $"{property.ControlName}.{property.PropertyName}";
                        var phraseElement = new XElement("Phrase",
                            new XAttribute("key", key),
                            property.Value);

                        // add a comment for empty lines that require translation.
                        if (string.IsNullOrEmpty(property.Value))
                        {
                            phraseElement.Add(new XComment($" {ClientPhrases.TranslationRequired} "));
                        }

                        dictionaryElement.Add(phraseElement);
                    }

                    root.Add(dictionaryElement);
                }

                var xmlDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    root);

                txtResult.Text = xmlDoc.ToString();
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }
        #endregion Basic

        #region Show Error
        /// <summary>
        /// Displaying an error message
        /// <para>Отображение сообщения с ошибкой</para>
        /// </summary>
        private void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show(this, ex.InnerException != null ? ex.InnerException.Message : ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Show Error

    }
}
