using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Applies a literal string mask to a string property 
/// during JSON serialization using `System.Text.Json` (STJ).
/// </summary>
public class LiteralMaskStringJsonConverter: JsonConverter<string>
{
    private readonly string? _maskLiteral;

    /// <summary>
    /// Initializes a new instance of the <see cref="LiteralMaskStringJsonConverter"/> class.
    /// </summary>
    /// <param name="maskLiteral">
    /// Literal string value that will replace the string property value (can be `null`).
    /// </param>
    public LiteralMaskStringJsonConverter
    (
        string? maskLiteral = null
    )
    {
        _maskLiteral = maskLiteral;
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
        if (value == null || _maskLiteral == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(_maskLiteral);
    }
}
