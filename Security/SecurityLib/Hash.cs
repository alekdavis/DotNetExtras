using System.Security.Cryptography;
using System.Text;

namespace DotNetExtras.Security;
/// <summary>
/// Implements hashing functions.
/// </summary>
public static class Hash
{
    /// <summary>
    /// Holds hex hash string lengths.
    /// </summary>
    public static readonly Dictionary<HashType, int> HashStringLength = new()
    {
        { HashType.SHA256, 64 },
        { HashType.SHA384, 96 },
        { HashType.SHA512, 128 }
    };

    /// <summary>
    /// Hex hash string length for SHA384.
    /// </summary>
    public const int HexHashStringLengthSha384 = 96;

    /// <summary>
    /// Hex hash string length for SHA512.
    /// </summary>
    public const int HexHashStringLengthSha512 = 128;

    /// <summary>
    /// Creates a hash of plain text with optional salt and return is converted to a hex string.
    /// </summary>
    /// <param name="hashType">
    /// Hash algorithm.
    /// </param>
    /// <param name="plainText">
    /// Plain text.
    /// </param>
    /// <param name="salt">
    /// Random salt (to be added in front of the plain text before hashing).
    /// </param>
    /// <returns>
    /// Hashed value as a hex-encoded, lower-case string (without the plain text salt appended).
    /// </returns>
    public static string Generate
    (
        HashType hashType,
        string plainText,
        string? salt = null
    )
    {
        ArgumentNullException.ThrowIfNull(plainText);

        string hashString;

        if (!string.IsNullOrEmpty(salt))
        {
            plainText = salt + plainText;
        }

        using (HashAlgorithm hashAlgorithm = hashType == HashType.SHA256 
            ? SHA256.Create() 
            : hashType == HashType.SHA384 
                ? SHA384.Create() 
                : SHA512.Create())
        {
            byte[] plainTextBytes;
            byte[] hashBytes;

            plainTextBytes = plainText == null 
                ? ([]) 
                : Encoding.UTF8.GetBytes(plainText);

            hashBytes = hashAlgorithm.ComputeHash(plainTextBytes);
 
            hashString = string.Concat(Array.ConvertAll(hashBytes, h => h.ToString("x2")));
        }

        return hashString;
    }

    /// <summary>
    /// Generates a hash for a plain text string with appended optional random salt and 
    /// returns hash value encoded as hex string with appended salt.
    /// </summary>
    /// <param name="hashType">
    /// Hashing algorithm.
    /// </param>
    /// <param name="plainText">
    /// Plain text string.
    /// </param>
    /// <param name="saltLength">
    /// Length of the random salt (if 0 or negative, no salt will be used).
    /// </param>
    /// <param name="saveSalt">
    /// If set to <code>true</code> the hex encoded salt value will be added to the beginning of the hashed value.
    /// </param>
    /// <returns>
    /// Hashed string encoded as hex string which may start with the salt value (also hex encoded).
    /// </returns>
    public static string ToHash
    (
        this string? plainText,
        HashType hashType,
        int saltLength = 8,
        bool saveSalt = true
    )
    {
        ArgumentNullException.ThrowIfNull(plainText);

        string? salt    = null;
        string? saltHex = null;

        if (saltLength > 0)
        {
            salt = Password.Generate(saltLength);

            saltHex = string.Concat(Array.ConvertAll(Encoding.UTF8.GetBytes(salt ?? ""), h => h.ToString("x2")));
        }

        string hashValue = Generate(hashType, plainText, salt);

        return string.IsNullOrEmpty(saltHex) || (!saveSalt) 
            ? hashValue 
            : saltHex + hashValue;
    }

    /// <summary>
    /// Verifies a plain text string against a hash value.
    /// </summary>
    /// <param name="hashType">
    /// Hashing algorithm.
    /// </param>
    /// <param name="plainText">
    /// Plain text string to verify.
    /// </param>
    /// <param name="hashValue">
    /// Hash value to verify against.
    /// </param>
    /// <returns>
    /// True if the hash value matches the hash of the plain text string.
    /// </returns>
    public static bool Verify
    (
        HashType hashType,
        string plainText,
        string hashValue
    )
    {
        ArgumentNullException.ThrowIfNull(plainText);
        ArgumentNullException.ThrowIfNull(hashValue);

        string? salt = null;

        int hashStringLength = HashStringLength[hashType];
        int saltLength       = hashValue.Length - hashStringLength;

        if (saltLength > 0)
        {
            string? hexSalt = hashValue[..^hashStringLength];
            salt = HexToString(hexSalt);
            hashValue = hashValue[^hashStringLength..];
        }

        string hashValueToCheck = Hash.Generate(hashType, plainText, salt);
        return hashValue == hashValueToCheck;
    }

    /// <summary>
    /// Converts a hex string to a string.
    /// </summary>
    /// <param name="hexValue">
    /// Hex string to convert.
    /// </param>
    /// <returns>
    /// Converted string.
    /// </returns>
    private static string HexToString
    (
        string hexValue
    )
    {
        string strValue = "";

        while (hexValue.Length > 0)
        {
            strValue += System.Convert.ToChar(System.Convert.ToUInt32(hexValue[..2], 16)).ToString();
            hexValue = hexValue[2..];
        }

        return strValue;
    }
}
