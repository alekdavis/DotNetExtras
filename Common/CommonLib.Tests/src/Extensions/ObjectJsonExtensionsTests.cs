using CommonLibTests.Models;
using DotNetExtras.Extensions;
using DotNetExtras.Extensions.Json;

namespace CommonLibTests.Extensions.Json;
public class ObjectJsonExtensionsTests
{
    [Fact]
    public void ToJsonFromJson()
    {
        User? u1 = null;
        string json = u1.ToJson();

        Assert.Equal("", json);

        u1 = new()
        {
            Id = "12345",
            Name = new()
            {
                GivenName = "Joe",
                Surname = "Doe"
            },
            Age = 31,
            Sponsor = new()
            {
                Id = "54321"
            },
            SocialAccounts = new()
            {
                ["facebook"] = new() { Account = "Joe.Doe@mail.com" },
            },
            Phones =
            [
                new() { Number = "123-456-7890", Type = PhoneType.Personal },
                new() { Number = "987-654-3210", Type = PhoneType.Business },
            ],

            PasswordExpirationDate = new (2031, 11, 30, 19, 15, 33),
            LastLoginDateOffset = new(new DateTime(2021, 10, 12, 20, 33, 41), new TimeSpan(3, 30, 0)),
        };

        json = u1.ToJson();

        /*
        {"age":31,"id":"12345","name":{"surname":"Doe","givenName":"Joe"},"passwordExpirationDate":"2031-11-30T19:15:33","lastLoginDateOffset":"2021-10-12T20:33:41+03:30","socialAccounts":{"facebook":{"account":"Joe.Doe@mail.com"}},"phones":[{"type":"Personal","number":"123-456-7890"},{"type":"Business","number":"987-654-3210"}],"sponsor":{"id":"54321"}}
         */
        Assert.Contains("\"age\":31", json);
        Assert.Contains("\"id\":\"12345\"", json);
        Assert.Contains("\"name\":{\"surname\":\"Doe\",\"givenName\":\"Joe\"}", json);
        Assert.Contains("\"passwordExpirationDate\":\"2031-11-30T19:15:33\"", json);
        Assert.Contains("\"lastLoginDateOffset\":\"2021-10-12T20:33:41+03:30\"", json);
        Assert.Contains("\"socialAccounts\":{\"facebook\":{\"account\":\"Joe.Doe@mail.com\"}}", json);
        Assert.Contains("\"phones\":[{\"type\":\"Personal\",\"number\":\"123-456-7890\"},{\"type\":\"Business\",\"number\":\"987-654-3210\"}]", json);
        Assert.Contains("\"sponsor\":{\"id\":\"54321\"", json);

        User? u2 = json.FromJson<User>();

        Assert.NotNull(u2);
        Assert.Equivalent(u1, u2);
    }
}
