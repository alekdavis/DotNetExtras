using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Allows applying masks to protect sensitive string object properties.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonLiteralMaskAttribute: JsonConverterAttribute
{
    private readonly string? _maskLiteral;

    public JsonLiteralMaskAttribute
    (
        string? maskLiteral = null
    )
    {
        _maskLiteral = maskLiteral;
    }

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