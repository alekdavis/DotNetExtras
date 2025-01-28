namespace DotNetExtras.Common.Metadata.Attributes;

/// <summary>
/// Use <see cref="ShortNameAttribute"/> to decorate enumerated fields
/// with the shortened versions of the field names.
/// </summary>
/// <seealso cref="System.Attribute" />
/// <example>
/// <code>
/// private enum TestEnum
/// {
///     [ShortName("Short1")]
///     LongValue1,
///
///     [ShortName("Short2")]
///     LongValue2,
/// }
/// 
/// public void Enum_ToShortName()
/// {
///     TestEnum value = TestEnum.Value1;
/// 
///     string? shortName = value.ToShortName();
/// 
///     Assert.Equal("Short1", abbreviation);
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class ShortNameAttribute: Attribute
{
    /// <summary>
    /// Short name value.
    /// </summary>
    public string ShortName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortNameAttribute"/> class.
    /// </summary>
    /// <param name="shortName">
    /// Short name value.
    /// </param>
    public ShortNameAttribute
    (
        string shortName
    )
    {
        ShortName = shortName;
    }
}
