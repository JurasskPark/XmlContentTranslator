namespace XmlContentTranslator.Forms
{
    partial class FrmAboutBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutBox));
            animationTimer = new System.Windows.Forms.Timer(components);
            headerPanel = new Panel();
            lblTitle = new Label();
            translationPanel = new Panel();
            lblVersion = new Label();
            lblDescription = new Label();
            lblCompany = new Label();
            lblStatus = new Label();
            btnClose = new Button();
            lblLanguageIndicator = new Label();
            lblAuthors = new Label();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // animationTimer
            // 
            animationTimer.Enabled = true;
            animationTimer.Interval = 50;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(70, 130, 200);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(700, 80);
            headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "XML Content Translator";
            // 
            // translationPanel
            // 
            translationPanel.BackColor = Color.FromArgb(30, 30, 40);
            translationPanel.Location = new Point(20, 100);
            translationPanel.Name = "translationPanel";
            translationPanel.Size = new Size(660, 173);
            translationPanel.TabIndex = 1;
            translationPanel.Paint += TranslationPanel_Paint;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 12F);
            lblVersion.ForeColor = Color.FromArgb(200, 200, 200);
            lblVersion.Location = new Point(20, 360);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(300, 25);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Version: ";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.FromArgb(220, 220, 220);
            lblDescription.Location = new Point(20, 400);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(526, 140);
            lblDescription.TabIndex = 3;
            lblDescription.Text = resources.GetString("lblDescription.Text");
            // 
            // lblCompany
            // 
            lblCompany.Font = new Font("Segoe UI", 9F);
            lblCompany.ForeColor = Color.FromArgb(150, 150, 150);
            lblCompany.Location = new Point(20, 551);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(526, 40);
            lblCompany.TabIndex = 4;
            lblCompany.Text = "© 2026 XML Content Translator \r\nAll rights reserved";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(0, 200, 0);
            lblStatus.Location = new Point(20, 288);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(250, 20);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "● Ready to work";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(70, 130, 200);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(580, 556);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 35);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            btnClose.MouseEnter += BtnClose_MouseEnter;
            btnClose.MouseLeave += BtnClose_MouseLeave;
            // 
            // lblLanguageIndicator
            // 
            lblLanguageIndicator.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLanguageIndicator.ForeColor = Color.FromArgb(70, 130, 200);
            lblLanguageIndicator.Location = new Point(480, 288);
            lblLanguageIndicator.Name = "lblLanguageIndicator";
            lblLanguageIndicator.Size = new Size(200, 30);
            lblLanguageIndicator.TabIndex = 7;
            lblLanguageIndicator.Text = "EN → RU";
            lblLanguageIndicator.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAuthors
            // 
            lblAuthors.Font = new Font("Segoe UI", 12F);
            lblAuthors.ForeColor = Color.FromArgb(200, 200, 200);
            lblAuthors.Location = new Point(20, 318);
            lblAuthors.Name = "lblAuthors";
            lblAuthors.Size = new Size(541, 25);
            lblAuthors.TabIndex = 8;
            lblAuthors.Text = "Authors: niksedk Nikolaj Olsson, JurasskPark Yuriy Pradius";
            // 
            // FrmAboutBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 50);
            ClientSize = new Size(700, 602);
            Controls.Add(lblAuthors);
            Controls.Add(lblLanguageIndicator);
            Controls.Add(headerPanel);
            Controls.Add(translationPanel);
            Controls.Add(lblVersion);
            Controls.Add(lblDescription);
            Controls.Add(lblCompany);
            Controls.Add(lblStatus);
            Controls.Add(btnClose);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAboutBox";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel translationPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLanguageIndicator;
        private Label lblAuthors;
    }
}