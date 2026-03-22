namespace XmlContentTranslator.Forms
{
    partial class FrmSettings
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTranslateFrom = new Label();
            cmbTranslateFrom = new ComboBox();
            lblTranslateTo = new Label();
            cmbTranslateTo = new ComboBox();
            lblTranslationService = new Label();
            cmbTranslationService = new ComboBox();
            lblTranslationServices = new Label();
            txtTranslationServices = new TextBox();
            panelButtons = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            tableLayoutPanel1.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblTranslateFrom, 0, 0);
            tableLayoutPanel1.Controls.Add(cmbTranslateFrom, 1, 0);
            tableLayoutPanel1.Controls.Add(lblTranslateTo, 0, 1);
            tableLayoutPanel1.Controls.Add(cmbTranslateTo, 1, 1);
            tableLayoutPanel1.Controls.Add(lblTranslationService, 0, 2);
            tableLayoutPanel1.Controls.Add(cmbTranslationService, 1, 2);
            tableLayoutPanel1.Controls.Add(lblTranslationServices, 0, 3);
            tableLayoutPanel1.Controls.Add(txtTranslationServices, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(16, 16);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 4, 0, 0);
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tableLayoutPanel1.Size = new Size(428, 202);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTranslateFrom
            // 
            lblTranslateFrom.Anchor = AnchorStyles.Left;
            lblTranslateFrom.AutoSize = true;
            lblTranslateFrom.Location = new Point(0, 17);
            lblTranslateFrom.Margin = new Padding(0);
            lblTranslateFrom.Name = "lblTranslateFrom";
            lblTranslateFrom.Size = new Size(35, 15);
            lblTranslateFrom.TabIndex = 17;
            lblTranslateFrom.Text = "From";
            // 
            // cmbTranslateFrom
            // 
            cmbTranslateFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTranslateFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTranslateFrom.FormattingEnabled = true;
            cmbTranslateFrom.Location = new Point(174, 14);
            cmbTranslateFrom.Margin = new Padding(4, 5, 0, 3);
            cmbTranslateFrom.Name = "cmbTranslateFrom";
            cmbTranslateFrom.Size = new Size(254, 23);
            cmbTranslateFrom.TabIndex = 0;
            cmbTranslateFrom.SelectedIndexChanged += control_Changed;
            // 
            // lblTranslateTo
            // 
            lblTranslateTo.Anchor = AnchorStyles.Left;
            lblTranslateTo.AutoSize = true;
            lblTranslateTo.Location = new Point(0, 59);
            lblTranslateTo.Margin = new Padding(0);
            lblTranslateTo.Name = "lblTranslateTo";
            lblTranslateTo.Size = new Size(19, 15);
            lblTranslateTo.TabIndex = 19;
            lblTranslateTo.Text = "To";
            // 
            // cmbTranslateTo
            // 
            cmbTranslateTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTranslateTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTranslateTo.FormattingEnabled = true;
            cmbTranslateTo.Location = new Point(174, 56);
            cmbTranslateTo.Margin = new Padding(4, 5, 0, 3);
            cmbTranslateTo.Name = "cmbTranslateTo";
            cmbTranslateTo.Size = new Size(254, 23);
            cmbTranslateTo.TabIndex = 1;
            cmbTranslateTo.SelectedIndexChanged += control_Changed;
            // 
            // lblTranslationService
            // 
            lblTranslationService.Anchor = AnchorStyles.Left;
            lblTranslationService.AutoSize = true;
            lblTranslationService.Location = new Point(0, 101);
            lblTranslationService.Margin = new Padding(0);
            lblTranslationService.Name = "lblTranslationService";
            lblTranslationService.Size = new Size(86, 15);
            lblTranslationService.TabIndex = 22;
            lblTranslationService.Text = "Service (active)";
            // 
            // cmbTranslationService
            // 
            cmbTranslationService.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbTranslationService.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTranslationService.FormattingEnabled = true;
            cmbTranslationService.Location = new Point(174, 98);
            cmbTranslationService.Margin = new Padding(4, 5, 0, 3);
            cmbTranslationService.Name = "cmbTranslationService";
            cmbTranslationService.Size = new Size(254, 23);
            cmbTranslationService.TabIndex = 2;
            cmbTranslationService.SelectedIndexChanged += control_Changed;
            // 
            // lblTranslationServices
            // 
            lblTranslationServices.Anchor = AnchorStyles.Left;
            lblTranslationServices.AutoSize = true;
            lblTranslationServices.Location = new Point(0, 159);
            lblTranslationServices.Margin = new Padding(0);
            lblTranslationServices.Name = "lblTranslationServices";
            lblTranslationServices.Size = new Size(49, 15);
            lblTranslationServices.TabIndex = 24;
            lblTranslationServices.Text = "Services";
            // 
            // txtTranslationServices
            // 
            txtTranslationServices.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTranslationServices.Location = new Point(174, 137);
            txtTranslationServices.Margin = new Padding(4, 5, 0, 3);
            txtTranslationServices.Multiline = true;
            txtTranslationServices.Name = "txtTranslationServices";
            txtTranslationServices.ScrollBars = ScrollBars.Vertical;
            txtTranslationServices.Size = new Size(254, 62);
            txtTranslationServices.TabIndex = 3;
            txtTranslationServices.TextChanged += control_Changed;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(16, 229);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(428, 48);
            panelButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(314, 10);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(114, 27);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Location = new Point(192, 10);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(114, 27);
            btnSave.TabIndex = 20;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(460, 293);
            Controls.Add(panelButtons);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettings";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Load += FrmSettings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbTranslateTo;
        private Label lblTranslateFrom;
        private ComboBox cmbTranslateFrom;
        private Label lblTranslateTo;
        private Button btnCancel;
        private Button btnSave;
        private Label lblTranslationService;
        private ComboBox cmbTranslationService;
        private Label lblTranslationServices;
        private TextBox txtTranslationServices;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelButtons;
    }
}
