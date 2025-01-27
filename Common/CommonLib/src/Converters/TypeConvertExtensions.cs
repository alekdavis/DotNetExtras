using System.Dynamic;

namespace DotNetExtras.Common;
/// <summary>
/// Implements extension methods for type conversions.
/// </summary>
public static class TypeConvertExtensions
{
    
    /// <summary>
    /// Converts a string dictionary object to a dynamic object.
    /// </summary>
    /// <param name="source">
    /// Dictionary object.
    /// </param>
    /// <returns>
    /// Expando object.
    /// </returns>
    public static dynamic ToDynamic
    (
        this Dictionary<string, object?> source
    )
    {
        dynamic target = source.Aggregate(
            new ExpandoObject() as IDictionary<string, object?>,
                (a, p) =>
                {
                    a.Add(p);
                    return a;
                });

        return target;
    }
}
