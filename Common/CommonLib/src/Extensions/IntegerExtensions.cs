namespace DotNetExtras.Common;
/// <summary>
/// Extension methods for integer values.
/// </summary>
public static class IntegerExtensions
{
    /// <summary>
    /// Converts negative integer value to properly formatted HResult value.
    /// </summary>
    /// <param name="hresult">
    /// HResult value.
    /// </param>
    /// <returns>
    /// Hex-formatted hresult value.
    /// </returns>
    public static string? ToHResult
    (
        this int hresult
    )
    {
        return "0x" + hresult.ToString("X8");
    }
}
