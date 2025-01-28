using DotNetExtras.Common.Extensions;
using DotNetExtras.Common.Metadata.Attributes;
using System.ComponentModel;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    private enum TestEnum
    {
        [Description("Description 1")]
        [Abbreviation("ABBR1")]
        [ShortName("Short1")]
        Value1,

        [Description("Description 2")]
        [Abbreviation("ABBR2")]
        [ShortName("Short2")]
        Value2,
    }

    [Fact]
    public void Enum_ToDescription()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? description = value.ToDescription();

        // Assert
        Assert.Equal("Description 1", description);
    }

    [Fact]
    public void Enum_ToAbbreviation()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? abbreviation = value.ToAbbreviation();

        // Assert
        Assert.Equal("ABBR1", abbreviation);
    }

    [Fact]
    public void Enum_ToShortName()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? shortName = value.ToShortName();

        // Assert
        Assert.Equal("Short1", shortName);
    }
}
