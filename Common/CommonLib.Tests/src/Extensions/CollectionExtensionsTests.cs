using DotNetExtras.Extensions;

namespace CommonLibTests.Extensions;
public class CollectionExtensionsTests
{
    [Fact]
    public void Count()
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
}
