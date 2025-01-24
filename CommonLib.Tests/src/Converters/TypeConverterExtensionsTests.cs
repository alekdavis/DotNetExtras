using DotNetExtras.Converters;

namespace CommonLibTests.Converters;
public class TypeConverterExtensionsTests
{
    [Fact]
    public void ToCsv()
    {
        List<string> stringList = ["a", "b", "c" ];
        Assert.Equal("a, b, c", stringList.ToCsv());

        string[] stringArray = ["a", "b", "c" ];
        Assert.Equal("'a','b','c'", stringArray.ToCsv(",", "'"));

        List<int> intList = [1, 2, 3];
        Assert.Equal("1;2;3", intList.ToCsv(";"));

        List<int> intArray = [1, 2, 3];
        Assert.Equal("[ 1 ] | [ 2 ] | [ 3 ]", intArray.ToCsv(" | ", "[ ", " ]"));
    }

    [Fact]
    public void ToDynamic()
    {
        Dictionary<string, object?> dictionary = new()
        {
            { "Key1", "Value1" },
            { "Key2", 123 },
            { "Key3", true }
        };

        dynamic result1 = dictionary.ToDynamic();

        Assert.Equal("Value1", result1.Key1);
        Assert.Equal(123, result1.Key2);
        Assert.Equal(true, result1.Key3);

        dictionary = new Dictionary<string, object?>
        {
            { "Key1", null }
        };

        dynamic result2 = dictionary.ToDynamic();

        Assert.Null(result2.Key1);

        dictionary = [];

        dynamic result3 = dictionary.ToDynamic();

        Assert.Empty((IDictionary<string, object>)result3);
    }
}
