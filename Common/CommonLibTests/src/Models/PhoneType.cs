using System.Text.Json.Serialization;

namespace CommonLibTests.Models;

internal enum PhoneType: int
{
    [JsonInclude]
    Personal = 0,
    [JsonInclude]
    Business = 1,
    [JsonInclude]
    Other = 2
}
