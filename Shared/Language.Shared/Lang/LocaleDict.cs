using System.Dynamic;

namespace Lang
{
    /// <summary>
    /// Represents a dictionary that contains phrases in a certain language.
    /// <para>Представляет словарь, который содержит фразы на определённом языке.</para>
    /// </summary>
    public class LocaleDict : DynamicObject
    {
        #region Property

        /// <summary>
        /// Gets the dictionary key.
        /// <para>Получает ключ словаря.</para>
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the phrase associated with the specified key or an empty phrase if the key is not found.
        /// <para>Получает фразу по ключу или пустую фразу, если ключ не найден.</para>
        /// </summary>
        public string this[string key] => GetPhrase(key);

        /// <summary>
        /// Gets the phrases contained in the dictionary.
        /// <para>Получает фразы, содержащиеся в словаре.</para>
        /// </summary>
        public Dictionary<string, string> Phrases { get; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public LocaleDict(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Phrases = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the phrase associated with the specified key.
        /// <para>Получает фразу, связанную с указанным ключом.</para>
        /// </summary>
        public string GetPhrase(string key)
        {
            return Phrases.TryGetValue(key, out string? phrase) ? phrase : "[" + key + "]";
        }

        /// <summary>
        /// Provides the implementation for operations that get member values.
        /// <para>Предоставляет реализацию операций получения значений членов.</para>
        /// </summary>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetPhrase(binder.Name);
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that set member values.
        /// <para>Предоставляет реализацию операций установки значений членов.</para>
        /// </summary>
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            Phrases[binder.Name] = value?.ToString() ?? string.Empty;
            return true;
        }

        #endregion Basic
    }
}
