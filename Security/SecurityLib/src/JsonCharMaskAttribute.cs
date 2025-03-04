using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Applies a character mask to a string property 
/// during JSON serialization using `System.Text.Json` (STJ).
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonCharMaskAttribute: JsonConverterAttribute
{
    private readonly char _maskChar;
    private readonly int _unmaskedCharsStart;
    private readonly int _unmaskedCharsEnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonCharMaskAttribute"/> class.
    /// </summary>
    /// <param name="maskChar">
    /// Mask character.
    /// </param>
    /// <param name="unmaskedCharsStart">
    /// Number characters to be left unmasked at the start of the string.
    /// </param>
    /// <param name="unmaskedCharsEnd">
    /// Number characters to be left unmasked at the end of the string.
    /// </param>
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

    /// <summary>
    /// Sets a converter that will apply a character mask to a decorated string property.
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
            : (JsonConverter)new CharMaskStringJsonConverter(_maskChar, _unmaskedCharsStart, _unmaskedCharsEnd);
    }
}