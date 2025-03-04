// Ignore Spelling: Auth

using System.Net;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Holds settings used by the <see cref="OAuthTokenService"/> class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="OAuthTokenServiceSettings"/> class.
/// </remarks>
/// <param name="url">
/// The URL of the token endpoint.
/// </param>
/// <param name="clientId">
/// The client identifier.
/// </param>
/// <param name="clientSecret">
/// The client secret.
/// </param>
/// <param name="scopes">
/// The requested authorization scopes.
/// </param>
/// <param name="useProxy">
/// Indicates whether to use proxy (in case it needs to be turned off explicitly).
/// </param>
/// <param name="proxy">
/// The optional proxy settings.
/// </param>
/// <param name="lazyLoad">
/// Indicates whether the token should be generated in constructor
/// (the value of <code>false</code> is the default and means no).
/// </param>
public class OAuthTokenServiceSettings(
    string? url,
    string? clientId,
    string? clientSecret,
    string[]? scopes = null,
    bool? useProxy = null,
    WebProxy? proxy = null,
    bool? lazyLoad = false
    )
{
    /// <summary>
    /// The URL of the token endpoint.
    /// </summary>
    public string? Url { get; private set; } = url;

    /// <summary>
    /// The client identifier.
    /// </summary>
    public string? ClientId { get; private set; } = clientId;

    /// <summary>
    /// The client secret.
    /// </summary>
    public string? ClientSecret { get; private set; } = clientSecret;

    /// <summary>
    /// The requested authorization scopes.
    /// </summary>
    public string[]? Scopes { get; private set; } = scopes;

    /// <summary>
    /// Indicates whether to use proxy (in case it should not be used explicitly).
    /// </summary>
    public bool? UseProxy { get; private set; } = useProxy;

    /// <summary>
    /// The optional proxy settings.
    /// </summary>
    public IWebProxy? Proxy { get; private set; } = proxy;

    /// <summary>
    /// Indicates whether the token should be generated in constructor
    /// (the value of <code>false</code> is the default and means no).
    /// </summary>
    public bool LazyLoad { get; private set; } = lazyLoad ?? false;
}
