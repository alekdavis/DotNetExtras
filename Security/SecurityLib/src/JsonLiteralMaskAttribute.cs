using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Allows applying masks to protect sensitive string object properties.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonLiteralMaskAttribute: JsonConverterAttribute
{
    private readonly string? _maskLiteral;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLiteralMaskAttribute"/> class.
    /// </summary>
    /// <param name="maskLiteral">
    /// Literal string value that will replace the string property value (can be `null`).
    /// </param>
    public JsonLiteralMaskAttribute
    (
        string? maskLiteral = null
    )
    {
        _maskLiteral = maskLiteral;
    }

    /// <summary>
    /// Sets a converter that will apply a literal string mask to a decorated string property.
    /// </summary>
    /// <param name="type">
    /// Data type to convert (<see cref="string"/> only).
    /// </param>
    /// <returns>
    /// Character mask converter.
    /// </returns>
    public override JsonConverter CreateConverter
    (
        Type type
    )
    {
        return type != typeof(string)
            ? throw new ArgumentException(
                $"Attribute can only be applied to 'string' properties, but it was applied to a '{type.Name}' property.")
            : (JsonConverter)new LiteralMaskStringJsonConverter(_maskLiteral);
    }
}