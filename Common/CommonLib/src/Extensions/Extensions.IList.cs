using System.Collections;

namespace DotNetExtras.Common.Extensions;
public static partial class Extensions
{
    /// <summary>
    /// Removes all items in the list that match the values of properties in the specified item.
    /// </summary>
    /// <param name="elements">
    /// List items.
    /// </param>
    /// <param name="elementToMatch">
    /// Item holding the property values that will need to match for the list elements to be removed.
    /// </param>
    /// <returns>
    /// Number of elements remove.
    /// </returns>
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
