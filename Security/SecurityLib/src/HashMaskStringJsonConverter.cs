using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
public class HashMaskStringJsonConverter: JsonConverter<string>
{
    private readonly HashType _hashType;
    private readonly int _saltLength;
    private readonly bool _saveSalt;

    public HashMaskStringJsonConverter
    (
        HashType hashType = HashType.SHA256,
        int saltLength = 0,
        bool saveSalt = false
    )
    {
        _hashType = hashType;
        _saltLength = saltLength;
        _saveSalt = saveSalt;
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

        string? salt    = null;
        string? saltHex = null;

        if (_saltLength > 0)
        {
            salt = Password.Generate(_saltLength);

            if (_saveSalt)
            {
                saltHex = string.Concat(Array.ConvertAll(Encoding.UTF8.GetBytes(salt ?? ""), h => h.ToString("x2")));
            }
        }

        string? hashValue = Hash.Generate(_hashType, value, salt);

        hashValue = (saltHex == null) ? hashValue : saltHex + hashValue;

        writer.WriteStringValue(hashValue);
    }
}
