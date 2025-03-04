using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Masks a string property with a specified character
/// during JSON serialization using `System.Text.Json` (STJ).
/// </summary>
public class CharMaskStringJsonConverter: JsonConverter<string>
{
    private readonly char _maskChar;
    private readonly int _unmaskedCharsStart;
    private readonly int _unmaskedCharsEnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharMaskStringJsonConverter"/> class.
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
    public CharMaskStringJsonConverter
    (
        char maskChar = '*',
        int unmaskedCharsStart = 0,
        int unmaskedCharsEnd = 0
    )
    {
        _maskChar = maskChar;
        _unmaskedCharsStart = unmaskedCharsStart < 0 ? 0 : unmaskedCharsStart;
        _unmaskedCharsEnd = unmaskedCharsEnd < 0 ? 0 : unmaskedCharsEnd;
    }

    /// <summary>
    /// Reads and converts the JSON to type <see cref="string"/>.
    /// </summary>
    /// <param name="reader">
    /// UTF-8 encoded text reader.
    /// </param>
    /// <param name="typeToConvert">
    /// Data type to convert (<see cref="string"/> only).
    /// </param>
    /// <param name="options">
    /// JSON serialization options.
    /// </param>
    /// <returns>
    /// Read string value.
    /// </returns>
    public override string? Read
    (
        ref Utf8JsonReader reader, 
        Type typeToConvert, 
        JsonSerializerOptions options
    )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Writes the masked string value to the JSON element.
    /// </summary>
    /// <param name="writer">
    /// UTF-8 encoded text writer.
    /// </param>
    /// <param name="value">
    /// String value to be masked.
    /// </param>
    /// <param name="options">
    /// JSON serialization options.
    /// </param>
    public override void Write
    (
        Utf8JsonWriter writer, 
        string value, 
        JsonSerializerOptions options
    )
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        if (_unmaskedCharsStart + _unmaskedCharsEnd < value.Length)
        {
            if (_unmaskedCharsStart == 0 && _unmaskedCharsEnd == 0)
            {
                value = new string(_maskChar, value.Length);
            }
            else
            {
                string start = _unmaskedCharsStart == 0 ? "" : value[.._unmaskedCharsStart];
                string end   = _unmaskedCharsEnd == 0 ? "" : value[^_unmaskedCharsEnd..];
                string middle= new(_maskChar, value.Length - _unmaskedCharsStart - _unmaskedCharsEnd);

                value = start + middle + end;
            }
        }

        writer.WriteStringValue(value);
    }
}
