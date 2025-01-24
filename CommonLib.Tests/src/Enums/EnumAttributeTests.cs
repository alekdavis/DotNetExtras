using DotNetExtras.Attributes;
using System.ComponentModel;

namespace CommonLibTests.Enums;

public class EnumAttributeTests
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
    public void ToDescription()
    {
        // Arrange
        var value = TestEnum.Value1;

        // Act
        var description = value.ToDescription();

        // Assert
        Assert.Equal("Description 1", description);
    }

    [Fact]
    public void ToAbbreviation()
    {
        // Arrange
        var value = TestEnum.Value1;

        // Act
        var abbreviation = value.ToAbbreviation();

        // Assert
        Assert.Equal("ABBR1", abbreviation);
    }

    [Fact]
    public void ToShortName()
    {
        // Arrange
        var value = TestEnum.Value1;

        // Act
        var shortName = value.ToShortName();

        // Assert
        Assert.Equal("Short1", shortName);
    }
}
