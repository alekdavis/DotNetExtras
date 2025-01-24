using System.Dynamic;

namespace DotNetExtras.Converters;
public static class TypeConvertExtensions
{
    /// <summary>
    /// Converts a collection of generic elements to a comma-separated string value.
    /// </summary>
    /// <param name="values">
    /// Collection of generic elements.
    /// </param>
    /// <param name="separator">
    /// Value separator.
    /// </param>
    /// <param name="leftQuote">
    /// Left quote enclosing each value.
    /// </param>
    /// <param name="rightQuote">
    /// Right quote enclosing each value 
    /// (if left quote is specified and right quote is not, then left quote will be used as right quote).
    /// </param>
    /// <returns>
    /// Comma-(or whatever)-separated string value (or empty string if collection is null or empty).
    /// </returns>
    public static string ToCsv<T>
    (
        this IEnumerable<T> values,
        string separator = ", ",
        string leftQuote = "",
        string rightQuote = ""
    )
    {
        if (string.IsNullOrEmpty(rightQuote))
        {
            rightQuote = leftQuote;
        }

        return values == null || !values.Any<T>() 
            ? "" 
            : string.Join(separator, values.Select(item => leftQuote + item + rightQuote));
    }
    
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
