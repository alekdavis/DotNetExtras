using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Extension methods for string values.
/// </summary>
public static partial class Extensions
{
    /// <summary>
    /// Escapes specific characters in a string.
    /// </summary>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="escapeChar">
    /// Specifies the character that must be escaped.
    /// </param>
    /// <param name="replacementString">
    /// Specifies the replacement string for the escaped character 
    /// (may need to include the escaped character).
    /// </param>
    /// <returns>
    /// String value with properly escaped character.
    /// </returns>
    public static string? Escape
    (
        this string source,
        char escapeChar = '\'',
        string replacementString = "''"
    )
    {
        return string.IsNullOrEmpty(source) 
            ? source 
            : source.Replace($"{escapeChar}", $"{replacementString}");
    }

    /// <summary>
    /// Appends a period at the end of the string,
    /// unless it already ends with one of the punctuation characters:
    /// ,.!?;:
    /// </summary>
    /// <param name="source">
    /// Input string.
    /// </param>
    /// <param name="trimStart">
    /// Indicates that white space characters must be trimmed from the string start.
    /// </param>
    /// <param name="trimEnd">
    /// Indicates that white space characters must be trimmed from the string end.
    /// </param>
    /// <returns>
    /// Input string that has a valid punctuation string at the end.
    /// </returns>
    public static string ToSentence
    (
        this string source,
        bool trimStart = true,
        bool trimEnd = true
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return "";
        }

        if (trimStart)
        {
            source = source.TrimStart();
        }

        if (trimEnd)
        {
            source = source.TrimEnd();
        }

        return string.IsNullOrEmpty(source)
            ? ""
            : Regex.IsMatch(source, @"[\p{P}]$")
                ? source
                : source + ".";
    }
    /// <summary>
    /// Converts a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">
    /// Target data type.
    /// </typeparam>
    /// <param name="json">
    /// Original value.
    /// </param>
    /// <returns>
    /// Converted value or default if conversion failed.
    /// </returns>
    public static T? FromJson<T>
    (
        this string? json
    )
    where T : class
    {
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        JsonSerializerOptions options = new() 
        {
            PropertyNameCaseInsensitive = true,  
            Converters = { new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <summary>
    /// Converts a string value to the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Target data type.
    /// </typeparam>
    /// <param name="source">
    /// Original string value.
    /// </param>
    /// <returns>
    /// Converted value or default if conversion failed.
    /// </returns>
    public static T ToType<T>
    (
        this string source
    )
    {
        if (typeof(T) == typeof(DateTimeOffset))
        {
            return (T)(object)DateTimeOffset.Parse(source);
        }
        // Handling Nullable types i.e, int?, double?, bool? .. etc
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        return Nullable.GetUnderlyingType(typeof(T)) != null
            ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(source)
            : (T)Convert.ChangeType(source, typeof(T));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8603 // Possible null reference return.
    }

    /// <summary>
    /// Converts a string to a <see cref="System.DateTime"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit date/time format.
    /// </param>
    /// <returns>
    /// DateTime value.
    /// </returns>
    public static DateTime? ToDateTime
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null ? DateTime.Parse(source) : DateTime.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a string to a <see cref="System.DateTimeOffset"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit date/time format.
    /// </param>
    /// <returns>
    /// DateTimeOffset value.
    /// </returns>
    public static DateTimeOffset? ToDateTimeOffset
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null 
                ? DateTimeOffset.Parse(source) 
                : DateTimeOffset.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts string to a list.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the list elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// List item delimiter character.
    /// </param>
    /// <returns>
    /// Generic list.
    /// </returns>
    public static List<T>? ToList<T>
    (
        this string? source,
        char delimiter = '|'
    )
    {
        if (source == null)
        {
            return null;
        }

        string[] items = source.Split(delimiter);
        List<T> list = [];

        foreach (string item in items)
        {
            if (item.Trim() != "")
            {
                T value = (T)Convert.ChangeType(item, typeof(T));
                list.Add(value);
            }
        }

        return list;
    }

    /// <summary>
    /// Converts string to array.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the array elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// Array item delimiter character.
    /// </param>
    /// <returns>
    /// Generic array.
    /// </returns>
    public static T[]? ToArray<T>
    (
        this string? source, 
        char delimiter='|'
    )
    {
        return source?.Split(delimiter).Select(n => (T)Convert.ChangeType(n, typeof(T))).ToArray<T>();
    }

    /// <summary>
    /// Converts string to a dictionary.
    /// </summary>
    /// <typeparam name="TKey">
    /// Data type of the dictionary key elements.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Data type of the dictionary value elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// List item delimiter character.
    /// </param>
    /// <param name="keyValueSeparator">
    /// Name value delimiter character.
    /// </param>
    /// <returns>
    /// Generic dictionary.
    /// </returns>
    public static Dictionary<TKey,TValue>? ToDictionary<TKey,TValue>
    (
        this string? source,
        char delimiter = '|',
        char keyValueSeparator = '='
    )
    where TKey : notnull
    {
        if (source == null)
        {
            return null;
        }

        Dictionary<TKey,TValue> dictionary = [];

        if (string.IsNullOrEmpty(source))
        {
            return dictionary;
        }

        string[] pairs = source.Split(delimiter);

        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(keyValueSeparator);

            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();

                string value = keyValue[1].Trim();

                if (key != "")
                {
                    try
                    {
                        dictionary[(TKey)Convert.ChangeType(key, typeof(TKey))] = (TValue)Convert.ChangeType(value, typeof(TValue));
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException($"Cannot convert key '{key}' to '{typeof(TKey).Name}' or '{value}' to '{typeof(TValue).Name}'.", ex);
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid key{keyValueSeparator}value pair '{pair}': it is expected to be split into 2 parts, but resulted in {keyValue.Length}.");
            }
        }

        return dictionary;
    }

    /// <summary>
    /// Converts string to a hash set.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the hash set elements.
    /// </typeparam>
    /// <param name="source">
    /// Input string.
    /// </param>
    /// <param name="delimiter">
    /// Delimiter character.
    /// </param>
    /// <returns>
    /// Generic hash set.
    /// </returns>
    public static HashSet<T>? ToHashSet<T>
    (
        this string? source,
        char delimiter = '|'
    )
    {
        if (source == null)
        {
            return null;
        }

        string[] items = source.Split(delimiter);
        HashSet<T> hashSet = [];

        foreach (string item in items)
        {
            if (item.Trim() != "")
            {
                T value = (T)Convert.ChangeType(item, typeof(T));
                hashSet.Add(value);
            }
        }

        return hashSet;
    }
}
