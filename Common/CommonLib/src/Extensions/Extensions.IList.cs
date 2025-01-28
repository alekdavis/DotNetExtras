using System.Collections;

namespace DotNetExtras.Common.Extensions;
public static partial class Extensions
{
    /// <summary>
    /// Removes all items in the list that match the values of 
    /// the specified properties in the provided item.
    /// </summary>
    /// <param name="elements">
    /// List items.
    /// </param>
    /// <param name="elementToMatch">
    /// Item holding property values that will need to match for the list elements to be removed.
    /// </param>
    /// <returns>
    /// Number of elements remove.
    /// </returns>
    /// <example>
    /// <code>
    /// List&lt;Sample&gt; elements = new()
    /// {
    ///     new(){ Id = 100, Parent =  1, Name = "Item1" },
    ///     new(){ Id = 200, Parent =  2, Name = "Item2" },
    ///     new(){ Id = 300, Parent =  2, Name = "Item3" },
    ///     new(){ Id = 400, Parent =  3, Name = "Item4" }
    /// };
    /// 
    /// Sample match = new() { ParentId = 2 };
    ///
    /// // Removes two items:
    /// int removedCount = elements.RemoveMatching(match);
    /// </code>
    /// </example>
    public static int RemoveMatching
    (
        this IList elements, 
        object elementToMatch
    )
    {
        int removed = 0;
 
        // Iterate through the elements in reverse to safely remove items while iterating
        for (int i = elements.Count - 1; i >= 0; i--)
        {
            object? element = elements[i];

            if (element == null)
            {
                continue;
            }

            if (elementToMatch.IsEquivalentTo(elements[i], true))
            {
                elements.RemoveAt(i);
                removed++;
            }
        }
 
        return removed;
    }
}
