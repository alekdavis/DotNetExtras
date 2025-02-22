using DotNetExtras.Security;

namespace SecurityTests;

public class CryptoTests
{
    [Theory]
    [InlineData("h", "world")]
    [InlineData("Hello, world!", "easy1234")]
    [InlineData("Не очень длинный текст.", "hello1234")]
    public void EncryptDecrypt
    (
        string plainText,
        string password
    )
    {
        string encryptedText, decryptedText;
        
        encryptedText = Crypto.Encrypt(plainText, password);
        Assert.NotEqual(plainText, encryptedText);

        decryptedText = Crypto.Decrypt(encryptedText, password);
        Assert.Equal(plainText, decryptedText);
    }
}