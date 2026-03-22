namespace XmlContentTranslator
{
    /// <summary>
    /// String utility helpers.
    /// <para>Вспомогательные строковые методы.</para>
    /// </summary>
    public static class StringUtils
    {
        #region Basic

        /// <summary>
        /// Trims text to at most 50 characters.
        /// <para>Обрезает текст максимум до 50 символов.</para>
        /// </summary>
        public static string Max50(string text)
        {
            if (text.Length > 50)
            {
                return text.Substring(0, 50).Trim() + "...";
            }

            return text;
        }

        #endregion Basic
    }
}
