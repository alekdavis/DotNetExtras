using DotNetExtras.Common;

namespace CommonLibTests.Extensions;
/// <summary>
/// Extension methods applicable to integers.
/// </summary>
public class IntegerExtensionsTests
{
    [Theory]
    [InlineData(-1, "0xFFFFFFFF")]
    [InlineData(0, "0x00000000")]
    [InlineData(1, "0x00000001")]
    [InlineData(-2147483648, "0x80000000")]
    [InlineData(2147483647, "0x7FFFFFFF")]
    public void ToHResult(int hresult, string expected)
    {
        // Act
        var result = hresult.ToHResult();

        // Assert
        Assert.Equal(expected, result);
    }
}
