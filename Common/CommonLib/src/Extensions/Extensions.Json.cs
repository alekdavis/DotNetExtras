using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Common.Extensions;
public static partial class Extensions
{
    #region Public methods
    /// <summary>
    /// Converts an object to a JSON string.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <param name="indented">
    /// If true, serialized JSON elements will be indented.
    /// </param>
    /// <returns>
    /// JSON string.
    /// </returns>
    /// <example>
    /// <code>
    /// Sample sample = new Sample(){ Name = "John", Age = 30 };
    /// 
    /// // Prints unformatted JSON version of the object
    /// Console.WriteLine(sample.ToJson());
    /// 
    /// // Prints formatted JSON version of the object
    /// Console.WriteLine(sample.ToJson(true));
    /// </code>
    /// </example>
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
