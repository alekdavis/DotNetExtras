// Ignore Spelling: Auth

using DotNetExtras.Common.Extensions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Handles creation and renewal of a single OAuth bearer token.
/// </summary>
/// <remarks>
/// <para>
/// Each instance if this class keeps track of a single access token.
/// If you need to use multiple tokens (for different endpoints or clients),
/// use multiple instances of this class.
/// </para>
/// <para>
/// The logic that determines token validity relies on the value of the 
/// <code>ExpiresIn</code> property returned when the token is generated and
/// the time of the request. Because the time of the request is not exact,
/// be aware that the calculated token expiration time may differ from 
/// the actual expiration time by a few seconds (or the time it took the 
/// request to complete). For that reason, by default the token gets 
/// refreshed within 5 minutes of the calculated expiration time
/// (you can overwrite the default threshold).
/// </para>
/// </remarks>
public class OAuthTokenService: IOAuthTokenService
{
    #region Private properties
    private readonly object     _accessTokenLock = new();
    private readonly object     _refreshLock     = new();
    private readonly ILogger?   _logger          = null;
    private readonly string?    _url             = null;
    private readonly string?    _clientId        = null;
    private readonly string[]?  _scopes          = null;
    private readonly bool?      _useProxy        = null;
    private readonly IWebProxy? _proxy           = null;
    private string?             _clientSecret    = null;
    private HttpClient?         _httpClient      = null;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthTokenService" /> class
    /// and makes a call to the specified token endpoint to generate the access token.
    /// </summary>
    /// <param name="settings">
    /// Configuration settings.
    /// </param>
    /// <param name="logger">
    /// Optional logger that can be used to log important operations and warnings.
    /// </param>
    public OAuthTokenService
    (
        OAuthTokenServiceSettings settings,
        ILogger? logger = null
    ) 
    : this
    (
        settings.Url, 
        settings.ClientId, 
        settings.ClientSecret, 
        settings.Scopes, 
        settings.UseProxy, 
        settings.Proxy, 
        settings.LazyLoad, 
        logger
    )
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthTokenService"/> class
    /// and makes a call to the specified token endpoint to generate an access token.
    /// </summary>
    /// <param name="url">
    /// URL of the token endpoint used to generate the access token.
    /// </param>
    /// <param name="clientId">
    /// ID of the client (must contain only ASCII characters; the colon character [:] is not allowed).
    /// </param>
    /// <param name="clientSecret">
    /// Client secret (must contain only ASCII characters).
    /// </param>
    /// <param name="scopes">
    /// Optional requested scopes.
    /// </param>
    /// <param name="useProxy">
    /// Indicates whether to use proxy.
    /// </param>
    /// <param name="proxy">
    /// Optional proxy.
    /// </param>
    /// <param name="lazyLoad">
    /// Indicates whether the token should be generated in constructor
    /// (the value of <code>false</code> is the default and means no).
    /// </param>
    /// <param name="logger">
    /// Optional logger that can be used to log important operations and warnings.
    /// </param>
    public OAuthTokenService
    (
        string?     url,
        string?     clientId,
        string?     clientSecret,
        string[]?   scopes          = null,
        bool?       useProxy        = null,
        IWebProxy?  proxy           = null,
        bool?       lazyLoad        = false,
        ILogger?    logger          = null
    )
    {
        _url            = url;
        _clientId       = clientId;
        _clientSecret   = clientSecret;
        _scopes         = scopes;
        _useProxy       = useProxy;
        _proxy          = proxy;
        _logger         = logger;

        if ((!lazyLoad.HasValue) || (!lazyLoad.Value))
        {
            Refresh();
        }
    }
    #endregion

    #region Public properties    
    /// <summary>
    /// Returns the current access token.
    /// </summary>
    public OAuthToken? AccessToken { get; private set; } = null;
    #endregion

    #region Public methods

    /// <inheritdoc cref="IOAuthTokenService.GetValidTokenValue(int, int)" path="summary|param|returns|remarks"/>
    public string? GetValidTokenValue
    (
        int secondsBeforeExpiration = 300,
        int secondsAfterCreation = 0
    )
    {
        string? tokenValue = GetValidToken(secondsBeforeExpiration, secondsAfterCreation)?.Value;

        return tokenValue;
    }

    /// <inheritdoc cref="IOAuthTokenService.GetValidTokenValue(int, int)" path="param|remarks"/>
    /// <summary>
    /// Returns a valid access token.
    /// </summary>
    /// <returns>
    /// Current or renewed access token.
    /// </returns>
    public OAuthToken? GetValidToken
    (
        int secondsBeforeExpiration = 300,
        int secondsAfterCreation = 0
    )
    {
        bool noAccessToken;

        lock (_accessTokenLock)
        {
            noAccessToken = AccessToken == null;
        }

        if (noAccessToken)
        {
            _logger?.LogDebug("Access token value is null.");

            Refresh();
        }

        if (!IsValidToken())
        {
            _logger?.LogDebug("Access token value is invalid.");

            Refresh();
        }
        else if (MustRenewToken(secondsBeforeExpiration, secondsAfterCreation))
        {
            try
            {
                _logger?.LogDebug("Access token must be refreshed.");

                Refresh();
            }
            catch (Exception ex)
            {
                string errMsg = "Cannot refresh access token.";

                if (IsTokenExpired())
                {
                    throw new OAuthException(errMsg, ex);
                }
                else
                {
                    // Do not treat it as an exception because the current token may still be good.
                    _logger?.LogWarning("{cannotRefreshAccessToken:l} {messages:l}", errMsg, ex.GetMessages());
                }
            }
        }

        return AccessToken;
    }

    /// <inheritdoc/>
    public void Refresh
    (
        string? clientSecret =  null
    )
    {
        if (clientSecret != null)
        {
            lock (_refreshLock)
            {
                _clientSecret = clientSecret;
            }
        }

        string? scope = null;

        if (_httpClient == null)
        {
            HttpClientHandler httpHandler = new() { UseDefaultCredentials = false };

            if (_useProxy.HasValue)
            {
                httpHandler.UseProxy = _useProxy.Value;
            }

            if (_proxy != null)
            {
                httpHandler.Proxy = _proxy;
                httpHandler.UseProxy = true;
            }

            _httpClient = new HttpClient(httpHandler);
        }

        _httpClient.DefaultRequestHeaders.Clear();
        //_httpClient.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        // We use ASCII encoding because some OAuth providers do not support UNICODE.
        _httpClient.DefaultRequestHeaders.Add(
            "Authorization",
            "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));

        List<KeyValuePair<string, string>> data =
        [
            new KeyValuePair<string,string>("grant_type", "client_credentials"),
        ];

        if (_scopes != null && _scopes.Length > 0)
        {
            scope = string.Join(" ", _scopes);
            data.Add(new KeyValuePair<string, string>("scope", scope));
        }

        FormUrlEncodedContent content = new(data);

        try
        {
            DateTime timestamp = DateTime.UtcNow;

            Task<HttpResponseMessage> postTask = _httpClient.PostAsync(_url, content);

            try
            {
                if (string.IsNullOrEmpty(scope))
                {
                    _logger?.LogDebug("Requesting access token for '{clientId:l}' with no scope.", _clientId);
                }
                else
                {
                    _logger?.LogDebug("Requesting access token for '{clientId:l}' with scope '{scope:l}'.", _clientId, scope);
                }

                _logger?.LogDebug("Sending a POST request with client credentials for '{clientId:l}' to '{url:l}'.", _clientId, _url);

                postTask.Wait();
            }
            catch (Exception ex)
            {
                throw new OAuthException(
                    "The POST operation failed.",
                    (ex is AggregateException && ex.InnerException != null) ? ex.InnerException : ex);
            }

            HttpResponseMessage response = postTask.Result;

            Task<string> readTask = response.Content.ReadAsStringAsync();

            try
            {
                _logger?.LogTrace("{message:l}", "Reading response content.");
                readTask.Wait();
            }
            catch (Exception ex)
            {
                throw new OAuthException(
                    "Cannot get response content from the POST operation.",
                    (ex is AggregateException && ex.InnerException != null) ? ex.InnerException : ex);
            }

            string result = readTask.Result;

            if (string.IsNullOrEmpty(result))
            {
                throw new OAuthException("Response content returned from the POST operation is empty.");
            }
            else
            {
                if (response.IsSuccessStatusCode)
                {
                    _logger?.LogTrace("{message:l}", "Successfully read response content.");

                    string errMsg = 
                        $"The POST operation succeeded but the returned response content cannot be deserialized: '{result}'.";

                    OAuthResponse? oauthResponse = result.FromJson<OAuthResponse>();

                    if (oauthResponse == null)
                    {
                        throw new OAuthException(errMsg);
                    }
                    else
                    {
                        try
                        {
                            _logger?.LogTrace("{message:l}", "Generating access token from the response content.");

                            lock(_accessTokenLock)
                            {
                                AccessToken = new OAuthToken(oauthResponse, timestamp);
                            }

                            _logger?.LogDebug("{message:l}", "Generated access token.");
                        }
                        catch (Exception ex)
                        {
                            throw new OAuthException(errMsg, ex);
                        }
                    }
                }
                else
                {
                    OAuthError oauthError = new(result);

                    if (oauthError == null)
                    {
                        throw new OAuthException(
                            $"The POST operation failed but the returned response content cannot be deserialized: '{result}'.");
                    }
                    else
                    {
                        // Need to specify URL in error message because this exception is re-thrown as-is.
                        throw new OAuthException(oauthError);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (_scopes == null || _scopes.Length == 0)
            {
                throw new OAuthException($"Cannot generate access token for '{_clientId}' from '{_url}'.", ex);
            }
            else
            {
                throw new OAuthException($"Cannot generate access token for '{_clientId}' with scope(s) '{string.Join(" ", _scopes)}' from '{_url}'.", ex);
            }
        }
    }

    /// <summary>
    /// Determines whether the token is valid 
    /// (i.e. within the threshold of the calculated lifetime).
    /// </summary>
    /// <returns>
    /// <code>true</code> if the token is valid; otherwise, <code>false</code>.
    /// </returns>
    public bool IsValidToken()
    {
        DateTime validFrom, validTo;

        lock (_accessTokenLock)
        {
            if (AccessToken == null)
            {
                return false;
            }

            validFrom   = AccessToken.ValidFrom;
            validTo     = AccessToken.ValidTo;
        }

        DateTime now = DateTime.UtcNow;

        return validFrom <= now && now <= validTo;
    }

    /// <summary>
    /// Checks if the current token must be renewed based on the token lifetime.
    /// </summary>
    /// <param name="secondsBeforeExpiration">
    /// Number of seconds before the calculated expiration when the token needs to be renewed.
    /// </param>
    /// <param name="secondsAfterCreation">
    /// Optional number of seconds after generation when the token must be renewed
    /// (when set to zero, which is the default, the setting will be ignored).
    /// </param>
    /// <returns>
    /// <code>true</code> if the token needs to be renewed; otherwise, <code>false</code>.
    /// </returns>
    public bool MustRenewToken
    (
        int secondsBeforeExpiration = 300,
        int secondsAfterCreation = 0
    )
    {
        DateTime validFrom, validTo;
        int secondsToLive;

        lock (_accessTokenLock)
        {
            if (AccessToken == null)
            {
                return true;
            }

            validFrom       = AccessToken.ValidFrom;
            secondsToLive   = AccessToken.SecondsToLive;
        }

        DateTime now = DateTime.UtcNow;

        lock (_accessTokenLock)
        {
        }

        if (secondsBeforeExpiration <= 0)
        {
            secondsBeforeExpiration = 0;
        }

        validTo = secondsAfterCreation > 0
            ? secondsToLive - secondsBeforeExpiration > secondsAfterCreation
                ? validFrom.AddSeconds(secondsAfterCreation)
                : validFrom.AddSeconds(secondsToLive - secondsBeforeExpiration)
            : validFrom.AddSeconds(secondsToLive - secondsBeforeExpiration);

        return !(validFrom <= now && now <= validTo);
    }

    /// <summary>
    /// Determines whether if the access token's calculated expiration time has passed.
    /// </summary>
    /// <returns>
    /// <code>true</code> if the current token is expired; otherwise, <code>false</code>.
    /// </returns>
    public bool IsTokenExpired()
    {
        DateTime validTo;

        lock (_accessTokenLock)
        {
            if (AccessToken == null)
            {
                return true;
            }

            validTo = AccessToken.ValidTo;
        }

        DateTime now = DateTime.UtcNow;

        return now > validTo;
    }
    #endregion
}
