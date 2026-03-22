/// <summary>
/// Provides helpers for converting parameter collections.
/// <para>Предоставляет вспомогательные методы преобразования коллекций параметров.</para>
/// </summary>
public class ParameterDecryptor
{
    #region Basic

    /// <summary>
    /// Converts a parameter list to dictionary.
    /// </summary>
    public static Dictionary<string, string> Converter(List<string> parameters)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        for (int i = 0; i < parameters.Count; i++)
        {
            if (parameters[i] == null)
            {
                continue;
            }

            string[] parameter = parameters[i].Split('|');
            if (!result.ContainsKey(parameter[0]))
            {
                result.Add(parameter[0], parameter[1]);
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a parameter list to SQL-ready dictionary.
    /// </summary>
    public static Dictionary<string, string> ConverterSQL(List<string> parameters)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        for (int i = 0; i < parameters.Count; i++)
        {
            if (parameters[i] == null)
            {
                continue;
            }

            string[] parameter = parameters[i].Split('|');
            if (!result.ContainsKey(parameter[0]))
            {
                result.Add("AU_" + parameter[0], parameter[1]);
            }
        }

        return result;
    }

    /// <summary>
    /// Converts list to string.
    /// <para>Конвертирование списка в строку.</para>
    /// </summary>
    public static string ListToString(List<string> parameters)
    {
        string result = string.Empty;

        foreach (string item in parameters)
        {
            result += item.Replace("|", "=") + Environment.NewLine;
        }

        return result;
    }

    /// <summary>
    /// Converts parameter list to comma separated string.
    /// </summary>
    public static string ListToStringParametrs(List<string> parameters)
    {
        string result = string.Empty;

        for (int i = 0; i < parameters.Count; i++)
        {
            string comma = i == parameters.Count - 1 ? string.Empty : ",";
            result += $@"'{parameters[i]}'{comma} ";
        }

        return result;
    }

    /// <summary>
    /// Converts dictionary to string.
    /// </summary>
    public static string DictionaryToString(Dictionary<string, string> parameters)
    {
        string result = string.Empty;

        foreach (KeyValuePair<string, string> item in parameters)
        {
            result += $@"{item.Key}={item.Value}" + Environment.NewLine;
        }

        return result;
    }

    #endregion Basic
}
