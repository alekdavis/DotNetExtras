namespace DotNetExtras.Attributes;

/// <summary>
/// Defines an abbreviation.
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="AbbreviationAttribute"/> class.
/// </remarks>
/// <param name="abbreviation">
/// Abbreviation.
/// </param>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class AbbreviationAttribute
(
    string abbreviation
)
: Attribute
{
    /// <summary>
    /// Abbreviation.
    /// </summary>
    public string Abbreviation { get; private set; } = abbreviation;
}
