namespace DotNetExtras.Common;

/// <summary>
/// Defines the abbreviation attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class AbbreviationAttribute: Attribute
{
    /// <summary>
    /// Abbreviation value.
    /// </summary>
    public string Abbreviation { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbbreviationAttribute"/> class.
    /// </summary>
    /// <param name="abbreviation">
    /// Abbreviation value.
    /// </param>
    public AbbreviationAttribute
    (
        string abbreviation
    )
    {
        Abbreviation = abbreviation;
    }
}
