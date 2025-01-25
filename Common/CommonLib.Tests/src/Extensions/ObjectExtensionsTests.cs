using CommonLibTests.Models;
using DotNetExtras.Extensions;

namespace CommonLibTests.Extensions;

public class ObjectExtensionsTests
{
    [Fact]
    public void IsEmpty()
    {
        object? o1 = null;
        Assert.True(o1.IsEmpty());

        User? o2 = null;
        Assert.True(o2.IsEmpty());

        User? o3 = new();
        Assert.True(o3.IsEmpty());

        User? o4 = new()
        {
            Id = "123"
        };
        Assert.False(o4.IsEmpty());

        Employee? o5 = new();
        Assert.True(o5.IsEmpty());

        Employee? o6 = new()
        {
            Sponsor = new()
        };
        Assert.True(o6.IsEmpty());

        Employee? o7 = new()
        {
            Sponsor = new()
            {
                Id = "",
            }
        };
        Assert.False(o7.IsEmpty());

        User? o8 = new()
        {
            Sponsor = new()
            {
                SocialAccounts = []
            }
        };
        Assert.True(o8.IsEmpty());

        User? o9 = new()
        {
            Sponsor = new()
            {
                Age = 0
            }
        };
        Assert.False(o9.IsEmpty());
    }
}
