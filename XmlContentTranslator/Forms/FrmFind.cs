using System.Windows.Forms;

namespace XmlContentTranslator.Forms
{
    /// <summary>
    /// Search dialog.
    /// <para>Диалог поиска.</para>
    /// </summary>
    public partial class FrmFind : Form
    {
        #region Variable

        /// <summary>
        /// Gets or sets a value indicating whether tags are searched.
        /// <para>Получает или задает признак поиска по тегам.</para>
        /// </summary>
        public bool SearchTags { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// <para>Получает или задает текст поиска.</para>
        /// </summary>
        public string SearchText { get; set; } = string.Empty;

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmFind()
        {
            InitializeComponent();
            rdbText.Checked = true;
            Translate();
        }

        #endregion Basic

        #region Control

        /// <summary>
        /// Translates the shell form.
        /// <para>Переводит форму оболочки.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// Handles key presses on the form.
        /// <para>Обрабатывает нажатия клавиш на форме.</para>
        /// </summary>
        private void Find_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(sender, e);
            }
        }

        /// <summary>
        /// Cancels the search.
        /// <para>Отменяет поиск.</para>
        /// </summary>
        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Confirms the search.
        /// <para>Подтверждает поиск.</para>
        /// </summary>
        private void btnFind_Click(object? sender, EventArgs e)
        {
            SearchTags = rdbTags.Checked;
            SearchText = txtFind.Text;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Prepares the form after showing.
        /// <para>Подготавливает форму после показа.</para>
        /// </summary>
        private void Find_Shown(object? sender, EventArgs e)
        {
            txtFind.Focus();
            txtFind.SelectAll();
        }

        #endregion Control
    }
}
