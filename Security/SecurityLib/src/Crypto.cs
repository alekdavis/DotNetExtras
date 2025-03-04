// Ignore Spelling: Crypto

using System.Security.Cryptography;
using System.Text;

namespace DotNetExtras.Security;
/// <summary>
/// Encrypts and decrypts text using the AES algorithm with 
/// 256-bit key,
/// 128-bit initialization vector,
/// 1000 password iterations
/// SHA-256 hash algorithm,
/// and random 64-bit salt.
/// </summary>
/// <remarks>
/// Adapted from ChatGPT-generated code.
/// </remarks>
/// <example>
/// <code>
/// string cipherText = Crypto.Encrypt("Hello, world!", "easy1234");
/// string plainText = Crypto.Decrypt(cipherText, "easy1234");
/// </code>
/// </example>
public class Crypto
{
    // The size of the key for AES encryption (256 bits).
    private const int _KEY_SIZE_IN_BYTES = 32;

    // The size of the initialization vector for AES encryption (128 bits).
    private const int _IV_SIZE_IN_BYTES = 16;

    // The number of iterations for the key generation.
    private const int _PASSWORD_ITERATIONS = 1000;

    // The hash algorithm to use for the key derivation.
    private const string _HASH_ALGORITHM = "SHA256";

    // The fixed size of the salt.
    // It will be appended at the beginning of the base64-encoded cipher text,
    // so make sure it is incremented by 4 (e.g. 4, 8, 12, 16, etc),
    // so the full string looks like a valid base64-encoded string.
    private const int SALT_SIZE_IN_CHARS = 8;
 
    /// <summary>
    /// Encrypts plain text using a 256-bit AES key and a 128-bit initialization vector (IV) 
    /// derived from the caller-provided password and random 8-character salt.
    /// </summary>
    /// <param name="plainText">
    /// Plain text to be encrypted.
    /// </param>
    /// <param name="password">
    /// Password from which the encryption key will be derived.
    /// </param>
    /// <returns>
    /// Base64-encoded ciphertext that starts with the salt value used for 
    /// the cryptographic key generation (the salt looks like base64-encoded text, too).
    /// </returns>
    /// <remarks>
    /// To generate the AES key, the code will use a random 8-character salt
    /// made of the base64-encoded characters. The salt will be added in front of the cipher text,
    /// so the returned result will look like a valid base64-encoded string.
    /// </remarks>
    public static string Encrypt
    (
        string plainText, 
        string password
    )
    {
        ArgumentNullException.ThrowIfNullOrEmpty(plainText);
        ArgumentNullException.ThrowIfNullOrEmpty(password);

        byte[] encrypted;
 
        // Generate a fixed 8-character salt
        string salt = CreateSalt(SALT_SIZE_IN_CHARS);
 
        // Generate the key and IV from the password and salt
        using (Rfc2898DeriveBytes keyDerivationFunction = CreateKey(password, salt))
        {
            byte[] keyBytes = keyDerivationFunction.GetBytes(_KEY_SIZE_IN_BYTES);
            byte[] ivBytes = keyDerivationFunction.GetBytes(_IV_SIZE_IN_BYTES);

            // Create an AES object with the derived Key and IV.
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                // Write all data to the stream.
                streamWriter.Write(plainText);
                streamWriter.Flush();
            }

            // THIS STATEMENT MUST BE OUTSIDE OF THE USING BLOCK SCOPE ABOVE!
            encrypted = memoryStream.ToArray();
        }
 
        // Return the encrypted bytes from the memory stream as a Base64 encoded string,
        // with the salt appended in plain text.
        return salt + Convert.ToBase64String(encrypted);
    }
 
    /// <summary>
    /// Decrypts cipher text using a 256-bit AES key and a 128-bit initialization vector (IV) 
    /// derived from the caller-provided password and random 8-character salt.
    /// </summary>
    /// <param name="cipherText">
    /// Cipher text to be decrypted.
    /// </param>
    /// <param name="password">
    /// Password from which the decryption key will be derived.
    /// </param>
    /// <returns>
    /// Plain text with the salt value removed.
    /// </returns>
    /// <remarks>
    /// To generate the AES key, the code must use the same password as the one used to encrypt
    /// plain text and the 8-character salt included in the beginning of the cipher text that
    /// was randomly generated and used for key generation during encryption.
    /// </remarks>
    public static string Decrypt
    (
        string cipherText, 
        string password
    )
    {
        ArgumentNullException.ThrowIfNullOrEmpty(cipherText);
        ArgumentNullException.ThrowIfNullOrEmpty(password);

        // Check arguments.
        if (cipherText.Length <= SALT_SIZE_IN_CHARS)
        {
            throw new ArgumentException("Cipher text cannot be shorter than salt.", nameof(cipherText));
        }

        // Extract the salt from the end of the encrypted text
        string salt = cipherText[..SALT_SIZE_IN_CHARS];

        // Extract the encrypted text without the salt
        string encryptedText = cipherText[SALT_SIZE_IN_CHARS..];
 
        // Convert the encrypted text from Base64 to byte array.
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        string plainText;
 
        // Generate the key and IV from the password and salt
        using (Rfc2898DeriveBytes keyDerivationFunction = CreateKey(password, salt))
        {
            byte[] keyBytes = keyDerivationFunction.GetBytes(_KEY_SIZE_IN_BYTES);
            byte[] ivBytes  = keyDerivationFunction.GetBytes(_IV_SIZE_IN_BYTES);

            // Create an AES object with the derived Key and IV.
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using MemoryStream memoryStream = new(cipherTextBytes);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.

            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plainText = streamReader.ReadToEnd();
        }
 
        return plainText;
    }
 
    /// <summary>
    /// Generates a predictable key from the given password and salt.
    /// </summary>
    /// <param name="password">
    /// Persistent password used to generate the key.
    /// </param>
    /// <param name="salt">
    /// Random salt used to generate the key.
    /// </param>
    /// <returns>
    /// Password-based key.
    /// </returns>
    private static Rfc2898DeriveBytes CreateKey
    (
        string password, 
        string salt
    )
    {
        return new Rfc2898DeriveBytes(
            password,
            Encoding.UTF8.GetBytes(salt),
            _PASSWORD_ITERATIONS,
            new HashAlgorithmName(_HASH_ALGORITHM)
        );
    }
 
    /// <summary>
    /// Generates a random salt of the specified length.
    /// </summary>
    /// <param name="length">
    /// Salt length in characters.
    /// </param>
    /// <returns>
    /// Salt value made of the characters in the base64 encoding character set.
    /// </returns>
    private static string CreateSalt
    (
        int length
    )
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(length))[..length];
    }
}
