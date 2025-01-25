namespace CommonLibTests.Models;
internal class Employee
{
    internal string? Id { get; set; }

    internal Name? Name { get; set; }

    internal string? Title { get; set; }

    internal DateTime? ExpirationDate { get; set; }

    internal DateTimeOffset? ExpirationOffset { get; set; }

    internal Employee? Manager { get; set; }

    internal Employee? Sponsor { get; set; }
}
