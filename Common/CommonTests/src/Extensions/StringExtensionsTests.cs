using DotNetExtras.Common;

namespace CommonLibTests.Extensions;
public class StringExtensionsTests
{
    [Theory]
    [InlineData("Hello", "Hello")]
    [InlineData("It's a test", "It''s a test")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void Escape
    (
        string? source, 
        string? expected
    )
    {
        var result = source?.Escape();

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
    public void ToSentence
    (
        string? source, 
        string expected
    )
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var result = source.ToSentence();
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToType()
    {
        Assert.Equal((short)123, "123".ToType<short>());
        Assert.Equal((short)123, "123".ToType<short?>());
        Assert.Equal(123, "123".ToType<int>());
        Assert.Equal(123, "123".ToType<int?>());
        Assert.Equal(123, "123".ToType<long>());
        Assert.Equal(123, "123".ToType<long?>());
        Assert.Equal((double)123.45, "123.45".ToType<double>());
        Assert.Equal((double)123.45, "123.45".ToType<double?>());
        Assert.Equal((float)123.45, "123.45".ToType<float>());
        Assert.Equal((float)123.45, "123.45".ToType<float?>());
        Assert.Equal(new DateTime(2021, 10, 11, 17, 54, 38), "2021-10-11T17:54:38".ToType<DateTime>());
        Assert.Equal(new DateTime(2021, 10, 11, 17, 54, 38), "2021-10-11T17:54:38".ToType<DateTime?>());
        Assert.Equal(new DateTimeOffset(2021, 10, 11, 17, 54, 38, new TimeSpan(-3, -30, 0)), "2021-10-11T17:54:38-03:30".ToType<DateTimeOffset>());
        Assert.Equal(new DateTimeOffset(2021, 10, 11, 17, 54, 38, new TimeSpan(-3, -30, 0)), "2021-10-11T17:54:38-03:30".ToType<DateTimeOffset?>());
        Assert.True("true".ToType<bool>());
        Assert.False("false".ToType<bool>());
    }

    [Fact]
    public void ToList()
    {
        Assert.Equal([1, 2, 3], "1|2|3".ToList<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToList<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToList<long>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToList<string>());
    }

    [Fact]
    public void ToArray()
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0305 // Simplify collection initialization
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<long>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToArray<string>());
#pragma warning restore IDE0305 // Simplify collection initialization
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void ToDictionary()
    {
        Assert.Equal(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        }, "key1=value1|key2=value2".ToDictionary<string, string>());

        Assert.Equal(new Dictionary<int, int>
        {
            { 12, 144 },
            { 13, 169 }
        }, "12=144|13=169".ToDictionary<int, int>());
    }

    [Fact]
    public void ToHashSet()
    {
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<long>());
        Assert.Equal([(double)1.1, (double)2.2, (double)3.3], "1.1|2.2|3.3".ToHashSet<double>());
        Assert.Equal([(float)1.1, (float)2.2, (float)3.3], "1.1|2.2|3.3".ToHashSet<float>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToHashSet<string>());
    }
}
