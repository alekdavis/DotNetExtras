using DotNetExtras.Security;
using System.Security.Claims;

namespace SecurityTests;
public class JwtTests
{
    [Theory]
    [InlineData("joe.doe@sample.com", "123", 60)]
    [InlineData("joe.doe@sample.com", "1234567890ABCDEF1234567890ABCDEF+", 900)]
    [InlineData("mary.jane@sample.com", "1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF-", 1200)]
    public void GenerateValidateToken_Success
    (
        string email,
        string secret,
        int tokenExpirationSeconds
    )
    {
        Jwt jwt = new(secret, tokenExpirationSeconds);

        string token = jwt.GenerateToken(email);

        ClaimsPrincipal principal = jwt.ValidateToken(token);

        Assert.NotNull(principal);
        Assert.Equal(email, principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value);
    }
    
    [Fact]
    public void GenerateValidateToken_ErrorExpired()
    {
        Jwt jwt = new("hello", 1);

        string token = jwt.GenerateToken("joe.doe@sample.com");
        Thread.Sleep(1001);

        Assert.ThrowsAny<Exception>(() => jwt.ValidateToken(token));
    }

    [Fact]
    public void ValidateToken_ErrorInvalidToken()
    {
        // ARRANGE
        Jwt jwt = new("hello", 900);

        string token = jwt.GenerateToken("joe.doe@intel.com");

        // JWT consists of 3 period separated parts.
        string[] tokenParts = token.Split('.');

        // Lets see if we make any one of the token parts or the whole token invalid. 
        string token1 = $"bad.tokenParts[1].tokenParts[2]";
        string token2 = $"tokenParts[0].bad.tokenParts[2]";
        string token3 = $"tokenParts[0].tokenParts[1].bad";
        string token4 = "bad";

        // ACT
        // ASSERT
        Assert.ThrowsAny<Exception>(() => jwt.ValidateToken(token1));
        Assert.ThrowsAny<Exception>(() => jwt.ValidateToken(token2));
        Assert.ThrowsAny<Exception>(() => jwt.ValidateToken(token3));
        Assert.ThrowsAny<Exception>(() => jwt.ValidateToken(token4));
    }
}
