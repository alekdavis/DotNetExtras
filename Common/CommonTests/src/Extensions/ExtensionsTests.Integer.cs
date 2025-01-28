using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Theory]
    [InlineData(-1, "0xFFFFFFFF")]
    [InlineData(0, "0x00000000")]
    [InlineData(1, "0x00000001")]
    [InlineData(-2147483648, "0x80000000")]
    [InlineData(2147483647, "0x7FFFFFFF")]
    public void Integer_ToHResult(int hresult, string expected)
    {
        string result = hresult.ToHResult();

        Assert.Equal(expected, result);
    }
}
