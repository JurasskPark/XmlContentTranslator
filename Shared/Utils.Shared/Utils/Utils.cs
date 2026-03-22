using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Assistant
{
    /// <summary>
    /// The class provides helper methods for the entire software package.
    /// <para>Класс, предоставляющий вспомогательные методы для всего программного комплекса.</para>
    /// </summary>
    public static partial class Utils
    {
        #region Variable

        /// <summary>
        /// Regex for validating GUID strings.
        /// </summary>
        private static readonly Regex ReGuid = new Regex(
            @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",
            RegexOptions.Compiled);

        /// <summary>
        /// Generates cryptographically strong random numbers.
        /// </summary>
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        /// <summary>
        /// The default secret key.
        /// </summary>
        private static readonly byte[] DefaultSecretKey = new byte[SecretKeySize]
        {
            0x0A, 0xBA, 0x06, 0xBC, 0x1A, 0x5D, 0x44, 0x3E, 0x5A, 0xE8, 0x46, 0x7F, 0xB8, 0x85, 0x49, 0xF6,
            0xE9, 0xCC, 0x90, 0xF0, 0x80, 0x45, 0x33, 0xFC, 0x2A, 0x67, 0xD9, 0xBA, 0x00, 0xCE, 0xC7, 0x8A,
        };

        /// <summary>
        /// The default initialization vector.
        /// </summary>
        private static readonly byte[] DefaultIV = new byte[IVSize]
        {
            0xA5, 0x5C, 0x5A, 0x7B, 0x40, 0xD4, 0x2D, 0x33, 0xA4, 0x6F, 0xF7, 0x84, 0x94, 0x1C, 0x47, 0x85,
        };

        /// <summary>
        /// The password hash salt.
        /// </summary>
        private const string PasswordSalt = "aEGnwn3CCSFdth7kNXc3";

        #endregion Variable

        #region Property

        /// <summary>
        /// The date format used for naming partitions.
        /// </summary>
        public const string PartitionDateFormat = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// The date-time format used by SQL-related helpers.
        /// </summary>
        public const string PartitionDateTime = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// The secret key size in bytes.
        /// </summary>
        public const int SecretKeySize = 32;

        /// <summary>
        /// The initialization vector size in bytes.
        /// </summary>
        public const int IVSize = 16;

        #endregion Property

        #region Support class

        /// <summary>
        /// Schedule mode.
        /// </summary>
        public enum ScheduleMode
        {
            None = 0,
            Secondly = 1,
            Minutly = 2,
            Hourly = 3,
            Daily = 4,
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct FloatUnion
        {
            [FieldOffset(0)]
            public float Value;

            [FieldOffset(0)]
            public int Binary;
        }

        #endregion Support class

        #region Basic

        /// <summary>
        /// Converts string GUID to <see cref="Guid"/>.
        /// </summary>
        public static Guid StringToGuid(string id)
        {
            if (id == null || id.Length != 36)
            {
                return Guid.Empty;
            }

            return ReGuid.IsMatch(id) ? new Guid(id) : Guid.Empty;
        }

        /// <summary>
        /// Creates a GUID string from a new GUID byte array.
        /// </summary>
        public static string CreateGuid()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in bytes)
            {
                stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Validates a GUID string.
        /// </summary>
        public static bool ValidateGuid(string id)
        {
            return ReGuid.IsMatch(id);
        }

        /// <summary>
        /// Converts an object to string. Returns an empty string for unsupported empty values.
        /// </summary>
        public static string NullToString(object? value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                Type type = value.GetType();

                if (type == typeof(DateTime))
                {
                    if ((DateTime)value == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    DateTime date = (DateTime)value;
                    return date.Millisecond > 0
                        ? date.ToString("yyyy-MM-dd HH:mm:ss.fffff")
                        : date.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)value).Name;
                }

                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)value).FullName ?? string.Empty;
                }

                if (type == typeof(byte[]))
                {
                    byte[] bytes = (byte[])value;
                    StringBuilder buffer = new StringBuilder(bytes.Length * 3);

                    foreach (byte character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append('.');
                    }

                    return buffer.ToString();
                }

                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;

                    foreach (object element in (Array)value)
                    {
                        result += string.Format("[{0}]", index++) + (element?.ToString() ?? string.Empty) + Environment.NewLine;
                    }

                    return $"{type.GetElementType()?.Name}[{((Array)value).Length}]{result}";
                }

                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;

                    foreach (object element in (Array)value)
                    {
                        result += string.Format("[{0}]", index++) + (element?.ToString() ?? string.Empty) + Environment.NewLine;
                    }

                    return $"Object[{((Array)value).Length}]{result}";
                }

                if (type.FullName == "System.Object")
                {
                    return string.Empty;
                }

                return value.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Calculates the next trigger time.
        /// </summary>
        public static DateTime CalculateTriggerTime(DateTime currentTime, TimeSpan processTime, ScheduleMode scheduleMode)
        {
            DateTime nextTime = currentTime;

            switch (scheduleMode)
            {
                case ScheduleMode.Secondly:
                    nextTime = nextTime.Add(new TimeSpan(0, 0, 0, processTime.Seconds, nextTime.Millisecond));
                    break;
                case ScheduleMode.Minutly:
                    nextTime = nextTime.Subtract(new TimeSpan(0, 0, 0, nextTime.Second, nextTime.Millisecond));
                    nextTime = nextTime.Add(new TimeSpan(0, 0, processTime.Minutes, processTime.Seconds, 0));
                    break;
                case ScheduleMode.Hourly:
                    nextTime = nextTime.Subtract(new TimeSpan(0, 0, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                    nextTime = nextTime.Add(new TimeSpan(0, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    break;
                case ScheduleMode.Daily:
                    nextTime = nextTime.Subtract(new TimeSpan(0, nextTime.Hour, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                    nextTime = nextTime.Add(new TimeSpan(processTime.Days, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    break;
            }

            return nextTime;
        }

        /// <summary>
        /// Converts the specified date and time to local time.
        /// </summary>
        public static DateTime UtcToLocalTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }

        /// <summary>
        /// Converts local date and time to universal time.
        /// </summary>
        public static DateTime LocalToUtcTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }

        /// <summary>
        /// Converts Unix timestamp to local date and time.
        /// </summary>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        /// <summary>
        /// Converts OLE Automation date value to <see cref="DateTime"/>.
        /// </summary>
        public static DateTime FromOADate(double unixTimeStamp)
        {
            return new DateTime(DoubleDateToTicks(unixTimeStamp), DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Converts OLE Automation date value to ticks.
        /// </summary>
        internal static long DoubleDateToTicks(double value)
        {
            if (value >= 2958466.0 || value <= -657435.0)
            {
                throw new ArgumentException("Not a valid value");
            }

            long num1 = (long)(value * 86400000.0 + (value >= 0.0 ? 0.5 : -0.5));
            if (num1 < 0L)
            {
                num1 -= num1 % 86400000L * 2L;
            }

            long num2 = num1 + 59926435200000L;
            if (num2 < 0L || num2 >= 315537897600000L)
            {
                throw new ArgumentException("Not a valid value");
            }

            return num2 * 10000L;
        }

        /// <summary>
        /// Converts date to SQL string.
        /// </summary>
        public static string DatetimeSql(DateTime date)
        {
            DateTime checkDatetime = DateTime.ParseExact(
                "2000-01-01 00:00:00",
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture);

            return date < checkDatetime
                ? checkDatetime.ToString("yyyy-MM-ddTHH:mm:ss")
                : date.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Converts a data table to a list of string values.
        /// </summary>
        public static List<string> ConvertDataTable(DataTable dt)
        {
            List<string> data = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string item = GetItem(row);
                data.Add(item);
            }

            return data;
        }

        /// <summary>
        /// Converts a <see cref="DataTable"/> to a typed list.
        /// </summary>
        public static List<T> DataTableToList<T>(this DataTable table)
            where T : new()
        {
            List<T> list = new List<T>();
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType,
            }).ToList();

            foreach (DataRow row in table.Rows.Cast<DataRow>())
            {
                T obj = new T();
                foreach (var typeProperty in typeProperties)
                {
                    object value = row[typeProperty.PropertyInfo.Name];
                    object? safeValue = value == null || DBNull.Value.Equals(value)
                        ? null
                        : Convert.ChangeType(value, typeProperty.Type);

                    typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Converts the specified date to a partition string.
        /// </summary>
        public static bool ParsePartitionString(DateTime dateTime, out string result)
        {
            try
            {
                result = dateTime.ToString(PartitionDateFormat);
                return true;
            }
            catch
            {
                result = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Parses the partition date string.
        /// </summary>
        public static bool ParsePartitionDate(string s, out DateTime result)
        {
            return DateTime.TryParseExact(s, PartitionDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        /// <summary>
        /// Converts a string value to a typed object.
        /// </summary>
        public static object? ConvertStringToObject(string typeData, string valueData)
        {
            object? obj = null;

            switch (typeData)
            {
                case "Single": obj = Convert.ToSingle(valueData); break;
                case "Double": obj = Convert.ToDouble(valueData); break;
                case "Boolean": obj = Convert.ToBoolean(valueData); break;
                case "SByte": obj = Convert.ToSByte(valueData); break;
                case "Byte": obj = Convert.ToByte(valueData); break;
                case "Int16": obj = Convert.ToInt16(valueData); break;
                case "UInt16": obj = Convert.ToUInt16(valueData); break;
                case "Int32": obj = Convert.ToInt32(valueData); break;
                case "UInt32": obj = Convert.ToUInt32(valueData); break;
                case "Int64": obj = Convert.ToInt64(valueData); break;
                case "UInt64": obj = Convert.ToUInt64(valueData); break;
                case "String": obj = Convert.ToString(valueData); break;
                case "DateTime": obj = Convert.ToDateTime(valueData); break;
                case "ArrayList": obj = valueData.ToArray(); break;
            }

            return obj;
        }

        /// <summary>
        /// Converts string to Boolean.
        /// </summary>
        public static bool ConvertStringToBoolean(string input)
        {
            if (input == null || input == string.Empty)
            {
                return false;
            }

            string formattedInput = input.Trim().ToLowerInvariant();
            List<string> trueValues = new List<string> { "1", "true", "t", "yes", "y", " " };
            List<string> falseValues = new List<string> { "0", "false", "f", "no", "n", "" };

            if (trueValues.Contains(formattedInput))
            {
                return true;
            }

            if (falseValues.Contains(formattedInput))
            {
                return false;
            }

            throw new FormatException("Invalid value for Boolean!");
        }

        /// <summary>
        /// Checks whether the string is a valid IP address.
        /// </summary>
        public static bool IsIpAddress(string address)
        {
            Regex ipMatch = new Regex(@"^([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])$");
            return ipMatch.IsMatch(address);
        }

        /// <summary>
        /// Checks whether a string contains a float separator.
        /// </summary>
        public static bool FloatAsTrue(string s)
        {
            s = FloatPuttingInOrder(s);
            return s.LastIndexOf('.') != -1;
        }

        /// <summary>
        /// Converts string to float.
        /// </summary>
        public static float FloatAsFloat(string s)
        {
            return float.Parse(FloatPuttingInOrder(s), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts float string to integer part.
        /// </summary>
        public static int FloatToInteger(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            return Convert.ToInt32(parts[0]);
        }

        /// <summary>
        /// Gets fractional part from float string.
        /// </summary>
        public static int FloatToFractionalNumber(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            return parts.Length == 1 ? 0 : Convert.ToInt32(parts[1]);
        }

        /// <summary>
        /// Checks a float for NaN.
        /// </summary>
        public static bool IsNaN(float f)
        {
            FloatUnion union = new FloatUnion { Value = f };
            return ((union.Binary & 0x7F800000) == 0x7F800000) && ((union.Binary & 0x007FFFFF) != 0);
        }

        /// <summary>
        /// Converts string to double.
        /// </summary>
        public static double DoubleAsDouble(string s)
        {
            try
            {
                return double.Parse(DoublePuttingInOrder(s), CultureInfo.InvariantCulture);
            }
            catch
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Normalizes a double string according to language standards.
        /// </summary>
        public static string StringDoubleAsString(string s)
        {
            if (!double.TryParse(s, out double result))
            {
                s = s.Replace(",", ".").Trim();
                if (!double.TryParse(s, out result))
                {
                    s = s.Replace(".", ",").Trim();
                    return double.TryParse(s, out result) ? s : "NaN";
                }
            }

            return s;
        }

        /// <summary>
        /// Converts string to double using RU and EN cultures.
        /// </summary>
        public static double StringToDouble(string s)
        {
            if (!double.TryParse(s, NumberStyles.Any, CultureInfo.GetCultureInfo("ru-RU"), out double result) &&
                !double.TryParse(s, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result))
            {
                return 0;
            }

            return result;
        }

        /// <summary>
        /// Formats byte count to user-friendly string.
        /// </summary>
        public static string FormatBytes(long bytes, bool localIsRussian = false)
        {
            string[] suffixRus = { "байт", "Кб", "Мб", "Гб", "Тб" };
            string[] suffixEng = { "byte", "Kb", "Mb", "Gb", "Tb" };
            string[] suffix = localIsRussian ? suffixRus : suffixEng;

            int i = 0;
            double dblSByte = bytes;
            try
            {
                if (bytes > 1024)
                {
                    for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    {
                        dblSByte = bytes / 1024.0;
                    }
                }

                return string.Format("{0:0.##}{1}", dblSByte, suffix[i]);
            }
            catch
            {
                return dblSByte.ToString();
            }
        }

        /// <summary>
        /// Gets random 64-bit integer.
        /// </summary>
        public static long GetRandomLong()
        {
            byte[] randomArr = new byte[8];
            Rng.GetBytes(randomArr);
            return BitConverter.ToInt64(randomArr, 0);
        }

        /// <summary>
        /// Gets random byte array.
        /// </summary>
        public static byte[] GetRandomBytes(int count)
        {
            byte[] randomArr = new byte[count];
            Rng.GetBytes(randomArr);
            return randomArr;
        }

        /// <summary>
        /// Computes MD5 hash value for a byte array.
        /// </summary>
        public static string ComputeHash(byte[] bytes)
        {
            return BytesToHex(MD5.Create().ComputeHash(bytes));
        }

        /// <summary>
        /// Computes MD5 hash value for a string.
        /// </summary>
        public static string ComputeHash(string s)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(s ?? string.Empty));
        }

        /// <summary>
        /// Encrypts the string with the specified secret key and initialization vector.
        /// </summary>
        public static string Encrypt(string s, byte[] secretKey, byte[] iv)
        {
            return BytesToHex(EncryptBytes(Encoding.UTF8.GetBytes(s ?? string.Empty), secretKey, iv));
        }

        /// <summary>
        /// Encrypts the string with the default secret key and initialization vector.
        /// </summary>
        public static string Encrypt(string s)
        {
            return Encrypt(s, DefaultSecretKey, DefaultIV);
        }

        /// <summary>
        /// Encrypts the byte array.
        /// </summary>
        public static byte[] EncryptBytes(byte[] bytes, byte[] secretKey, byte[] iv)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (secretKey == null)
            {
                throw new ArgumentNullException(nameof(secretKey));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using Aes alg = Aes.Create();
            alg.Key = secretKey;
            alg.IV = iv;

            using MemoryStream memStream = new MemoryStream();
            using (CryptoStream cryptoStream = new CryptoStream(memStream, alg.CreateEncryptor(secretKey, iv), CryptoStreamMode.Write))
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
            }

            return memStream.ToArray();
        }

        /// <summary>
        /// Decrypts the string with the specified secret key and initialization vector.
        /// </summary>
        public static string Decrypt(string s, byte[] secretKey, byte[] iv)
        {
            byte[] bytes = HexToBytes(s, false, true) ?? throw new FormatException("Not Hexadecimal");
            return Encoding.UTF8.GetString(DecryptBytes(bytes, secretKey, iv));
        }

        /// <summary>
        /// Decrypts the string with the default secret key and initialization vector.
        /// </summary>
        public static string Decrypt(string s)
        {
            return Decrypt(s, DefaultSecretKey, DefaultIV);
        }

        /// <summary>
        /// Decrypts the byte array.
        /// </summary>
        public static byte[] DecryptBytes(byte[] bytes, byte[] secretKey, byte[] iv)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (secretKey == null)
            {
                throw new ArgumentNullException(nameof(secretKey));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using Aes alg = Aes.Create();
            alg.Key = secretKey;
            alg.IV = iv;

            using MemoryStream memStream = new MemoryStream(bytes);
            using CryptoStream cryptoStream = new CryptoStream(memStream, alg.CreateDecryptor(secretKey, iv), CryptoStreamMode.Read);
            return ReadToEnd(cryptoStream);
        }

        /// <summary>
        /// Gets the password hash.
        /// </summary>
        public static string GetPasswordHash(int itemKey, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            string hash1 = ComputeHash(password);
            string hash2 = ComputeHash(BitConverter.GetBytes(itemKey));
            return ComputeHash(hash1 + hash2 + PasswordSalt);
        }

        /// <summary>
        /// Generates password of the specified length.
        /// </summary>
        public static string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            while (length-- > 0)
            {
                result.Append(valid[random.Next(valid.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts byte array to hexadecimal string.
        /// </summary>
        public static string BytesToHex(byte[] bytes)
        {
            return BytesToHex(bytes, 0, bytes == null ? 0 : bytes.Length);
        }

        /// <summary>
        /// Converts byte array to hexadecimal string.
        /// </summary>
        public static string BytesToHex(byte[] bytes, int index, int count)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = index, endIdx = index + count; i < endIdx; i++)
            {
                stringBuilder.Append(bytes[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts byte array to user friendly string.
        /// </summary>
        public static string BytesToString(byte[] bytes, int index, int count, bool hexFormat = true, bool skipNonPrinting = false)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (hexFormat)
            {
                for (int i = index, lastIdx = index + count - 1; i <= lastIdx; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("X2"));
                    if (i < lastIdx)
                    {
                        stringBuilder.Append(' ');
                    }
                }
            }
            else
            {
                bool notSkip = !skipNonPrinting;
                for (int i = index, endIdx = index + count; i < endIdx; i++)
                {
                    byte b = bytes[i];

                    if (b >= 32)
                    {
                        stringBuilder.Append(Encoding.ASCII.GetString(bytes, i, 1));
                    }
                    else if (notSkip)
                    {
                        stringBuilder.Append('<');
                        stringBuilder.Append(b.ToString("X2"));
                        stringBuilder.Append('>');
                    }
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts hexadecimal string to byte array.
        /// </summary>
        public static bool HexToBytes(string? s, int stringIndex, byte[] buffer, int bufferIndex, int byteCount)
        {
            if (s == null)
            {
                return byteCount == 0;
            }

            int lastIndex = s.Length - 1;
            int bytesConverted = 0;

            while (stringIndex < lastIndex && bytesConverted < byteCount)
            {
                if (byte.TryParse(s.Substring(stringIndex, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture.NumberFormat, out byte val))
                {
                    buffer[bufferIndex] = val;
                    bufferIndex++;
                    bytesConverted++;
                    stringIndex += 2;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Converts hexadecimal string to byte array.
        /// </summary>
        public static bool HexToBytes(string? s, out byte[]? bytes, bool skipWhiteSpace = false)
        {
            if (skipWhiteSpace)
            {
                s = RemoveWhiteSpace(s);
            }

            int strLen = s == null ? 0 : s.Length;

            if (strLen % 2 == 0)
            {
                int bufLen = strLen / 2;
                bytes = new byte[bufLen];
                return HexToBytes(s, 0, bytes, 0, bufLen);
            }

            bytes = null;
            return false;
        }

        /// <summary>
        /// Converts hexadecimal string to byte array.
        /// </summary>
        public static byte[]? HexToBytes(string? s, bool skipWhiteSpace = false, bool throwOnFail = false)
        {
            if (HexToBytes(s, out byte[]? bytes, skipWhiteSpace) && bytes != null)
            {
                return bytes;
            }

            if (throwOnFail)
            {
                throw new FormatException("Not Hexadecimal");
            }

            return null;
        }

        #endregion Basic

        #region Support methods

        /// <summary>
        /// Gets first string item from a data row.
        /// </summary>
        private static string GetItem(DataRow dataRow)
        {
            return dataRow.ItemArray[0]?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Replaces the comma in float string with a dot.
        /// </summary>
        private static string FloatPuttingInOrder(string s)
        {
            return s.Replace(",", ".").Trim();
        }

        /// <summary>
        /// Replaces the comma in double string with a dot.
        /// </summary>
        public static string DoublePuttingInOrder(string s)
        {
            return s.Replace(",", ".").Trim();
        }

        /// <summary>
        /// Reads all data from the stream.
        /// </summary>
        private static byte[] ReadToEnd(Stream inputStream)
        {
            using MemoryStream memStream = new MemoryStream();
            inputStream.CopyTo(memStream);
            return memStream.ToArray();
        }

        /// <summary>
        /// Removes white space characters from the string.
        /// </summary>
        private static string RemoveWhiteSpace(string? s)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (s != null)
            {
                foreach (char c in s)
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        stringBuilder.Append(c);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        #endregion Support methods
    }
}
