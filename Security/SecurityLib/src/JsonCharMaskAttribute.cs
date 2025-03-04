using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Allows applying masks to protect sensitive string object properties.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonCharMaskAttribute: JsonConverterAttribute
{
    private readonly char _maskChar;
    private readonly int _unmaskedCharsStart;
    private readonly int _unmaskedCharsEnd;

    public JsonCharMaskAttribute
    (
        char maskChar = '*',
        int unmaskedCharsStart = 0,
        int unmaskedCharsEnd = 0
    )
    {
        _maskChar = maskChar;
        _unmaskedCharsStart = unmaskedCharsStart;
        _unmaskedCharsEnd = unmaskedCharsEnd;
    }

    public override JsonConverter CreateConverter
    (
        Type type
    )
    {
        return type != typeof(string)
            ? throw new ArgumentException(
                $"Attribute can only be applied to 'string' properties, but it was applied to a '{type.Name}' property.")
            : (JsonConverter)new CharMaskStringJsonConverter(_maskChar, _unmaskedCharsStart, _unmaskedCharsEnd);
    }
}