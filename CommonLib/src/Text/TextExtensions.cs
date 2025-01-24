using System.Text.RegularExpressions;

namespace DotNetExtras.Text;
/// <summary>
/// General-purpose extension methods applicable to strings.
/// </summary>
public static partial class TextExtensions
{
    /// <summary>
    /// Escapes single quotes in a string so it can be used in the Graph API filter condition.
    /// </summary>
    /// <param name="input">
    /// String value.
    /// </param>
    /// <param name="escapeChar">
    /// Specifies the escape character (such as backslash).
    /// </param>
    /// <returns>
    /// Value that can be safely enclosed in single quotes.
    /// </returns>
    public static string? EscapeSingleQuotes
    (
        this string input,
        char escapeChar = '\''
    )
    {
        return string.IsNullOrEmpty(input) 
            ? input 
            : input.Contains('\'') 
                ? input.Replace("'", $"{escapeChar}'") 
                : input;
    }

    /// <summary>
    /// Appends a period at the end of the string,
    /// unless it already ends with one of the punctuation characters:
    /// ,.!?;:
    /// </summary>
    /// <param name="input">
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
        this string input,
        bool trimStart = true,
        bool trimEnd = true
    )
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        if (trimStart)
        {
            input = input.TrimStart();
        }

        if (trimEnd)
        {
            input = input.TrimEnd();
        }

        return string.IsNullOrEmpty(input)
            ? ""
            : Regex.IsMatch(input, @"[\p{P}]$")
                ? input
                : input + ".";
    }
}

