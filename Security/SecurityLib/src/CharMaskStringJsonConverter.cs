using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
public class CharMaskStringJsonConverter: JsonConverter<string>
{
    private readonly char _maskChar;
    private readonly int _unmaskedCharsStart;
    private readonly int _unmaskedCharsEnd;

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
