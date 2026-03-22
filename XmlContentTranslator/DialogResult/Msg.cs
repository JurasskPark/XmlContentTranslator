/// <summary>
/// Standard message dialog helper.
/// <para>Вспомогательный класс стандартных диалогов сообщений.</para>
/// </summary>
internal static class Msg
{
    #region Basic

    /// <summary>
    /// Shows an error message.
    /// <para>Показывает сообщение об ошибке.</para>
    /// </summary>
    internal static System.Windows.Forms.DialogResult ShowError(string text)
    {
        return MessageBox.Show(
            text,
            string.Empty,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    /// <summary>
    /// Shows an information message.
    /// <para>Показывает информационное сообщение.</para>
    /// </summary>
    public static void ShowMessage(string text)
    {
        MessageBox.Show(
            text,
            string.Empty,
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    /// <summary>
    /// Shows a question dialog.
    /// <para>Показывает диалог вопроса.</para>
    /// </summary>
    public static System.Windows.Forms.DialogResult ShowQuestion(string text)
    {
        return MessageBox.Show(
            text,
            string.Empty,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
    }

    #endregion Basic
}
