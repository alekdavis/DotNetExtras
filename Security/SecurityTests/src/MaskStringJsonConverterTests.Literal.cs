using DotNetExtras.Security;

namespace SecurityTests;
public partial class MaskStringJsonConverterTests
{
    [Theory]
    [InlineData(null, null, "***")]
    [InlineData("", "***", "***")]
    [InlineData("password", "***", "***")]
    [InlineData("password", "hidden", "hidden")]
    public void LiteralMask_Write
    (
        string? input,
        string? expected,
        string maskLiteral
    )
    {
        LiteralMaskStringJsonConverter converter = new(maskLiteral);

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = SerializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("null", null)]
    [InlineData("\"\"", "")]
    [InlineData("\"abc\"", "abc")]
    public void LiteralMask_Read
    (
        string? input,
        string? expected
    )
    {
        LiteralMaskStringJsonConverter converter = new();

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = DeserializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }
}
