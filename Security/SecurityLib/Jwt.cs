using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetExtras.Security;
/// <summary>
/// Class for generating and validating JWTs.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Jwt"/> class.
/// </remarks>
/// <param name="secret">
/// Secret from which the encryption key will be derived (encryption key will be extended to 256 characters or longer, which is the minimum).
/// </param>
/// <param name="expirationSeconds">
/// JWT expiration time in seconds.
/// </param>
public class Jwt
(
    string secret, 
    int expirationSeconds
)
{
    #region Private properties
    // Secret key used for token generation and validation
    private readonly string _key = ConvertTo256BitOrLonger(secret);

    // Expiration time in minutes for the generated tokens
    private readonly int _expirationSeconds = expirationSeconds;
    #endregion

    #region Public methods
    /// <summary>
    /// Generates a JWT for the specified user email.
    /// </summary>
    /// <param name="emailAddress">
    /// Email of the user for whom the token is generated.
    /// </param>
    /// <returns>
    /// Generated JWT.
    /// </returns>
    public string GenerateToken
    (
        string emailAddress
    )
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.UTF8.GetBytes(_key);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.Email, emailAddress)]),
            Expires = DateTime.UtcNow.AddSeconds(_expirationSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Validates a JWT.
    /// </summary>
    /// <param name="token">
    /// JWT to validate.
    /// </param>
    /// <returns>
    /// The <see cref="ClaimsPrincipal"/> object if the token is valid; otherwise, null.
    /// </returns>
    public ClaimsPrincipal ValidateToken
    (
        string token
    )
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.UTF8.GetBytes(_key);

        TokenValidationParameters validationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

        return claimsPrincipal;
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Extends string to match the minimum key length requirement for the symmetric key algorithm used by JWT.
    /// </summary>
    /// <param name="input">
    /// String that can be shorter than 256 bits..
    /// </param>
    /// <returns>
    /// String that is at least 256-bit long.
    /// </returns>
    private static string ConvertTo256BitOrLonger
    (
        string input
    )
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(input));

        int minLength = 256 / 8;

        string output = input;

        while (output.Length < minLength) 
        {
            output += input;
        }

        return output;
    }
    #endregion
}

