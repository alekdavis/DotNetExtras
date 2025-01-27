using System.Text.Json.Serialization;

namespace CommonLibTests.Models;
internal class User
{
    [JsonInclude]
    internal int? Age { get; set; }

    [JsonInclude]
    internal string? Id { get; set; }

    [JsonInclude]
    internal string? Mail { get; set; }

    [JsonInclude]
    internal string[]? OtherMail { get; set; }

    [JsonInclude]
    internal int[]? LuckyNumbers { get; set; }

    [JsonInclude]
    internal Name? Name { get; set; }

    [JsonInclude]
    internal DateTime? PasswordExpirationDate { get; set; }

    [JsonInclude]
    internal DateTimeOffset? LastLoginDateOffset { get; set; }

    [JsonInclude]
    internal Dictionary<string, SocialAccount>? SocialAccounts { get; set; }

    [JsonInclude]
    internal List<Phone>? Phones { get; set; }

    [JsonInclude]
    internal User? Sponsor { get; set; }

    [JsonInclude]
    internal Dictionary<string, string>? Tags { get; set; }
}
