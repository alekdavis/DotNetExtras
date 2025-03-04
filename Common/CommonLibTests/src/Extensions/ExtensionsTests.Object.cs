using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_IsEmpty()
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

    [Fact]
    public void Object_ToDynamic()
    {
        User user = new()
        {
            Name = new()
            {
                GivenName = "John",
                Surname = "Doe"
            },
            Age = 42,
            Mail = "John.Doe@mail.com",
            OtherMail = ["DoeJohn@mail.com"],
            Phones =
            [
                new() { Number = "123-456-7890", Type = PhoneType.Personal },
                new() { Number = "987-654-3210", Type = PhoneType.Business },
            ],
        };

        Dictionary<string, object> extras = new()
        {
            { "ExtraProperty", "XYZ" }
        };

        dynamic? result = user.ToDynamic(extras);

        Assert.Equal("John", result?.Name.GivenName);
        Assert.Equal("Doe", result?.Name.Surname);
        Assert.Equal(42, result?.Age);
        Assert.Equal("John.Doe@mail.com", result?.Mail);
        Assert.Equal("DoeJohn@mail.com", result?.OtherMail[0]);
        Assert.Equal("123-456-7890", result?.Phones[0].Number);
        Assert.Equal(PhoneType.Personal, result?.Phones[0].Type);
        Assert.Equal("987-654-3210", result?.Phones[1].Number);
        Assert.Equal(PhoneType.Business, result?.Phones[1].Type);
        Assert.Equal("XYZ", result?.ExtraProperty);
    }
}
