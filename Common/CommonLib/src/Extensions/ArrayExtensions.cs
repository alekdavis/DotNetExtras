using DotNetExtras.Extensions.Utilities;

namespace DotNetExtras.Extensions;

/// <summary>
/// Implements extension methods applicable to arrays.
/// </summary>
/// <remarks>
/// Adapted from 
/// https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs
/// for deep cloning.
/// </remarks>
internal static class ArrayExtensions
{
    internal static void ForEach
    (
        this Array array, 
        Action<Array, int[]> action
    )
    {
        if (array.LongLength == 0)
        {
            return;
        }

        ArrayTraverse walker = new(array);

        do
        {
            action(array, walker.Position);
        }
        while (walker.Step());
    }
}
