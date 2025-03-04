// Ignore Spelling: Auth

using System.Text.Json.Serialization;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Represents the access token object returned by an OAuth token endpoint 
/// per RFC 6749 (see https://oauth.net/2/).
/// </summary>
/// <remarks>
/// For more details, see 
/// <see href="https://www.rfc-editor.org/rfc/rfc6749">RFC 6749</see> 
/// and related documents.
/// </remarks>
public class OAuthResponse
{
    /// <summary>
    /// The access token issued by the authorization server.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// The type of the token issued,
    /// which in our case will be "bearer".
    /// </summary>
    /// <remarks>
    /// Value is case insensitive.
    /// </remarks>
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    /// <summary>
    /// The lifetime of the access token in seconds.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public string? ExpiresIn { get; set; }

    /// <summary>
    /// The refresh token, which can be used to obtain new
    /// access tokens using the same authorization grant.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    /// <summary>
    /// The Scope of the access token.
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
}
