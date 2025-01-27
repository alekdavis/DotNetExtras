using System.Text.Json.Serialization;

namespace CommonLibTests.Models;
internal class Phone
{
    [JsonInclude]
    internal PhoneType? Type { get; set; }

    [JsonInclude]
    internal bool? IsMobile { get; set; }

    [JsonInclude]
    internal string? Number { get; set; }

    [JsonInclude]
    internal bool? IsPrimary { get; set; }
}
