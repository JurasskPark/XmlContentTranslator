namespace XmlContentTranslator.Forms
{
    partial class FrmGenerateXML
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerateXML));
            mnuMenu = new ToolStrip();
            mnuXMLGenerate = new ToolStripDropDownButton();
            mnuImportFromFile = new ToolStripMenuItem();
            mnuImportFromFolder = new ToolStripMenuItem();
            mnuSave = new ToolStripMenuItem();
            mnuSaveAs = new ToolStripMenuItem();
            tlpPanel = new TableLayoutPanel();
            txtResult = new TextBox();
            pnlPanel = new Panel();
            lblNameDriver = new Label();
            txtNameDriver = new TextBox();
            txtNameDictionaries = new TextBox();
            lblNameDictionaries = new Label();
            mnuMenu.SuspendLayout();
            tlpPanel.SuspendLayout();
            pnlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mnuMenu
            // 
            mnuMenu.BackColor = Color.White;
            mnuMenu.Items.AddRange(new ToolStripItem[] { mnuXMLGenerate });
            mnuMenu.GripStyle = ToolStripGripStyle.Hidden;
            mnuMenu.Location = new Point(0, 0);
            mnuMenu.Name = "mnuMenu";
            mnuMenu.Padding = new Padding(0);
            mnuMenu.Size = new Size(822, 25);
            mnuMenu.Stretch = true;
            mnuMenu.TabIndex = 0;
            mnuMenu.Text = "mnuMenu";
            // 
            // mnuXMLGenerate
            // 
            mnuXMLGenerate.DropDownItems.AddRange(new ToolStripItem[] { mnuImportFromFile, mnuImportFromFolder, mnuSave, mnuSaveAs });
            mnuXMLGenerate.Name = "mnuXMLGenerate";
            mnuXMLGenerate.DisplayStyle = ToolStripItemDisplayStyle.Text;
            mnuXMLGenerate.Size = new Size(93, 25);
            mnuXMLGenerate.Text = "XML Generate";
            // 
            // mnuImportFromFile
            // 
            mnuImportFromFile.Image = (Image)resources.GetObject("mnuImportFromFile.Image");
            mnuImportFromFile.Name = "mnuImportFromFile";
            mnuImportFromFile.Size = new Size(182, 22);
            mnuImportFromFile.Text = "Import from file...";
            mnuImportFromFile.Click += mnuImportFromFile_Click;
            // 
            // mnuImportFromFolder
            // 
            mnuImportFromFolder.Image = (Image)resources.GetObject("mnuImportFromFolder.Image");
            mnuImportFromFolder.Name = "mnuImportFromFolder";
            mnuImportFromFolder.Size = new Size(182, 22);
            mnuImportFromFolder.Text = "Import from folder...";
            mnuImportFromFolder.Click += mnuImportFromFolder_Click;
            // 
            // mnuSave
            // 
            mnuSave.Image = (Image)resources.GetObject("mnuSave.Image");
            mnuSave.Name = "mnuSave";
            mnuSave.Size = new Size(182, 22);
            mnuSave.Text = "Save";
            mnuSave.Click += mnuSave_Click;
            // 
            // mnuSaveAs
            // 
            mnuSaveAs.Image = (Image)resources.GetObject("mnuSaveAs.Image");
            mnuSaveAs.Name = "mnuSaveAs";
            mnuSaveAs.Size = new Size(182, 22);
            mnuSaveAs.Text = "Save as...";
            mnuSaveAs.Click += mnuSaveAs_Click;
            // 
            // tlpPanel
            // 
            tlpPanel.BackColor = Color.WhiteSmoke;
            tlpPanel.ColumnCount = 1;
            tlpPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanel.Controls.Add(txtResult, 0, 1);
            tlpPanel.Controls.Add(pnlPanel, 0, 0);
            tlpPanel.Dock = DockStyle.Fill;
            tlpPanel.Location = new Point(0, 25);
            tlpPanel.Name = "tlpPanel";
            tlpPanel.Padding = new Padding(12);
            tlpPanel.RowCount = 2;
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanel.Size = new Size(822, 451);
            tlpPanel.TabIndex = 1;
            // 
            // txtResult
            // 
            txtResult.AcceptsTab = true;
            txtResult.BackColor = Color.White;
            txtResult.BorderStyle = BorderStyle.FixedSingle;
            txtResult.Dock = DockStyle.Fill;
            txtResult.Font = new Font("Consolas", 9F);
            txtResult.Location = new Point(16, 107);
            txtResult.Margin = new Padding(4, 3, 4, 3);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(790, 330);
            txtResult.TabIndex = 2;
            txtResult.WordWrap = false;
            // 
            // pnlPanel
            // 
            pnlPanel.BackColor = Color.White;
            pnlPanel.BorderStyle = BorderStyle.FixedSingle;
            pnlPanel.Controls.Add(lblNameDriver);
            pnlPanel.Controls.Add(txtNameDriver);
            pnlPanel.Controls.Add(txtNameDictionaries);
            pnlPanel.Controls.Add(lblNameDictionaries);
            pnlPanel.Dock = DockStyle.Fill;
            pnlPanel.Location = new Point(15, 15);
            pnlPanel.Name = "pnlPanel";
            pnlPanel.Padding = new Padding(12, 10, 12, 10);
            pnlPanel.Size = new Size(792, 86);
            pnlPanel.TabIndex = 4;
            // 
            // lblNameDriver
            // 
            lblNameDriver.AutoSize = true;
            lblNameDriver.Location = new Point(12, 50);
            lblNameDriver.Name = "lblNameDriver";
            lblNameDriver.Size = new Size(73, 15);
            lblNameDriver.TabIndex = 3;
            lblNameDriver.Text = "Driver Name";
            // 
            // txtNameDriver
            // 
            txtNameDriver.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNameDriver.Location = new Point(173, 46);
            txtNameDriver.Name = "txtNameDriver";
            txtNameDriver.Size = new Size(596, 23);
            txtNameDriver.TabIndex = 2;
            txtNameDriver.TextChanged += txtNameDriver_TextChanged;
            // 
            // txtNameDictionaries
            // 
            txtNameDictionaries.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNameDictionaries.Location = new Point(173, 17);
            txtNameDictionaries.Name = "txtNameDictionaries";
            txtNameDictionaries.Size = new Size(596, 23);
            txtNameDictionaries.TabIndex = 1;
            txtNameDictionaries.TextChanged += txtNameDictionaries_TextChanged;
            // 
            // lblNameDictionaries
            // 
            lblNameDictionaries.AutoSize = true;
            lblNameDictionaries.Location = new Point(12, 21);
            lblNameDictionaries.Name = "lblNameDictionaries";
            lblNameDictionaries.Size = new Size(122, 15);
            lblNameDictionaries.TabIndex = 0;
            lblNameDictionaries.Text = "Names of dictionaries";
            // 
            // FrmGenerateXML
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(822, 476);
            Controls.Add(tlpPanel);
            Controls.Add(mnuMenu);
            MinimumSize = new Size(760, 460);
            Name = "FrmGenerateXML";
            ShowIcon = false;
            Text = "XML Generator";
            mnuMenu.ResumeLayout(false);
            tlpPanel.ResumeLayout(false);
            tlpPanel.PerformLayout();
            pnlPanel.ResumeLayout(false);
            pnlPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStrip mnuMenu;
        private ToolStripDropDownButton mnuXMLGenerate;
        private ToolStripMenuItem mnuImportFromFile;
        private ToolStripMenuItem mnuImportFromFolder;
        private ToolStripMenuItem mnuSave;
        private ToolStripMenuItem mnuSaveAs;
        private TableLayoutPanel tlpPanel;
        private TextBox txtResult;
        private Panel pnlPanel;
        private Label lblNameDictionaries;
        private TextBox txtNameDictionaries;
        private Label lblNameDriver;
        private TextBox txtNameDriver;
    }
}
