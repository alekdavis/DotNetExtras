using DotNetExtras.Text;

namespace CommonLibTests.Text;
public class TextExtensionsTests
{
    [Theory]
    [InlineData("Hello", "Hello")]
    [InlineData("It's a test", "It''s a test")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void EscapeSingleQuotes(string input, string expected)
    {
        // Act
        var result = input.EscapeSingleQuotes();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Hello", "Hello.")]
    [InlineData(" Hello", "Hello.")]
    [InlineData("Hello ", "Hello.")]
    [InlineData(" Hello ", "Hello.")]
    [InlineData("Hello.", "Hello.")]
    [InlineData("Hello. ", "Hello.")]
    [InlineData("Hello!", "Hello!")]
    [InlineData("Hello?", "Hello?")]
    [InlineData("Hello,", "Hello,")]
    [InlineData("Hello;", "Hello;")]
    [InlineData("Hello:", "Hello:")]
    [InlineData("Hello 123", "Hello 123.")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToSentence(string input, string expected)
    {
        // Act
        var result = input.ToSentence();

        // Assert
        Assert.Equal(expected, result);
    }
}
