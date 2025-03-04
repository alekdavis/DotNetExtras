// Ignore Spelling: Auth

namespace DotNetExtras.Web.Client;

/// <summary>
/// Defines simple method to get a valid bearer token.
/// </summary>
public interface IOAuthTokenService
{
    /// <summary>
    /// Returns a valid access token string value.
    /// </summary>
    /// <param name="secondsBeforeExpiration">
    /// Number of seconds before the calculated expiration when the token needs to be renewed.
    /// </param>
    /// <param name="secondsAfterCreation">
    /// Optional number of seconds after generation when the token must be renewed
    /// (when set to zero, which is the default, the setting will be ignored).
    /// </param>
    /// <returns>
    /// Current or renewed access token value.
    /// </returns>
    /// <remarks>
    /// This function checks the calculated token expiration and if it is within the 
    /// allowed threshold, it will return the existing token. 
    /// If the expiration is beyond the threshold,
    /// the function will attempt to renew the token. 
    /// If the token renewal attempt fails,
    /// the function will check if the token is past the expiration and if so,
    /// it will throw an exception; if not, it will return the existing token,
    /// because it may still be good.
    /// </remarks>
    string? GetValidTokenValue
    (
        int secondsBeforeExpiration = 300,
        int secondsAfterCreation = 0
    );

    /// <summary>
    /// Generates a new access token.
    /// </summary>
    /// <param name="clientSecret">
    /// In case client secrets needs to be renewed, it can be passed to this method.
    /// </param>
    void Refresh(string? clientSecret = null);
}
