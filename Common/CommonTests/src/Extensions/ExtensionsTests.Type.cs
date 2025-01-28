using DotNetExtras.Common.Extensions;

namespace CommonLibTests;
public partial class ExtensionsTests
{
    [Theory]
    [InlineData(typeof(bool), true)]
    [InlineData(typeof(bool?), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(string), true)]
    [InlineData(typeof(DateTime), true)]
    [InlineData(typeof(DateTime?), true)]
    [InlineData(typeof(DateTimeOffset), true)]
    [InlineData(typeof(DateTimeOffset?), true)]
    [InlineData(typeof(Guid), true)]
    [InlineData(typeof(Guid?), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(int[]), false)]
    [InlineData(typeof(List<int>), false)]
    [InlineData(typeof(string[]), false)]
    [InlineData(typeof(List<string>), false)]
    [InlineData(typeof(object[]), false)]
    [InlineData(typeof(List<object>), false)]
    [InlineData(typeof(Dictionary<string, string>), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(Models.Employee), false)]
    public void Type_IsSimple
    (
        Type type, 
        bool expected
    )
    {
        Assert.Equal(expected, type.IsSimple());
    }

    [Theory]
    [InlineData(typeof(bool), true)]
    [InlineData(typeof(bool?), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(string), true)]
    [InlineData(typeof(DateTime), true)]
    [InlineData(typeof(DateTime?), true)]
    [InlineData(typeof(DateTimeOffset), true)]
    [InlineData(typeof(DateTimeOffset?), true)]
    [InlineData(typeof(Guid), true)]
    [InlineData(typeof(Guid?), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(int[]), false)]
    [InlineData(typeof(List<int>), false)]
    [InlineData(typeof(string[]), false)]
    [InlineData(typeof(List<string>), false)]
    [InlineData(typeof(object[]), false)]
    [InlineData(typeof(List<object>), false)]
    [InlineData(typeof(Dictionary<string, string>), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(Models.Employee), false)]
    public void Type_IsPrimitive
    (
        Type type, 
        bool expected
    )
    {
        Assert.Equal(expected, type.IsSimple());
    }
}
