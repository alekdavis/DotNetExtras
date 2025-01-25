using DotNetExtras.Extensions;

namespace CommonLibTests.Extensions;
public class TypeExtensionsTests
{
    [Theory]
    [InlineData(typeof(void), true)]
    [InlineData(typeof(bool), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(string), true)]
    [InlineData(typeof(DateTime), true)]
    [InlineData(typeof(DateTimeOffset), true)]
    [InlineData(typeof(Guid), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(int[]), false)]
    [InlineData(typeof(List<int>), false)]
    [InlineData(typeof(string[]), false)]
    [InlineData(typeof(List<string>), false)]
    [InlineData(typeof(object[]), false)]
    [InlineData(typeof(List<object>), false)]
    [InlineData(typeof(Dictionary<string, string>), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(Models.Employee), false)]
    public void IsSimpleType
    (
        Type type, 
        bool expected
    )
    {
        // Act
        var result = type.IsSimple();

        // Assert
        Assert.Equal(expected, result);
    }
}
