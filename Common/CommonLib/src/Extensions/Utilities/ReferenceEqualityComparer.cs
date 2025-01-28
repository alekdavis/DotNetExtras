namespace DotNetExtras.Common.Extensions.Utilities;

/// <summary>
/// Implements reference equality operations for deep cloning.
/// </summary>
/// <remarks>
/// Adapted from https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs
/// for deep cloning.
/// </remarks>
public class ReferenceEqualityComparer: EqualityComparer<object>
{
    /// <summary>
    /// Determines whether the two objects are equal.
    /// </summary>
    /// <param name="left">
    /// One object.
    /// </param>
    /// <param name="right">
    /// Another object.
    /// </param>
    /// <returns>
    /// True if the two objects are equal; otherwise, false.
    /// </returns>
    public override bool Equals
    (
        object? left, 
        object? right
    )
    {
        return ReferenceEquals(left, right);
    }

    /// <summary>
    /// Returns the hash code for the specified object.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <returns>
    /// Object's hash code.
    /// </returns>
    public override int GetHashCode
    (
        object source
    )
    {
        return source == null
            ? 0
            : source.GetHashCode();
    }
}
