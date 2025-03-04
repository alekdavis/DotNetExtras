using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SecurityTests;
public partial class MaskStringJsonConverterTests
{
    private string? SerializeValue
    (
        JsonConverter<string> converter, 
        string value
    )
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);
        JsonSerializerOptions options = new()
        {
            Converters = { converter }
        };

        converter.Write(writer, value, options);
        writer.Flush();
        stream.Position = 0;

        using JsonDocument document = JsonDocument.Parse(stream);

        return document.RootElement.GetString();
    }

    private string? DeserializeValue
    (
        JsonConverter<string> converter, 
        string json
    )
    {
        using JsonDocument document = JsonDocument.Parse(json);

        JsonElement element = document.RootElement;

        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(json));

        reader.Read();

        return converter.Read(ref reader, typeof(string), new JsonSerializerOptions());
    }
}
