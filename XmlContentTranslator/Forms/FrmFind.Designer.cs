namespace XmlContentTranslator.Forms
{
    partial class FrmFind
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
            btnFind = new Button();
            lblFind = new Label();
            txtFind = new TextBox();
            rdbTags = new RadioButton();
            gpbSearchIn = new GroupBox();
            rdbText = new RadioButton();
            btnCancel = new Button();
            gpbSearchIn.SuspendLayout();
            SuspendLayout();
            // 
            // btnFind
            // 
            btnFind.Location = new Point(285, 29);
            btnFind.Margin = new Padding(4, 3, 4, 3);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(114, 27);
            btnFind.TabIndex = 3;
            btnFind.Text = "&Find";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // lblFind
            // 
            lblFind.AutoSize = true;
            lblFind.Location = new Point(14, 10);
            lblFind.Margin = new Padding(4, 0, 4, 0);
            lblFind.Name = "lblFind";
            lblFind.Size = new Size(59, 15);
            lblFind.TabIndex = 0;
            lblFind.Text = "Find what";
            // 
            // txtFind
            // 
            txtFind.Location = new Point(14, 29);
            txtFind.Margin = new Padding(4, 3, 4, 3);
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(263, 23);
            txtFind.TabIndex = 1;
            // 
            // rdbTags
            // 
            rdbTags.AutoSize = true;
            rdbTags.Location = new Point(22, 22);
            rdbTags.Margin = new Padding(4, 3, 4, 3);
            rdbTags.Name = "rdbTags";
            rdbTags.Size = new Size(48, 19);
            rdbTags.TabIndex = 0;
            rdbTags.TabStop = true;
            rdbTags.Text = "Tags";
            rdbTags.UseVisualStyleBackColor = true;
            // 
            // gpbSearchIn
            // 
            gpbSearchIn.Controls.Add(rdbText);
            gpbSearchIn.Controls.Add(rdbTags);
            gpbSearchIn.Location = new Point(18, 62);
            gpbSearchIn.Margin = new Padding(4, 3, 4, 3);
            gpbSearchIn.Name = "gpbSearchIn";
            gpbSearchIn.Padding = new Padding(4, 3, 4, 3);
            gpbSearchIn.Size = new Size(260, 59);
            gpbSearchIn.TabIndex = 2;
            gpbSearchIn.TabStop = false;
            gpbSearchIn.Text = "Search in";
            // 
            // rdbText
            // 
            rdbText.AutoSize = true;
            rdbText.Location = new Point(100, 22);
            rdbText.Margin = new Padding(4, 3, 4, 3);
            rdbText.Name = "rdbText";
            rdbText.Size = new Size(46, 19);
            rdbText.TabIndex = 1;
            rdbText.TabStop = true;
            rdbText.Text = "Text";
            rdbText.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(285, 62);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(114, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "C&ancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmFind
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 144);
            Controls.Add(btnCancel);
            Controls.Add(gpbSearchIn);
            Controls.Add(txtFind);
            Controls.Add(lblFind);
            Controls.Add(btnFind);
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFind";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Find";
            Shown += Find_Shown;
            KeyDown += Find_KeyDown;
            gpbSearchIn.ResumeLayout(false);
            gpbSearchIn.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.RadioButton rdbTags;
        private System.Windows.Forms.GroupBox gpbSearchIn;
        private System.Windows.Forms.RadioButton rdbText;
        private System.Windows.Forms.Button btnCancel;
    }
}