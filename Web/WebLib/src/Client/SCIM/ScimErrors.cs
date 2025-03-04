// Ignore Spelling: Scim
using System.Text.Json.Serialization;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Error returned by IAM Web Services.
/// </summary>
public class ScimErrors
{
	/// <summary>
	/// A list of SCIM errors. 
	/// </summary>
	[JsonPropertyName("errors")]
	public List<ScimError>? Errors { get; set; }
}
