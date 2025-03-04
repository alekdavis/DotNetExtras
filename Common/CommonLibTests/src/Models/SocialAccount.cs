using System.Text.Json.Serialization;

namespace CommonLibTests.Models;
internal class SocialAccount
{
    [JsonInclude]
    internal string? Provider { get; set; }

    [JsonInclude]
    internal string? Account { get; set; }
 
    [JsonInclude]
    internal bool? Enabled { get; set; }
}
