using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Allows applying masks to protect sensitive string object properties.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonHashMaskAttribute: JsonConverterAttribute
{
    private readonly HashType _hashType;
    private readonly int _saltLength;
    private readonly bool _saveSalt;

    public JsonHashMaskAttribute
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

    public override JsonConverter CreateConverter
    (
        Type type
    )
    {
        return type != typeof(string)
            ? throw new ArgumentException(
                $"Attribute can only be applied to 'string' properties, but it was applied to a '{type.Name}' property.")
            : (JsonConverter)new HashMaskStringJsonConverter(_hashType, _saltLength, _saveSalt);
    }
}