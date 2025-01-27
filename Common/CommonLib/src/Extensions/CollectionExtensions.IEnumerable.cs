using System.Collections;

namespace DotNetExtras.Common;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns number of items in any collection type.
    /// </summary>
    /// <param name="source">
    /// Any type of collection.
    /// </param>
    /// <returns>
    /// Number of items.
    /// </returns>
    public static int Count
    (
        this IEnumerable source
    )
    {
        if (source == null)
        {
            return 0;
        }

        if (source is ICollection collection)
        {
            return collection.Count;
        }

        int count = 0;
        IEnumerator e = source.GetEnumerator();
        while (e.MoveNext())
        {
            count++;
        }

        if (e is IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
            }
        }

        return count;
    }

    /// <summary>
    /// Converts a collection of generic elements to a comma-separated string value.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the generic elements.
    /// </typeparam>
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
}
