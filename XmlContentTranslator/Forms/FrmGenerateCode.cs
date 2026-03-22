using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using XmlContentTranslator.Translator;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Generates helper code for assigning column names in ListView and DataGridView controls.
    /// <para>Генерирует вспомогательный код для назначения имен столбцов ListView и DataGridView.</para>
    /// </summary>
    public partial class FrmGenerateCode : Form
    {
        #region Variable

        private readonly List<string> designerFiles = new List<string>();

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmGenerateCode()
        {
            InitializeComponent();
            Translate();
        }

        /// <summary>
        /// Translates the shell form.
        /// <para>Переводит форму оболочки.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// Processes loaded designer files.
        /// <para>Обрабатывает загруженные дизайнерские файлы.</para>
        /// </summary>
        private void ProcessFiles()
        {
            if (designerFiles.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (string file in designerFiles)
            {
                DesignerColumnModel model = ParseDesignerFile(file);
                sb.AppendLine($"// Source: {file}");
                sb.AppendLine(GenerateCode(model));
                sb.AppendLine();
            }

            txtResult.Text = sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Parses a designer file into a column model.
        /// <para>Разбирает дизайнерский файл в модель столбцов.</para>
        /// </summary>
        private static DesignerColumnModel ParseDesignerFile(string filePath)
        {
            DesignerColumnModel model = new DesignerColumnModel
            {
                FileName = Path.GetFileName(filePath)
            };

            string content = File.ReadAllText(filePath, Encoding.UTF8);
            DesignerColumnsAnalysis analysis = DesignerColumnsAnalyzer.Analyze(content);

            foreach (KeyValuePair<string, List<string>> pair in analysis.ListViewColumnVariables)
            {
                model.ListViews[pair.Key] = pair.Value;
            }

            foreach (KeyValuePair<string, List<string>> pair in analysis.DataGridViewColumnVariables)
            {
                model.DataGridViews[pair.Key] = pair.Value;
            }

            return model;
        }

        /// <summary>
        /// Generates C# code for the column model.
        /// <para>Генерирует C# код для модели столбцов.</para>
        /// </summary>
        private static string GenerateCode(DesignerColumnModel model)
        {
            if (model.ListViews.Count == 0 && model.DataGridViews.Count == 0)
            {
                return "// No ListView/DataGridView columns found.";
            }

            StringBuilder sb = new StringBuilder();
            bool hasContent = false;

            if (model.ListViews.Count > 0)
            {
                hasContent = true;
                sb.AppendLine("private void SetListViewColumnNames()");
                sb.AppendLine("{");

                foreach (KeyValuePair<string, List<string>> pair in model.ListViews)
                {
                    foreach (string column in pair.Value)
                    {
                        sb.AppendLine($"    {column}.Name = nameof({column}.Name);");
                    }
                }

                sb.AppendLine("}");
            }

            if (model.DataGridViews.Count > 0)
            {
                if (hasContent)
                {
                    sb.AppendLine();
                }

                sb.AppendLine("private void SetDataGridViewColumnNames()");
                sb.AppendLine("{");

                foreach (KeyValuePair<string, List<string>> pair in model.DataGridViews)
                {
                    foreach (string column in pair.Value)
                    {
                        sb.AppendLine($"    {column}.Name = nameof({column}.Name);");
                    }
                }

                sb.AppendLine("}");
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Saves the generated result.
        /// <para>Сохраняет сгенерированный результат.</para>
        /// </summary>
        private void SaveResult(bool showDialog)
        {
            if (string.IsNullOrWhiteSpace(txtResult.Text))
            {
                MessageBox.Show("No data to save", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath;

                if (showDialog)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Title = "Save generated code";
                        sfd.Filter = "C# files|*.cs|Text files|*.txt|All files|*.*";
                        sfd.FilterIndex = 1;
                        sfd.DefaultExt = "cs";
                        sfd.FileName = $"GeneratedColumns_{DateTime.Now:yyyyMMdd_HHmmss}.cs";
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
                    string folderPath = Path.Combine(Application.StartupPath, "GeneratedCode");
                    Directory.CreateDirectory(folderPath);
                    filePath = Path.Combine(folderPath, $"GeneratedColumns_{DateTime.Now:yyyyMMdd_HHmmss}.cs");
                }

                File.WriteAllText(filePath, txtResult.Text, new UTF8Encoding(true));
                Clipboard.SetText(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Basic

        #region Control

        /// <summary>
        /// Imports designer files from a file selection dialog.
        /// <para>Импортирует дизайнерские файлы из диалога выбора файлов.</para>
        /// </summary>
        private void mnuImportFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select designer files";
                ofd.Filter = "Designer files|*.Designer.cs|C# files|*.cs|All files|*.*";
                ofd.FilterIndex = 1;
                ofd.Multiselect = true;
                ofd.CheckFileExists = true;

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                designerFiles.Clear();
                designerFiles.AddRange(ofd.FileNames);
            }

            ProcessFiles();
        }

        /// <summary>
        /// Imports designer files from a folder.
        /// <para>Импортирует дизайнерские файлы из папки.</para>
        /// </summary>
        private void mnuImportFromFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                designerFiles.Clear();
                designerFiles.AddRange(Directory.GetFiles(fbd.SelectedPath, "*.Designer.cs", SearchOption.AllDirectories));
            }

            ProcessFiles();
        }

        /// <summary>
        /// Saves the generated code.
        /// <para>Сохраняет сгенерированный код.</para>
        /// </summary>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveResult(false);
        }

        /// <summary>
        /// Saves the generated code with a dialog.
        /// <para>Сохраняет сгенерированный код через диалог.</para>
        /// </summary>
        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveResult(true);
        }

        #endregion Control

        #region Support class

        /// <summary>
        /// Parsed designer column model.
        /// <para>Модель разобранных столбцов дизайнерского файла.</para>
        /// </summary>
        private sealed class DesignerColumnModel
        {
            /// <summary>
            /// Gets or sets the file name.
            /// <para>Получает или задает имя файла.</para>
            /// </summary>
            public string FileName { get; init; } = string.Empty;

            /// <summary>
            /// Gets ListView columns.
            /// <para>Получает столбцы ListView.</para>
            /// </summary>
            public Dictionary<string, List<string>> ListViews { get; } =
                new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            /// <summary>
            /// Gets DataGridView columns.
            /// <para>Получает столбцы DataGridView.</para>
            /// </summary>
            public Dictionary<string, List<string>> DataGridViews { get; } =
                new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion Support class
    }
}
