namespace XmlContentTranslator.Forms
{
    partial class FrmStart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStart));
            stsStrip = new StatusStrip();
            mnuMenu = new MenuStrip();
            tolOperation = new ToolStripMenuItem();
            tolGenerateXML = new ToolStripMenuItem();
            tolGenerateCode = new ToolStripMenuItem();
            tolTranslate = new ToolStripMenuItem();
            tolSettings = new ToolStripMenuItem();
            tolWindows = new ToolStripMenuItem();
            tolSeparator2 = new ToolStripSeparator();
            tolCascade = new ToolStripMenuItem();
            tolHorizontal = new ToolStripMenuItem();
            tolVertical = new ToolStripMenuItem();
            tolSeparator1 = new ToolStripSeparator();
            tolCloseAll = new ToolStripMenuItem();
            tolAbout = new ToolStripMenuItem();
            tolLang = new ToolStripMenuItem();
            imgList = new ImageList(components);
            toolStripHost = new ToolStrip();
            mnuMenu.SuspendLayout();
            SuspendLayout();
            // 
            // stsStrip
            // 
            stsStrip.ImageScalingSize = new Size(24, 24);
            stsStrip.Location = new Point(0, 428);
            stsStrip.Name = "stsStrip";
            stsStrip.Size = new Size(931, 22);
            stsStrip.TabIndex = 0;
            // 
            // mnuMenu
            // 
            mnuMenu.Items.AddRange(new ToolStripItem[] { tolOperation, tolSettings, tolWindows, tolAbout, tolLang });
            mnuMenu.Location = new Point(0, 0);
            mnuMenu.Name = "mnuMenu";
            mnuMenu.Size = new Size(931, 24);
            mnuMenu.TabIndex = 1;
            // 
            // tolOperation
            // 
            tolOperation.DropDownItems.AddRange(new ToolStripItem[] { tolGenerateXML, tolGenerateCode, tolTranslate });
            tolOperation.Image = (Image)resources.GetObject("tolOperation.Image");
            tolOperation.Name = "tolOperation";
            tolOperation.Size = new Size(88, 20);
            tolOperation.Text = "Operation";
            // 
            // tolGenerateXML
            // 
            tolGenerateXML.Image = (Image)resources.GetObject("tolGenerateXML.Image");
            tolGenerateXML.Name = "tolGenerateXML";
            tolGenerateXML.Size = new Size(180, 22);
            tolGenerateXML.Text = "Generate XML";
            tolGenerateXML.Click += tolGenerateXML_Click;
            // 
            // tolGenerateCode
            // 
            tolGenerateCode.Image = (Image)resources.GetObject("tolGenerateCode.Image");
            tolGenerateCode.Name = "tolGenerateCode";
            tolGenerateCode.Size = new Size(180, 22);
            tolGenerateCode.Text = "Generate Code";
            tolGenerateCode.Click += tolGenerateCode_Click;
            // 
            // tolTranslate
            // 
            tolTranslate.Image = (Image)resources.GetObject("tolTranslate.Image");
            tolTranslate.Name = "tolTranslate";
            tolTranslate.Size = new Size(180, 22);
            tolTranslate.Text = "Translate";
            tolTranslate.Click += tolTranslate_Click;
            // 
            // tolSettings
            // 
            tolSettings.Image = (Image)resources.GetObject("tolSettings.Image");
            tolSettings.Name = "tolSettings";
            tolSettings.Size = new Size(77, 20);
            tolSettings.Text = "Settings";
            tolSettings.Click += tolSettings_Click;
            // 
            // tolWindows
            // 
            tolWindows.DropDownItems.AddRange(new ToolStripItem[] { tolSeparator2, tolCascade, tolHorizontal, tolVertical, tolSeparator1, tolCloseAll });
            tolWindows.Image = (Image)resources.GetObject("tolWindows.Image");
            tolWindows.Name = "tolWindows";
            tolWindows.Size = new Size(84, 20);
            tolWindows.Text = "Windows";
            // 
            // tolSeparator2
            // 
            tolSeparator2.Name = "tolSeparator2";
            tolSeparator2.Size = new Size(126, 6);
            // 
            // tolCascade
            // 
            tolCascade.Image = (Image)resources.GetObject("tolCascade.Image");
            tolCascade.Name = "tolCascade";
            tolCascade.Size = new Size(129, 22);
            tolCascade.Text = "Cascade";
            tolCascade.Click += tolCascade_Click;
            // 
            // tolHorizontal
            // 
            tolHorizontal.Image = (Image)resources.GetObject("tolHorizontal.Image");
            tolHorizontal.Name = "tolHorizontal";
            tolHorizontal.Size = new Size(129, 22);
            tolHorizontal.Text = "Horizontal";
            tolHorizontal.Click += tolHorizontal_Click;
            // 
            // tolVertical
            // 
            tolVertical.Image = (Image)resources.GetObject("tolVertical.Image");
            tolVertical.Name = "tolVertical";
            tolVertical.Size = new Size(129, 22);
            tolVertical.Text = "Vertical";
            tolVertical.Click += tolVertical_Click;
            // 
            // tolSeparator1
            // 
            tolSeparator1.Name = "tolSeparator1";
            tolSeparator1.Size = new Size(126, 6);
            // 
            // tolCloseAll
            // 
            tolCloseAll.Name = "tolCloseAll";
            tolCloseAll.Size = new Size(129, 22);
            tolCloseAll.Text = "Close All";
            tolCloseAll.Click += tolCloseAll_Click;
            // 
            // tolAbout
            // 
            tolAbout.Alignment = ToolStripItemAlignment.Right;
            tolAbout.Image = (Image)resources.GetObject("tolAbout.Image");
            tolAbout.Name = "tolAbout";
            tolAbout.Size = new Size(68, 20);
            tolAbout.Text = "About";
            tolAbout.Click += tolAbout_Click;
            // 
            // tolLang
            // 
            tolLang.Alignment = ToolStripItemAlignment.Right;
            tolLang.Image = (Image)resources.GetObject("tolLang.Image");
            tolLang.Name = "tolLang";
            tolLang.Size = new Size(87, 20);
            tolLang.Text = "Language";
            tolLang.Click += tolLang_Click;
            // 
            // imgList
            // 
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            imgList.ImageStream = (ImageListStreamer)resources.GetObject("imgList.ImageStream");
            imgList.TransparentColor = Color.Transparent;
            imgList.Images.SetKeyName(0, "flag_russia.png");
            imgList.Images.SetKeyName(1, "flag_usa.png");
            // 
            // toolStripHost
            // 
            toolStripHost.GripStyle = ToolStripGripStyle.Hidden;
            toolStripHost.Location = new Point(0, 24);
            toolStripHost.Name = "toolStripHost";
            toolStripHost.Size = new Size(931, 25);
            toolStripHost.TabIndex = 2;
            // 
            // FrmStart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 450);
            Controls.Add(stsStrip);
            Controls.Add(toolStripHost);
            Controls.Add(mnuMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            KeyPreview = true;
            MainMenuStrip = mnuMenu;
            Name = "FrmStart";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "XML Content Translator 6.0.0.0";
            WindowState = FormWindowState.Maximized;
            mnuMenu.ResumeLayout(false);
            mnuMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip stsStrip;
        private MenuStrip mnuMenu;
        private ToolStripMenuItem tolSettings;
        private ToolStripMenuItem tolWindows;
        private ToolStripMenuItem tolCascade;
        private ToolStripMenuItem tolHorizontal;
        private ToolStripMenuItem tolVertical;
        private ImageList imgList;
        private ToolStripMenuItem tolLang;
        private ToolStripMenuItem tolCloseAll;
        private ToolStripSeparator tolSeparator1;
        private ToolStripSeparator tolSeparator2;
        private ToolStripMenuItem tolAbout;
        private ToolStripMenuItem tolGenerateXML;
        private ToolStripMenuItem tolTranslate;
        private ToolStripMenuItem tolGenerateCode;
        private ToolStripMenuItem tolOperation;
        private ToolStrip toolStripHost;
    }
}