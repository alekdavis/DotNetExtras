using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
public class LiteralMaskStringJsonConverter: JsonConverter<string>
{
    private readonly string? _maskLiteral;

    public LiteralMaskStringJsonConverter
    (
        string? maskLiteral = null
    )
    {
        _maskLiteral = maskLiteral;
    }

    public override string? Read
    (
        ref Utf8JsonReader reader, 
        Type typeToConvert, 
        JsonSerializerOptions options
    )
    {
        return reader.GetString();
    }

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
