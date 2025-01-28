namespace DotNetExtras.Common.Extensions;

public static partial class Extensions
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
