using System.Security.Cryptography;

namespace DotNetExtras.Security;

/// <summary>
/// Generates random password, which will not include ambiguous characters, 
/// such as I, l, and 1. The generated password will be made of 7-bit ASCII symbols. 
/// very four characters will include:
/// - one lower case character
/// - one upper case character
/// - one number
/// - one special symbol (such as '%') 
/// in a random order. 
/// The password will always start with an alpha-numeric character; 
/// it will not start with a special symbol 
/// (because some back-end systems do not like certain special
/// characters in the first position).
/// </summary>
/// <remarks>
/// Adapted from 
/// https://github.com/obviex/Samples/blob/master/Password.md
/// </remarks>
public static class Password
{
    // Define default min and max password lengths.
    private static readonly int _DEFAULT_MIN_PASSWORD_LENGTH  = 8;
    private static readonly int _DEFAULT_MAX_PASSWORD_LENGTH  = 12;

    // Define supported password characters divided into groups.
    // You can add (or remove) characters to (from) these groups.
    private static readonly string _PASSWORD_CHARS_LCASE   = "abcdefgijkmnopqrstwxyz";
    private static readonly string _PASSWORD_CHARS_UCASE   = "ABCDEFGHJKLMNPQRSTWXYZ";
    private static readonly string _PASSWORD_CHARS_NUMERIC = "23456789";
    private static readonly string _PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/[].,':;~()";

    /// <summary>
    /// Generates a random password.
    /// </summary>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    /// <remarks>
    /// The length of the generated password will be determined at
    /// random. It will be no shorter than the minimum default and
    /// no longer than maximum default.
    /// </remarks>
    public static string Generate()
    {
        return Generate(_DEFAULT_MIN_PASSWORD_LENGTH, _DEFAULT_MAX_PASSWORD_LENGTH);
    }

    /// <summary>
    /// Generates a random password of the exact length.
    /// </summary>
    /// <param name="length">
    /// Exact password length.
    /// </param>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    public static string Generate
    (
        int length
    )
    {
        return Generate(length, length);
    }

    /// <summary>
    /// Generates a random password between the min and max length.
    /// </summary>
    /// <param name="minLength">
    /// Minimum password length.
    /// </param>
    /// <param name="maxLength">
    /// Maximum password length.
    /// </param>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    /// <remarks>
    /// The length of the generated password will be determined at
    /// random and it will fall with the range determined by the
    /// function parameters.
    /// </remarks>
    public static string Generate
    (
        int minLength,
        int maxLength
    )
    {
        // Make sure that input parameters are valid.
        if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
        {
            throw new ArgumentException("Min and max length values must be greater than zero, and max must be greater than min.");
        }

        // Create a local array containing supported password characters
        // grouped by types. You can remove character groups from this
        // array, but doing so will weaken the password strength.
        char[][] charGroups =
        [
            _PASSWORD_CHARS_LCASE.ToCharArray(),
            _PASSWORD_CHARS_UCASE.ToCharArray(),
            _PASSWORD_CHARS_NUMERIC.ToCharArray(),
            _PASSWORD_CHARS_SPECIAL.ToCharArray()
        ];

        // Use this array to track the number of unused characters in each
        // character group.
        int[] charsLeftInGroup = new int[charGroups.Length];

        // Initially, all characters in each group are not used.
        for (int i=0; i<charsLeftInGroup.Length; i++)
        {
            charsLeftInGroup[i] = charGroups[i].Length;
        }

        // Use this array to track (iterate through) unused character groups.
        int[] leftGroupsOrder = new int[charGroups.Length];

        // Initially, all character groups are not used.
        for (int i=0; i<leftGroupsOrder.Length; i++)
        {
            leftGroupsOrder[i] = i;
        }

        // Because we cannot use the default randomizer, which is based on the
        // current time (it will produce the same "random" number within a
        // second), we will use a random number generator to seed the
        // randomizer.

        // Use a 4-byte array to fill it with random bytes and convert it then
        // to an integer value.
        byte[] randomBytes = new byte[4];

        // Generate 4 random bytes.
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
             rng.GetBytes(randomBytes);
        }

        // Convert 4 bytes into a 32-bit integer value.
        int seed = BitConverter.ToInt32(randomBytes, 0);

        // Now, this is real randomization.
        Random  random  = new(seed);

        // This array will hold password characters.
        char[]? password = minLength < maxLength 
            ? (new char[random.Next(minLength, maxLength+1)]) 
            : (new char[minLength]);

        // Allocate appropriate memory for the password.

        // Index of the next character to be added to password.
        int nextCharIdx;
        
        // Index of the next character group to be processed.
        int nextGroupIdx;

        // Index which will be used to track not processed character groups.
        int nextLeftGroupsOrderIdx;
        
        // Index of the last non-processed character in a group.
        int lastCharIdx;

        // Index of the last non-processed group.
        int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
        
        // Generate password characters one at a time.
        for (int i=0; i<password.Length; i++)
        {
            // If only one character group remained unprocessed, process it;
            // otherwise, pick a random character group from the unprocessed
            // group list. To allow a special character to appear in the
            // first position, increment the second parameter of the Next
            // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
            nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0
                ? 0
                : random.Next(0, lastLeftGroupsOrderIdx);

            // Get the actual index of the character group, from which we will
            // pick the next character.
            nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

            // Get the index of the last unprocessed characters in this group.
            lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;
            
            // If only one unprocessed character is left, pick it; otherwise,
            // get a random character from the unused character list.
            nextCharIdx = lastCharIdx == 0 
                ? 0 
                : random.Next(0, lastCharIdx+1);

            // Add this character to the password.
            password[i] = charGroups[nextGroupIdx][nextCharIdx];
            
            // If we processed the last character in this group, start over.
            if (lastCharIdx == 0)
            {
                charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
            }
            // There are more unprocessed characters left.
            else
            {
                // Swap processed character with the last unprocessed character
                // so that we don't pick it until we process all characters in
                // this group.
                if (lastCharIdx != nextCharIdx)
                {
                    (
                        charGroups[nextGroupIdx][nextCharIdx], 
                        charGroups[nextGroupIdx][lastCharIdx]
                    ) 
                    = 
                    (
                        charGroups[nextGroupIdx][lastCharIdx], 
                        charGroups[nextGroupIdx][nextCharIdx]
                    );
                }
                // Decrement the number of unprocessed characters in
                // this group.
                charsLeftInGroup[nextGroupIdx]--;
            }

            // If we processed the last group, start all over.
            if (lastLeftGroupsOrderIdx == 0)
            {
                lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            }
            // There are more unprocessed groups left.
            else
            {
                // Swap processed group with the last unprocessed group
                // so that we don't pick it until we process all groups.
                if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                {
                    (
                        leftGroupsOrder[nextLeftGroupsOrderIdx], 
                        leftGroupsOrder[lastLeftGroupsOrderIdx]
                    ) 
                    = 
                    (
                        leftGroupsOrder[lastLeftGroupsOrderIdx], 
                        leftGroupsOrder[nextLeftGroupsOrderIdx]
                    );
                }

                // Decrement the number of unprocessed groups.
                lastLeftGroupsOrderIdx--;
            }
        }

        // Convert password characters into a string and return the result.
        return new string(password);
     }
}

