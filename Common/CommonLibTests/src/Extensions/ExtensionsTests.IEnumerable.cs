using DotNetExtras.Common.Extensions;

namespace CommonLibTests;
public partial class ExtensionsTests
{
    [Fact]
    public void IEnumerable_Count()
    {
        string[] emptyStringArray = [];
        Assert.Equal(0, emptyStringArray.Count());

        List<string> stringList = ["a", "b", "c"];
        Assert.Equal(3, stringList.Count());

        string[] stringArray = ["a", "b", "c"];
        Assert.Equal(3, stringArray.Count());

        List<int> intList = [1, 2, 3];
        Assert.Equal(3, intList.Count());

        List<int> intArray = [1, 2, 3];
        Assert.Equal(3, intArray.Count());

        HashSet<string> stringHashSet = ["a", "b", "c", "d", "e", "f"];
        Assert.Equal(6, stringHashSet.Count());

        Dictionary<string, int> stringIntDictionary = new()
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3
        };
        Assert.Equal(3, stringIntDictionary.Count());
    }

    [Fact]
    public void IEnumerable_ToCsv()
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
}
