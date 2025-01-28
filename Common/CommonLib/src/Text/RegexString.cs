namespace DotNetExtras.Common.Text;

/// <summary>
/// Implements common regular expressions.
/// </summary>
/// <remarks>
/// This class is not declared as static because a non-static class 
/// can be extended, so you can access both these regular expressions
/// and your custom regular expressions from a derived class.
/// </remarks>
public class RegexString
{
    /// <summary>
    /// A regular expression for validating GUID/UUID values:
    /// ^[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?$
    /// </summary>
    /// <remarks>
    /// <para>
    /// This regular expression allows GUID/UUID values to be enclosed in parentheses or braces
    /// and allows hyphens to be optional (also, can use upper an lower letters a-f).
    /// </para>
    /// <para>
    /// Adapted from <see href="https://stackoverflow.com/questions/11040707/c-sharp-regex-for-guid"/>.
    /// </para>
    /// </remarks>
    public static readonly string Guid =
        //"^[0-9A-Fa-f]{8}(?:-[0-9A-Fa-f]{4}){3}-[0-9A-Fa-f]{12}$";
        "^[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?$";

    /// <summary>
    /// A simplified regular expression for validating Azure-compliant email addresses:
    /// (?=^.{5,64}$)^[a-z0-9!#$%&amp;'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$
    /// </summary>
    /// <remarks>
    /// <para>
    /// Adapted from
    /// <see href="https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp"/>.
    /// In the StackOverflow example, the \A, \Z modifiers mean the beginning and end of the string anchors
    /// (see <see href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/anchors-in-regular-expressions"/>).
    /// I changed it to use the standard ^ and $ anchors.
    /// </para>
    /// <para>
    /// Do not allow asterisk because it may interfere with some Azure queries.
    /// </para>
    /// <para>
    /// Max length is set to 64 chars due to Azure limitations (how it handles mailNickname and UPN).
    /// </para>
    /// </remarks>
    public static readonly string EmailAddress =
        @"(?=^.{5,64}$)^[a-z0-9!#$%&'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
}
