namespace CommonLibTests.Models;
internal class User
{
    internal int? Age { get; set; }

    internal string? Id { get; set; }

    internal string? Mail { get; set; }

    internal string[]? OtherMail { get; set; }

    internal int[]? LuckyNumbers { get; set; }

    internal Name? Name { get; set; }

    internal DateTime? PasswordExpirationDate { get; set; }

    internal Dictionary<string, SocialAccount>? SocialAccounts { get; set; }

    internal List<Phone>? Phones { get; set; }

    internal User? Sponsor { get; set; }

    internal Dictionary<string, string>? Tags { get; set; }
}
