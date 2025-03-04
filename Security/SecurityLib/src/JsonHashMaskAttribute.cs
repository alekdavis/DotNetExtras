using System.Text.Json.Serialization;

namespace DotNetExtras.Security;
/// <summary>
/// Applies a hash mask to a string property 
/// during JSON serialization using `System.Text.Json` (STJ).
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonHashMaskAttribute: JsonConverterAttribute
{
    private readonly HashType _hashType;
    private readonly int _saltLength;
    private readonly bool _saveSalt;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonHashMaskAttribute"/> class.
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

    /// <summary>
    /// Sets a converter that will apply a hash mask to a decorated string property.
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
            : (JsonConverter)new HashMaskStringJsonConverter(_hashType, _saltLength, _saveSalt);
    }
}