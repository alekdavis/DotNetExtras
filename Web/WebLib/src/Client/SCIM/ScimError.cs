// Ignore Spelling: Scim
using System.Text.Json.Serialization;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Error returned by IAM Web Services.
/// </summary>
public class ScimError
{
    /// <summary>
    /// Error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// The standard HTTP
    /// <see href="http://en.wikipedia.org/wiki/List_of_HTTP_status_codes" target="_blank">status code</see>
    /// defined by the HTTP specification.
    /// </summary>
    [JsonPropertyName("code")]
	public int? Code { get; set; }

    /// <summary>
    /// The service code that is more granular than the one returned by the 
    /// <see cref="Code"/> property.
    /// This service code must be defined by the Service Provider instead of the HTTP specification.
    /// A client can check the value of this parameter to detect certain conditions.
    /// The string value is language independent.
    /// </summary>
    [JsonPropertyName("serviceCode")]
	public string? ServiceCode { get; set; }

	/// <summary>
	/// The error code returned from the backend system called by the web service.
	/// </summary>
	[JsonPropertyName("sourceSystemCode")]
	public string? SourceSystemCode { get; set; }

	/// <summary>
	/// The error description returned from the backend system called by the web service.  
	/// </summary>
	[JsonPropertyName("sourceSystemMessage")]
	public string? SourceSystemMessage { get; set; }

	/// <summary>
	/// The identifier used to trace transactions through the log files and disarticulated database systems.
	/// It may be left blank for some errors.
	/// </summary>
	[JsonPropertyName("requestId")]
	public string? RequestId { get; set; }
}
