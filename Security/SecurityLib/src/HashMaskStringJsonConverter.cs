using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Masks a string property with a specified hash value
/// during JSON serialization using `System.Text.Json` (STJ).
/// </summary>
public class HashMaskStringJsonConverter: JsonConverter<string>
{
    private readonly HashType _hashType;
    private readonly int _saltLength;
    private readonly bool _saveSalt;

    /// <summary>
    /// Initializes a new instance of the <see cref="HashMaskStringJsonConverter"/> class.
    /// </summary>
    /// <param name="hashType">
    /// Hash algorithm.
    /// </param>
    /// <param name="saltLength">
    /// Length of the optional random salt to be generated.
    /// </param>
    /// <param name="saveSalt">
    /// If `true`, the salt will be added to the beginning of the hash string.
    /// </param>
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
