// Ignore Spelling: Auth

using DotNetExtras.Common.Exceptions;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Holds properties of the OAuth bearer token 
/// that can help determine when the token needs to be renewed.
/// </summary>
public class OAuthToken
{
    #region Public properties    
    /// <summary>
    /// Returns the valid from date/time (in UTC).
    /// </summary>
    /// <remarks>
    /// This value may be approximate.
    /// </remarks>
    public DateTime ValidFrom
    {
        get;
    }

    /// <summary>
    /// Returns the valid to date/time (in UTC).
    /// </summary>
    /// <remarks>
    /// This value may be approximate.
    /// </remarks>
    public DateTime ValidTo
    {
        get;
    }

    /// <summary>
    /// Returns the OAuth bearer token value.
    /// </summary>
    public string Value
    {
        get;
    }

    /// <summary>
    /// Returns the token seconds to live.
    /// </summary>
    /// <remarks>
    /// This value reflects the <code>ExpiresIn</code> property returned with the token.
    /// </remarks>
    public int SecondsToLive
    {
        get;
    }
    #endregion

    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthToken"/> class.
    /// </summary>
    /// <param name="oAuthResponse">
    /// The authentication response.
    /// </param>
    /// <param name="validFrom">
    /// The optional valid from date/time (if not specified, the current UTC date/time value will be used).
    /// </param>
    public OAuthToken
    (
        OAuthResponse oAuthResponse,
        DateTime? validFrom = null
    )
    {
        ArgumentNullException.ThrowIfNull(oAuthResponse);
        ArgumentNullException.ThrowIfNull(oAuthResponse.ExpiresIn);
        ArgumentNullException.ThrowIfNull(oAuthResponse.AccessToken);

        ValidFrom = validFrom == null ? DateTime.UtcNow : validFrom.Value;

        double expiresIn;

        try
        {
            expiresIn = double.Parse(oAuthResponse.ExpiresIn);
            SecondsToLive = int.Parse(oAuthResponse.ExpiresIn);
        }
        catch (Exception ex)
        {
            throw new SafeException(
                $"Cannot convert the string value of '{nameof(OAuthResponse.ExpiresIn)}' " +
                $"holding '{oAuthResponse.ExpiresIn}' to a number.", ex);
        }

        ValidTo = ValidFrom.AddSeconds(expiresIn);

        Value = oAuthResponse.AccessToken;
    }
    #endregion
}
