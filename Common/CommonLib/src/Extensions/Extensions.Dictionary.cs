using System.Dynamic;

namespace DotNetExtras.Common.Extensions;

public static partial class Extensions
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
    /// <example>
    /// <code>
    /// Dictionary&lt;string, object?&gt; dictionary = new()
    /// {
    ///     { "Key1", "Value1" },
    ///     { "Key2", 123 },
    ///     { "Key3", true }
    /// };
    /// 
    /// dynamic result1 = dictionary.ToDynamic();
    /// </code>
    /// </example>
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
