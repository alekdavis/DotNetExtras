namespace DotNetExtras.Common.Metadata.Attributes;

/// <summary>
/// Use <see cref="ShortNameAttribute"/> to decorate enumerated fields
/// with the abbreviated versions of the field names.
/// </summary>
/// <remarks>
/// To get the abbreviation value, 
/// use the <see cref="Extensions.Extensions.ToShortName(Enum)"/>
/// extension method
/// </remarks>
/// <seealso cref="System.Attribute" />
/// <example>
/// <code>
/// private enum TestEnum
/// {
///     [Abbreviation("ABBR1")]
///     LongValue1,
///   
///     [Abbreviation("ABBR2")]
///     LongValue2,
/// } 
/// 
/// public void Enum_ToAbbreviation()
/// {
///     TestEnum value = TestEnum.Value1;
/// 
///     string? abbreviation = value.ToAbbreviation();
/// 
///     Assert.Equal("ABBR1", abbreviation);
/// }
/// </code>
/// </example>
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
