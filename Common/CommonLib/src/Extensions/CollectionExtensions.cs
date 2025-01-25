using System.Collections;

namespace DotNetExtras.Extensions;

/// <summary>
/// Implements extension methods applicable to collections.
/// </summary>
public static class CollectionExtensions
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
}
