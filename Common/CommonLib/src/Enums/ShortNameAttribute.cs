namespace DotNetExtras.Common;

/// <summary>
/// Defines a short name.
/// </summary>
/// <seealso cref="System.Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="ShortNameAttribute"/> class.
/// </remarks>
/// <param name="shortName">
/// Short name.
/// </param>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class ShortNameAttribute
(
    string shortName
)
: Attribute
{
    /// <summary>
    /// Short name.
    /// </summary>
    public string ShortName { get; private set; } = shortName;
}
