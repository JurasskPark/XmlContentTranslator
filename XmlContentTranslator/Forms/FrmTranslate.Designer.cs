namespace XmlContentTranslator.Forms
{
    partial class FrmTranslate
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTranslate));
            lstLanguageTags = new ListView();
            clmTag = new ColumnHeader();
            clmSource = new ColumnHeader();
            clmTarget = new ColumnHeader();
            cmnuMenu = new ContextMenuStrip(components);
            cmnuTranslateGoogle = new ToolStripMenuItem();
            cmnuSetValueFromMaster = new ToolStripMenuItem();
            mnuMenu = new ToolStrip();
            mnuFile = new ToolStripDropDownButton();
            mnuFileNew = new ToolStripMenuItem();
            mnuFileSeparator1 = new ToolStripSeparator();
            mnuFileOpen = new ToolStripMenuItem();
            mnuFileSave = new ToolStripMenuItem();
            mnuFileSaveAs = new ToolStripMenuItem();
            mnuEdit = new ToolStripDropDownButton();
            mnuEditFind = new ToolStripMenuItem();
            mnuTools = new ToolStripDropDownButton();
            mnuToolsTranslateSelectedLines = new ToolStripMenuItem();
            mnuToolsSeparator1 = new ToolStripSeparator();
            mnuToolsTranslationService = new ToolStripMenuItem();
            mnuToolsTranslationServiceGoogleWeb = new ToolStripMenuItem();
            mnuToolsTranslationServiceYandexWeb = new ToolStripMenuItem();
            txtCurrentText = new TextBox();
            trvProject = new TreeView();
            stsStatus = new StatusStrip();
            tssStatus1 = new ToolStripStatusLabel();
            tssStatus2 = new ToolStripStatusLabel();
            lblTo = new Label();
            cmbTranslateTo = new ComboBox();
            lblFrom = new Label();
            cmbTranslateFrom = new ComboBox();
            btnGoToNextBlankLine = new Button();
            tlpPanel1 = new TableLayoutPanel();
            tlpPanel2 = new TableLayoutPanel();
            pnlPanel09 = new Panel();
            pnlPanel05 = new Panel();
            pnlPanel06 = new Panel();
            pnlPanel07 = new Panel();
            pnlPanel08 = new Panel();
            tlpPanel3 = new TableLayoutPanel();
            tlpPanelRight = new TableLayoutPanel();
            txtLog = new TextBox();
            cmnuMenu.SuspendLayout();
            mnuMenu.SuspendLayout();
            stsStatus.SuspendLayout();
            tlpPanel1.SuspendLayout();
            tlpPanel2.SuspendLayout();
            pnlPanel09.SuspendLayout();
            pnlPanel05.SuspendLayout();
            pnlPanel06.SuspendLayout();
            pnlPanel07.SuspendLayout();
            pnlPanel08.SuspendLayout();
            tlpPanel3.SuspendLayout();
            tlpPanelRight.SuspendLayout();
            SuspendLayout();
            // 
            // lstLanguageTags
            // 
            lstLanguageTags.AllowDrop = true;
            lstLanguageTags.BorderStyle = BorderStyle.FixedSingle;
            lstLanguageTags.Columns.AddRange(new ColumnHeader[] { clmTag, clmSource, clmTarget });
            lstLanguageTags.ContextMenuStrip = cmnuMenu;
            lstLanguageTags.Dock = DockStyle.Fill;
            lstLanguageTags.FullRowSelect = true;
            lstLanguageTags.Location = new Point(4, 3);
            lstLanguageTags.Margin = new Padding(4, 3, 4, 3);
            lstLanguageTags.Name = "lstLanguageTags";
            lstLanguageTags.Size = new Size(734, 227);
            lstLanguageTags.TabIndex = 10;
            lstLanguageTags.UseCompatibleStateImageBehavior = false;
            lstLanguageTags.View = View.Details;
            lstLanguageTags.SelectedIndexChanged += lstLanguageTags_SelectedIndexChanged;
            lstLanguageTags.DragDrop += lstLanguageTags_DragDrop;
            lstLanguageTags.DragEnter += lstLanguageTags_DragEnter;
            lstLanguageTags.DoubleClick += listViewLanguageTags_DoubleClick;
            // 
            // clmTag
            // 
            clmTag.Text = "Tag";
            clmTag.Width = 120;
            // 
            // clmSource
            // 
            clmSource.Text = "Source";
            clmSource.Width = 120;
            // 
            // clmTarget
            // 
            clmTarget.Text = "Target";
            clmTarget.Width = 120;
            // 
            // cmnuMenu
            // 
            cmnuMenu.Items.AddRange(new ToolStripItem[] { cmnuTranslateGoogle, cmnuSetValueFromMaster });
            cmnuMenu.Name = "contextMenuStrip1";
            cmnuMenu.Size = new Size(215, 48);
            // 
            // cmnuTranslateGoogle
            // 
            cmnuTranslateGoogle.Name = "cmnuTranslateGoogle";
            cmnuTranslateGoogle.ShortcutKeys = Keys.F5;
            cmnuTranslateGoogle.Size = new Size(214, 22);
            cmnuTranslateGoogle.Text = "Translate";
            cmnuTranslateGoogle.Click += mnuToolsTranslateSelectedLines_Click;
            // 
            // cmnuSetValueFromMaster
            // 
            cmnuSetValueFromMaster.Name = "cmnuSetValueFromMaster";
            cmnuSetValueFromMaster.Size = new Size(214, 22);
            cmnuSetValueFromMaster.Text = "Transfer value from master";
            cmnuSetValueFromMaster.Click += cmnuSetValueFromMaster_Click;
            // 
            // mnuMenu
            // 
            mnuMenu.BackColor = Color.White;
            mnuMenu.GripStyle = ToolStripGripStyle.Hidden;
            mnuMenu.Items.AddRange(new ToolStripItem[] { mnuFile, mnuEdit, mnuTools });
            mnuMenu.Location = new Point(0, 0);
            mnuMenu.Name = "mnuMenu";
            mnuMenu.Padding = new Padding(0);
            mnuMenu.Size = new Size(1072, 25);
            mnuMenu.Stretch = true;
            mnuMenu.TabIndex = 11;
            mnuMenu.Text = "mnuMenu";
            // 
            // mnuFile
            // 
            mnuFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileNew, mnuFileSeparator1, mnuFileOpen, mnuFileSave, mnuFileSaveAs });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(38, 22);
            mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            mnuFileNew.Image = (Image)resources.GetObject("mnuFileNew.Image");
            mnuFileNew.Name = "mnuFileNew";
            mnuFileNew.ShortcutKeys = Keys.Control | Keys.N;
            mnuFileNew.Size = new Size(180, 22);
            mnuFileNew.Text = "New";
            mnuFileNew.Click += mnuFileNew_Click;
            // 
            // mnuFileSeparator1
            // 
            mnuFileSeparator1.Name = "mnuFileSeparator1";
            mnuFileSeparator1.Size = new Size(177, 6);
            // 
            // mnuFileOpen
            // 
            mnuFileOpen.Image = (Image)resources.GetObject("mnuFileOpen.Image");
            mnuFileOpen.Name = "mnuFileOpen";
            mnuFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            mnuFileOpen.Size = new Size(180, 22);
            mnuFileOpen.Text = "Open";
            mnuFileOpen.Click += mnuFileOpen_Click;
            // 
            // mnuFileSave
            // 
            mnuFileSave.Image = (Image)resources.GetObject("mnuFileSave.Image");
            mnuFileSave.Name = "mnuFileSave";
            mnuFileSave.ShortcutKeys = Keys.Control | Keys.S;
            mnuFileSave.Size = new Size(180, 22);
            mnuFileSave.Text = "Save";
            mnuFileSave.Click += mnuFileSave_Click;
            // 
            // mnuFileSaveAs
            // 
            mnuFileSaveAs.Image = (Image)resources.GetObject("mnuFileSaveAs.Image");
            mnuFileSaveAs.Name = "mnuFileSaveAs";
            mnuFileSaveAs.Size = new Size(180, 22);
            mnuFileSaveAs.Text = "Save as...";
            mnuFileSaveAs.Click += mnuFileSaveAs_Click;
            // 
            // mnuEdit
            // 
            mnuEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuEditFind });
            mnuEdit.Name = "mnuEdit";
            mnuEdit.Size = new Size(40, 22);
            mnuEdit.Text = "Edit";
            // 
            // mnuEditFind
            // 
            mnuEditFind.Image = (Image)resources.GetObject("mnuEditFind.Image");
            mnuEditFind.Name = "mnuEditFind";
            mnuEditFind.ShortcutKeys = Keys.Control | Keys.F;
            mnuEditFind.Size = new Size(180, 22);
            mnuEditFind.Text = "Find";
            mnuEditFind.Click += mnuEditFind_Click;
            // 
            // mnuTools
            // 
            mnuTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
            mnuTools.DropDownItems.AddRange(new ToolStripItem[] { mnuToolsTranslateSelectedLines, mnuToolsSeparator1, mnuToolsTranslationService });
            mnuTools.Name = "mnuTools";
            mnuTools.Size = new Size(47, 22);
            mnuTools.Text = "Tools";
            // 
            // mnuToolsTranslateSelectedLines
            // 
            mnuToolsTranslateSelectedLines.Name = "mnuToolsTranslateSelectedLines";
            mnuToolsTranslateSelectedLines.ShortcutKeys = Keys.F5;
            mnuToolsTranslateSelectedLines.Size = new Size(212, 22);
            mnuToolsTranslateSelectedLines.Text = "Translate selected lines";
            mnuToolsTranslateSelectedLines.Click += mnuToolsTranslateSelectedLines_Click;
            // 
            // mnuToolsSeparator1
            // 
            mnuToolsSeparator1.Name = "mnuToolsSeparator1";
            mnuToolsSeparator1.Size = new Size(209, 6);
            // 
            // mnuToolsTranslationService
            // 
            mnuToolsTranslationService.DropDownItems.AddRange(new ToolStripItem[] { mnuToolsTranslationServiceGoogleWeb, mnuToolsTranslationServiceYandexWeb });
            mnuToolsTranslationService.Name = "mnuToolsTranslationService";
            mnuToolsTranslationService.Size = new Size(212, 22);
            mnuToolsTranslationService.Text = "Translation service";
            // 
            // mnuToolsTranslationServiceGoogleWeb
            // 
            mnuToolsTranslationServiceGoogleWeb.Name = "mnuToolsTranslationServiceGoogleWeb";
            mnuToolsTranslationServiceGoogleWeb.Size = new Size(180, 22);
            mnuToolsTranslationServiceGoogleWeb.Text = "GoogleWeb";
            mnuToolsTranslationServiceGoogleWeb.Click += mnuToolsTranslationService_Click;
            // 
            // mnuToolsTranslationServiceYandexWeb
            // 
            mnuToolsTranslationServiceYandexWeb.Name = "mnuToolsTranslationServiceYandexWeb";
            mnuToolsTranslationServiceYandexWeb.Size = new Size(180, 22);
            mnuToolsTranslationServiceYandexWeb.Text = "YandexWeb";
            mnuToolsTranslationServiceYandexWeb.Click += mnuToolsTranslationService_Click;
            // 
            // txtCurrentText
            // 
            txtCurrentText.BackColor = Color.White;
            txtCurrentText.BorderStyle = BorderStyle.FixedSingle;
            txtCurrentText.Dock = DockStyle.Fill;
            txtCurrentText.Location = new Point(4, 236);
            txtCurrentText.Margin = new Padding(4, 3, 4, 3);
            txtCurrentText.Multiline = true;
            txtCurrentText.Name = "txtCurrentText";
            txtCurrentText.ScrollBars = ScrollBars.Vertical;
            txtCurrentText.Size = new Size(734, 194);
            txtCurrentText.TabIndex = 12;
            txtCurrentText.TextChanged += txtCurrentText_TextChanged;
            txtCurrentText.KeyDown += txtCurrentText_KeyDown;
            // 
            // trvProject
            // 
            trvProject.BackColor = Color.White;
            trvProject.BorderStyle = BorderStyle.FixedSingle;
            trvProject.Dock = DockStyle.Fill;
            trvProject.HideSelection = false;
            trvProject.Location = new Point(4, 3);
            trvProject.Margin = new Padding(4, 3, 4, 3);
            trvProject.Name = "trvProject";
            trvProject.Size = new Size(282, 557);
            trvProject.TabIndex = 13;
            trvProject.AfterSelect += trvProject_AfterSelect;
            // 
            // stsStatus
            // 
            stsStatus.BackColor = Color.White;
            stsStatus.Items.AddRange(new ToolStripItem[] { tssStatus1, tssStatus2 });
            stsStatus.Location = new Point(0, 661);
            stsStatus.Name = "stsStatus";
            stsStatus.Padding = new Padding(1, 0, 16, 0);
            stsStatus.Size = new Size(1072, 22);
            stsStatus.TabIndex = 14;
            // 
            // tssStatus1
            // 
            tssStatus1.Name = "tssStatus1";
            tssStatus1.Size = new Size(25, 17);
            tssStatus1.Text = "      ";
            // 
            // tssStatus2
            // 
            tssStatus2.Name = "tssStatus2";
            tssStatus2.Size = new Size(1030, 17);
            tssStatus2.Spring = true;
            tssStatus2.Text = "      ";
            tssStatus2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(4, 11);
            lblTo.Margin = new Padding(4, 0, 4, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(22, 15);
            lblTo.TabIndex = 19;
            lblTo.Text = "To:";
            // 
            // cmbTranslateTo
            // 
            cmbTranslateTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTranslateTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTranslateTo.FormattingEnabled = true;
            cmbTranslateTo.Location = new Point(4, 7);
            cmbTranslateTo.Margin = new Padding(4, 3, 4, 3);
            cmbTranslateTo.Name = "cmbTranslateTo";
            cmbTranslateTo.Size = new Size(187, 23);
            cmbTranslateTo.TabIndex = 18;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(4, 11);
            lblFrom.Margin = new Padding(4, 0, 4, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(38, 15);
            lblFrom.TabIndex = 17;
            lblFrom.Text = "From:";
            // 
            // cmbTranslateFrom
            // 
            cmbTranslateFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTranslateFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTranslateFrom.FormattingEnabled = true;
            cmbTranslateFrom.Location = new Point(4, 7);
            cmbTranslateFrom.Margin = new Padding(4, 3, 4, 3);
            cmbTranslateFrom.Name = "cmbTranslateFrom";
            cmbTranslateFrom.Size = new Size(187, 23);
            cmbTranslateFrom.TabIndex = 16;
            // 
            // btnGoToNextBlankLine
            // 
            btnGoToNextBlankLine.Anchor = AnchorStyles.Left;
            btnGoToNextBlankLine.BackColor = Color.FromArgb(232, 239, 247);
            btnGoToNextBlankLine.FlatStyle = FlatStyle.Flat;
            btnGoToNextBlankLine.Location = new Point(4, 4);
            btnGoToNextBlankLine.Margin = new Padding(4, 3, 4, 3);
            btnGoToNextBlankLine.Name = "btnGoToNextBlankLine";
            btnGoToNextBlankLine.Size = new Size(496, 28);
            btnGoToNextBlankLine.TabIndex = 20;
            btnGoToNextBlankLine.Text = "Go to next blank line (F6)";
            btnGoToNextBlankLine.UseVisualStyleBackColor = true;
            btnGoToNextBlankLine.Click += btnGoToNextBlankLine_Click;
            // 
            // tlpPanel1
            // 
            tlpPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlpPanel1.BackColor = Color.WhiteSmoke;
            tlpPanel1.ColumnCount = 1;
            tlpPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanel1.Controls.Add(tlpPanel2, 0, 0);
            tlpPanel1.Controls.Add(tlpPanel3, 0, 1);
            tlpPanel1.Location = new Point(0, 28);
            tlpPanel1.Margin = new Padding(4, 3, 4, 3);
            tlpPanel1.Name = "tlpPanel1";
            tlpPanel1.Padding = new Padding(12, 8, 12, 0);
            tlpPanel1.RowCount = 2;
            tlpPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanel1.Size = new Size(1072, 633);
            tlpPanel1.TabIndex = 21;
            // 
            // tlpPanel2
            // 
            tlpPanel2.ColumnCount = 5;
            tlpPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 72F));
            tlpPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
            tlpPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            tlpPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
            tlpPanel2.Controls.Add(pnlPanel09, 4, 0);
            tlpPanel2.Controls.Add(pnlPanel05, 0, 0);
            tlpPanel2.Controls.Add(pnlPanel06, 1, 0);
            tlpPanel2.Controls.Add(pnlPanel07, 2, 0);
            tlpPanel2.Controls.Add(pnlPanel08, 3, 0);
            tlpPanel2.Location = new Point(16, 11);
            tlpPanel2.Margin = new Padding(4, 3, 4, 3);
            tlpPanel2.Name = "tlpPanel2";
            tlpPanel2.RowCount = 1;
            tlpPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanel2.Size = new Size(1040, 42);
            tlpPanel2.TabIndex = 2;
            // 
            // pnlPanel09
            // 
            pnlPanel09.BackColor = Color.White;
            pnlPanel09.BorderStyle = BorderStyle.FixedSingle;
            pnlPanel09.Controls.Add(cmbTranslateTo);
            pnlPanel09.Dock = DockStyle.Fill;
            pnlPanel09.Location = new Point(839, 3);
            pnlPanel09.Margin = new Padding(4, 3, 4, 3);
            pnlPanel09.Name = "pnlPanel09";
            pnlPanel09.Size = new Size(197, 36);
            pnlPanel09.TabIndex = 4;
            // 
            // pnlPanel05
            // 
            pnlPanel05.BackColor = Color.White;
            pnlPanel05.BorderStyle = BorderStyle.FixedSingle;
            pnlPanel05.Controls.Add(btnGoToNextBlankLine);
            pnlPanel05.Dock = DockStyle.Fill;
            pnlPanel05.Location = new Point(4, 3);
            pnlPanel05.Margin = new Padding(4, 3, 4, 3);
            pnlPanel05.Name = "pnlPanel05";
            pnlPanel05.Size = new Size(506, 36);
            pnlPanel05.TabIndex = 0;
            // 
            // pnlPanel06
            // 
            pnlPanel06.BackColor = Color.Transparent;
            pnlPanel06.Controls.Add(lblFrom);
            pnlPanel06.Dock = DockStyle.Fill;
            pnlPanel06.Location = new Point(518, 3);
            pnlPanel06.Margin = new Padding(4, 3, 4, 3);
            pnlPanel06.Name = "pnlPanel06";
            pnlPanel06.Size = new Size(64, 36);
            pnlPanel06.TabIndex = 1;
            // 
            // pnlPanel07
            // 
            pnlPanel07.BackColor = Color.White;
            pnlPanel07.BorderStyle = BorderStyle.FixedSingle;
            pnlPanel07.Controls.Add(cmbTranslateFrom);
            pnlPanel07.Dock = DockStyle.Fill;
            pnlPanel07.Location = new Point(590, 3);
            pnlPanel07.Margin = new Padding(4, 3, 4, 3);
            pnlPanel07.Name = "pnlPanel07";
            pnlPanel07.Size = new Size(197, 36);
            pnlPanel07.TabIndex = 2;
            // 
            // pnlPanel08
            // 
            pnlPanel08.BackColor = Color.Transparent;
            pnlPanel08.Controls.Add(lblTo);
            pnlPanel08.Dock = DockStyle.Fill;
            pnlPanel08.Location = new Point(795, 3);
            pnlPanel08.Margin = new Padding(4, 3, 4, 3);
            pnlPanel08.Name = "pnlPanel08";
            pnlPanel08.Size = new Size(36, 36);
            pnlPanel08.TabIndex = 3;
            // 
            // tlpPanel3
            // 
            tlpPanel3.ColumnCount = 2;
            tlpPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 290F));
            tlpPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanel3.Controls.Add(trvProject, 0, 0);
            tlpPanel3.Controls.Add(tlpPanelRight, 1, 0);
            tlpPanel3.Dock = DockStyle.Fill;
            tlpPanel3.Location = new Point(16, 67);
            tlpPanel3.Margin = new Padding(4, 3, 4, 3);
            tlpPanel3.Name = "tlpPanel3";
            tlpPanel3.RowCount = 1;
            tlpPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanel3.Size = new Size(1040, 563);
            tlpPanel3.TabIndex = 3;
            // 
            // tlpPanelRight
            // 
            tlpPanelRight.ColumnCount = 1;
            tlpPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPanelRight.Controls.Add(txtLog, 0, 2);
            tlpPanelRight.Controls.Add(lstLanguageTags, 0, 0);
            tlpPanelRight.Controls.Add(txtCurrentText, 0, 1);
            tlpPanelRight.Dock = DockStyle.Fill;
            tlpPanelRight.Location = new Point(294, 3);
            tlpPanelRight.Margin = new Padding(4, 3, 4, 3);
            tlpPanelRight.Name = "tlpPanelRight";
            tlpPanelRight.RowCount = 3;
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 36F));
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            tlpPanelRight.Size = new Size(742, 557);
            tlpPanelRight.TabIndex = 14;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(250, 250, 250);
            txtLog.BorderStyle = BorderStyle.FixedSingle;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.Location = new Point(4, 436);
            txtLog.Margin = new Padding(4, 3, 4, 3);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(734, 118);
            txtLog.TabIndex = 13;
            // 
            // FrmTranslate
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1072, 683);
            Controls.Add(mnuMenu);
            Controls.Add(tlpPanel1);
            Controls.Add(stsStatus);
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(954, 398);
            Name = "FrmTranslate";
            ShowIcon = false;
            Text = "Translate";
            FormClosing += FrmTranslate_FormClosing;
            Load += FrmTranslate_Load;
            ResizeEnd += FrmTranslate_ResizeEnd;
            KeyDown += FrmTranslate_KeyDown;
            Resize += FrmTranslate_Resize;
            cmnuMenu.ResumeLayout(false);
            mnuMenu.ResumeLayout(false);
            mnuMenu.PerformLayout();
            stsStatus.ResumeLayout(false);
            stsStatus.PerformLayout();
            tlpPanel1.ResumeLayout(false);
            tlpPanel2.ResumeLayout(false);
            pnlPanel09.ResumeLayout(false);
            pnlPanel05.ResumeLayout(false);
            pnlPanel06.ResumeLayout(false);
            pnlPanel06.PerformLayout();
            pnlPanel07.ResumeLayout(false);
            pnlPanel08.ResumeLayout(false);
            pnlPanel08.PerformLayout();
            tlpPanel3.ResumeLayout(false);
            tlpPanelRight.ResumeLayout(false);
            tlpPanelRight.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstLanguageTags;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripDropDownButton mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.TextBox txtCurrentText;
        private System.Windows.Forms.TreeView trvProject;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripDropDownButton mnuEdit;
        private System.Windows.Forms.ToolStripDropDownButton mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsTranslateSelectedLines;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus1;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus2;
        private System.Windows.Forms.ContextMenuStrip cmnuMenu;
        private System.Windows.Forms.ToolStripMenuItem cmnuSetValueFromMaster;
        private System.Windows.Forms.ToolStripMenuItem cmnuTranslateGoogle;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ComboBox cmbTranslateTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.ComboBox cmbTranslateFrom;
        private System.Windows.Forms.Button btnGoToNextBlankLine;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFind;
        private System.Windows.Forms.ToolStripSeparator mnuFileSeparator1;
        private System.Windows.Forms.TableLayoutPanel tlpPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpPanel2;
        private System.Windows.Forms.Panel pnlPanel05;
        private System.Windows.Forms.Panel pnlPanel06;
        private System.Windows.Forms.Panel pnlPanel07;
        private System.Windows.Forms.Panel pnlPanel08;
        private System.Windows.Forms.Panel pnlPanel09;
        private System.Windows.Forms.TableLayoutPanel tlpPanel3;
        private System.Windows.Forms.TableLayoutPanel tlpPanelRight;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStripSeparator mnuToolsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsTranslationService;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsTranslationServiceGoogleWeb;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsTranslationServiceYandexWeb;
        private ColumnHeader clmTag;
        private ColumnHeader clmSource;
        private ColumnHeader clmTarget;
    }
}

