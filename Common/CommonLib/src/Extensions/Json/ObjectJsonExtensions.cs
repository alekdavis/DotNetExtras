using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Extensions.Json;
/// <summary>
/// JSON-specific extension methods for objects.
/// </summary>
public static class ObjectJsonExtensions
{
    #region Public methods
    /// <summary>
    /// Converts an object to a JSON string.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <returns>
    /// JSON string.
    /// </returns>
    public static string ToJson
    (
        this object? source,
        bool indented = false
    )
    {
        if (source == null)
        {
            return "";
        }

        JsonSerializerOptions options = new() 
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = indented,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        return JsonSerializer.Serialize(source, options);
    }
    #endregion
}
