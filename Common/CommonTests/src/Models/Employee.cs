using System.Text.Json.Serialization;

namespace CommonLibTests.Models;
internal class Employee
{
    [JsonInclude]
    internal string? Id { get; set; }

    [JsonInclude]
    internal Name? Name { get; set; }

    [JsonInclude]
    internal string? Title { get; set; }

    [JsonInclude]
    internal DateTime? ExpirationDate { get; set; }

    [JsonInclude]
    internal DateTimeOffset? ExpirationOffset { get; set; }

    [JsonInclude]
    internal Employee? Manager { get; set; }

    [JsonInclude]
    internal Employee? Sponsor { get; set; }
}
