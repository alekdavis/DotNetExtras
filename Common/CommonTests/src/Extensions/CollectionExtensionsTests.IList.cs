using CommonLibTests.Models;
using DotNetExtras.Common;

namespace CommonLibTests.Extensions;
public partial class CollectionExtensionsTests
{
    [Fact]
    public void RemoveMatching()
    {
        int count, total = 0, originalCount;

        List<User> users =
        [
            new() { Name = new() { GivenName = "Alice", Surname = "Wonder" }, Age = 10, Sponsor = new() { Id = "12345" }, },
            new() { Name = new() { GivenName = "Jack", Surname = "Black" }, Age = 13, Sponsor = new() { Id = "54321" }, },
            new() { Name = new() { GivenName = "Magic", Surname = "Seven" }, Age = 25, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Mary", Surname = "Jones" }, Age = 20, Sponsor = new() { Id = "12345" }, },
            new() { Name = new() { GivenName = "Swift", Surname = "Bracket" }, Age = 20, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Flint", Surname = "Eastwood" }, Age = 25, Sponsor = new() { Id = "13579" }, },
            new() { Name = new() { GivenName = "Magic", Surname = "Eight" }, Age = 25, Sponsor = new() { Id = "98765" }, },
        ];

        originalCount = users.Count;

        count = users.RemoveMatching(new User() { Name = new() { GivenName = "Alice" }});
        total += count;

        Assert.Equal(1, count);
        Assert.Equal(originalCount -  total, users.Count);
        Assert.DoesNotContain(users, u => u.Name?.GivenName == "Alice");

        count = users.RemoveMatching(new User() { Age = 25, Sponsor = new() { Id = "98765" } });
        total += count;

        Assert.Equal(2, count);
        Assert.Equal(originalCount -  total, users.Count);
        Assert.DoesNotContain(users, u => u.Age == 25 && u.Sponsor?.Id == "98765");
    }
}
