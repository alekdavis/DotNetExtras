namespace DotNetExtras.Text;

/// <summary>
/// Implements common regular expressions.
/// </summary>
/// <remarks>
/// Do not make this class static because it will be easier to extend in the projects using the library.
/// </remarks>
public class RegexString
{
    /// <summary>
    /// Used to indicate whether string is a valid GUID/UUID adopted from:
    /// https://stackoverflow.com/questions/11040707/c-sharp-regex-for-guid
    /// </summary>
    public static readonly string Guid =
        //"^[0-9A-Fa-f]{8}(?:-[0-9A-Fa-f]{4}){3}-[0-9A-Fa-f]{12}$";
        "^[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?$";

    /// <summary>
    /// The email address validation adopted from:
    /// https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp
    /// </summary>
    /// <remarks>
    /// <para>
    /// In the StackOverflow example, the \A, \Z modifiers mean beginning/end of string anchors per:
    /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/anchors-in-regular-expressions
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
