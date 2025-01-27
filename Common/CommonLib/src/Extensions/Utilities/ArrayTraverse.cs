namespace DotNetExtras.Common;

/// <summary>
/// Implements extension methods for array traversals.
/// </summary>
/// <remarks>
/// Adapted from 
/// https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs
/// for deep cloning.
/// </remarks>
internal class ArrayTraverse
{
    internal int[] Position;
    private readonly int[] _maxLengths;

    internal ArrayTraverse
    (
        Array array
    )
    {
        _maxLengths = new int[array.Rank];

        for (int i = 0; i < array.Rank; ++i)
        {
            _maxLengths[i] = array.GetLength(i) - 1;
        }

        Position = new int[array.Rank];
    }

    internal bool Step()
    {
        for (int i = 0; i < Position.Length; ++i)
        {
            if (Position[i] < _maxLengths[i])
            {
                Position[i]++;
                for (int j = 0; j < i; j++)
                {
                    Position[j] = 0;
                }

                return true;
            }
        }

        return false;
    }
}

