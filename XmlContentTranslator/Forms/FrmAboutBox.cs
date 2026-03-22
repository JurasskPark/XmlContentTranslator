using System.Drawing.Drawing2D;
using System.Reflection;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// About dialog with animated translation preview.
    /// <para>Диалог О программе с анимированным предпросмотром перевода.</para>
    /// </summary>
    public partial class FrmAboutBox : Form
    {
        #region Variable

        private int animationOffset;
        private bool directionRight = true;
        private float pulseValue;
        private bool pulseIncreasing = true;
        private int languageIndex = 1;
        private readonly Random random = new Random();

        private readonly string[][] translatedTags =
        {
            new[] { "title", "description", "content", "button", "label", "message" },
            new[] { "заголовок", "описание", "содержание", "кнопка", "метка", "сообщение" },
            new[] { "标题", "描述", "内容", "按钮", "标签", "消息" },
            new[] { "título", "descripción", "contenido", "botón", "etiqueta", "mensaje" },
            new[] { "titel", "beschreibung", "inhalt", "schaltfläche", "bezeichnung", "nachricht" },
            new[] { "titre", "description", "contenu", "bouton", "étiquette", "message" },
            new[] { "タイトル", "説明", "内容", "ボタン", "ラベル", "メッセージ" },
            new[] { "titolo", "descrizione", "contenuto", "pulsante", "etichetta", "messaggio" }
        };

        private readonly string[] languageNames = { "EN", "RU", "CN", "ES", "DE", "FR", "JP", "IT" };
        private readonly Point[] wordPositions = new Point[6];

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmAboutBox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            animationTimer.Tick += AnimationTimer_Tick;
            Paint += AboutForm_Paint;

            LoadAssemblyInfo();
            Translate();
        }

        /// <summary>
        /// Loads assembly information into the form.
        /// <para>Загружает информацию о сборке в форму.</para>
        /// </summary>
        private void LoadAssemblyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version? versionAttribute = assembly.GetName().Version;
            AssemblyCompanyAttribute? companyAttribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            AssemblyCopyrightAttribute? copyrightAttribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            DateTime buildDateAttribute = GetLinkerTime(assembly);

            string version = lblVersion.Text;
            string versionString = versionAttribute?.ToString() ?? "1.0.0.0";
            string buildDateString = buildDateAttribute.ToString("yyyy-MM-dd HH:mm");
            lblVersion.Text = $"{version} {versionString} ({buildDateString})";

            string company = lblCompany.Text;
            lblCompany.Text = copyrightAttribute != null && !string.IsNullOrEmpty(copyrightAttribute.Copyright)
                ? string.Format(company, copyrightAttribute.Copyright)
                : company;

            string authors = lblAuthors.Text;
            lblAuthors.Text = companyAttribute != null && !string.IsNullOrEmpty(companyAttribute.Company)
                ? string.Format(authors, companyAttribute.Company)
                : authors;

            string description = lblDescription.Text;
            lblDescription.Text = description;
        }

        /// <summary>
        /// Gets the build timestamp of the assembly.
        /// <para>Получает время сборки приложения.</para>
        /// </summary>
        private DateTime GetLinkerTime(Assembly assembly)
        {
            try
            {
                return File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location);
            }
            catch
            {
                return File.GetLastWriteTime(assembly.Location);
            }
        }

        /// <summary>
        /// Updates language word positions.
        /// <para>Обновляет позиции слов для анимации.</para>
        /// </summary>
        private void UpdateWordPositions()
        {
            wordPositions[0] = new Point(80, 40);
            wordPositions[1] = new Point(80, 70);
            wordPositions[2] = new Point(80, 100);
            wordPositions[3] = new Point(80, 130);
            wordPositions[4] = new Point(80, 160);
            wordPositions[5] = new Point(80, 190);
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
        /// Converts HSV color to RGB.
        /// <para>Преобразует цвет HSV в RGB.</para>
        /// </summary>
        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
            {
                return Color.FromArgb(255, v, t, p);
            }

            if (hi == 1)
            {
                return Color.FromArgb(255, q, v, p);
            }

            if (hi == 2)
            {
                return Color.FromArgb(255, p, v, t);
            }

            if (hi == 3)
            {
                return Color.FromArgb(255, p, q, v);
            }

            if (hi == 4)
            {
                return Color.FromArgb(255, t, p, v);
            }

            return Color.FromArgb(255, v, p, q);
        }

        #endregion Basic

        #region Control

        #region Timer

        /// <summary>
        /// Handles animation timer ticks.
        /// <para>Обрабатывает тики таймера анимации.</para>
        /// </summary>
        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            if (directionRight)
            {
                animationOffset += 1;
                if (animationOffset >= 20)
                {
                    directionRight = false;
                }
            }
            else
            {
                animationOffset -= 1;
                if (animationOffset <= 0)
                {
                    directionRight = true;
                }
            }

            if (pulseIncreasing)
            {
                pulseValue += 0.02f;
                if (pulseValue >= 1)
                {
                    pulseIncreasing = false;
                }
            }
            else
            {
                pulseValue -= 0.02f;
                if (pulseValue <= 0)
                {
                    pulseIncreasing = true;
                }
            }

            if (animationOffset % 50 == 0)
            {
                languageIndex = (languageIndex + 1) % translatedTags.Length;
                if (languageIndex == 0)
                {
                    languageIndex = 1;
                }

                lblLanguageIndicator.Text = $"{languageNames[0]} -> {languageNames[languageIndex]}";
                int hue = (languageIndex * 45) % 360;
                lblLanguageIndicator.ForeColor = ColorFromHSV(hue, 0.8, 0.9);
            }

            UpdateWordPositions();
            translationPanel.Invalidate();
        }

        #endregion Timer

        #region Paint

        /// <summary>
        /// Paints the animated translation preview panel.
        /// <para>Рисует панель предпросмотра перевода.</para>
        /// </summary>
        private void TranslationPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(30, 30, 40)))
            {
                g.FillRectangle(brush, translationPanel.ClientRectangle);
            }

            using (Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(70, 130, 200)))
                {
                    g.DrawString("ENGLISH", headerFont, brush, 40, 10);
                }

                using (SolidBrush brush = new SolidBrush(lblLanguageIndicator.ForeColor))
                {
                    g.DrawString(languageNames[languageIndex].ToUpperInvariant(), headerFont, brush, 360, 10);
                }
            }

            using (Pen pen = new Pen(Color.FromArgb(70, 130, 200), 2))
            {
                pen.DashStyle = DashStyle.Dash;
                g.DrawLine(pen, 250, 30, 250, 210);
            }

            using (Pen pen = new Pen(Color.FromArgb(150, 70, 130, 200), 2))
            {
                pen.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(pen, 200, 20, 300, 20);
            }

            using (Font wordFont = new Font("Segoe UI", 11, FontStyle.Regular))
            using (SolidBrush englishBrush = new SolidBrush(Color.White))
            {
                for (int i = 0; i < 6; i++)
                {
                    g.DrawString("<" + translatedTags[0][i] + ">", wordFont, englishBrush, wordPositions[i].X - 30, wordPositions[i].Y);
                }
            }

            using (Font wordFont = new Font("Segoe UI", 11, FontStyle.Regular))
            using (SolidBrush translatedBrush = new SolidBrush(lblLanguageIndicator.ForeColor))
            {
                for (int i = 0; i < 6; i++)
                {
                    int xOffset = (int)(Math.Sin(DateTime.Now.Millisecond / 200f + i) * 5);
                    g.DrawString("<" + translatedTags[languageIndex][i] + ">", wordFont, translatedBrush, 360 + xOffset, wordPositions[i].Y);
                }
            }

            using (Pen pen = new Pen(Color.FromArgb(100, 70, 130, 200), 1))
            {
                pen.DashStyle = DashStyle.Dot;

                for (int i = 0; i < 6; i++)
                {
                    int startX = 240;
                    int endX = 260;
                    if (i == (int)(pulseValue * 5))
                    {
                        using (Pen pulsePen = new Pen(Color.FromArgb(200, 70, 130, 200), 2))
                        {
                            pulsePen.DashStyle = DashStyle.Solid;
                            g.DrawLine(pulsePen, startX, wordPositions[i].Y + 5, endX, wordPositions[i].Y + 5);
                        }
                    }
                    else
                    {
                        g.DrawLine(pen, startX, wordPositions[i].Y + 5, endX, wordPositions[i].Y + 5);
                    }
                }
            }

            using (Brush dotBrush = new SolidBrush(Color.FromArgb(70, 130, 200)))
            {
                for (int i = 0; i < 6; i++)
                {
                    g.FillEllipse(dotBrush, 238, wordPositions[i].Y + 2, 6, 6);
                    g.FillEllipse(dotBrush, 258, wordPositions[i].Y + 2, 6, 6);
                }
            }
        }

        /// <summary>
        /// Paints the form background.
        /// <para>Рисует фон формы.</para>
        /// </summary>
        private void AboutForm_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(20, 70, 130, 200), 1))
            {
                pen.DashStyle = DashStyle.Dot;

                for (int i = 0; i < Width; i += 40)
                {
                    g.DrawLine(pen, i, 0, i, Height);
                }

                for (int i = 0; i < Height; i += 40)
                {
                    g.DrawLine(pen, 0, i, Width, i);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                float y = 300 + i * 35;
                float x = 500 + (float)Math.Sin(DateTime.Now.Millisecond / 300f + i) * 10;

                using (Brush brush = new SolidBrush(Color.FromArgb(30, 70, 130, 200)))
                using (Font font = new Font("Segoe UI", 7, FontStyle.Italic))
                {
                    g.DrawString("<translation>", font, brush, x, y);
                }
            }
        }

        #endregion Paint

        #region Button

        /// <summary>
        /// Closes the dialog.
        /// <para>Закрывает диалог.</para>
        /// </summary>
        private void BtnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles mouse enter on close button.
        /// <para>Обрабатывает наведение на кнопку закрытия.</para>
        /// </summary>
        private void BtnClose_MouseEnter(object? sender, EventArgs e)
        {
            btnClose.BackColor = Color.FromArgb(100, 150, 220);
        }

        /// <summary>
        /// Handles mouse leave on close button.
        /// <para>Обрабатывает уход курсора с кнопки закрытия.</para>
        /// </summary>
        private void BtnClose_MouseLeave(object? sender, EventArgs e)
        {
            btnClose.BackColor = Color.FromArgb(70, 130, 200);
        }

        #endregion Button

        #endregion Control
    }
}
