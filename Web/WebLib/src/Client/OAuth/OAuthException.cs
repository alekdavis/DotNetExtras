// Ignore Spelling: Auth

using DotNetExtras.Common.Exceptions;
using System.Text.RegularExpressions;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Encapsulates error details from failed OAuth requests.
/// </summary>
public partial class OAuthException: SafeException
{
    #region Private properties
    private static readonly string _defaultMessage = "OAuth error occurred.";
    #endregion

    #region Public properties
    /// <summary>
    /// Gets or sets the OAuth error object.
    /// </summary>
    public OAuthError? OAuthError
    {
        get; set;
    }
    #endregion

    #region Constructors 
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class.
    /// </summary>
    public OAuthException(): base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class 
    /// with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public OAuthException
    (
        string message
    )
    : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class 
    /// with a specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public OAuthException
    (
        string message,
        Exception innerException
    ) 
    : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class.
    /// </summary>
    /// <param name="message">
    /// Custom error message.
    /// </param>
    /// <param name="error">
    /// OAuth error object.
    /// </param>
    public OAuthException
    (
        string message,
        OAuthError error
    )
    : base(message)
    {
        OAuthError = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class.
    /// </summary>
    /// <param name="error">
    /// OAuth error object.
    /// </param>
    public OAuthException
    (
        OAuthError error
    )
    : base(FormatMessage(error))
    {
        Init(error);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthException"/> class.
    /// </summary>
    /// <param name="error">
    /// OAuth error object.
    /// </param>
    /// <param name="ex">
    /// Inner exception.
    /// </param>
    public OAuthException
    (
        OAuthError error,
        Exception ex
    )
    : base(FormatMessage(error), ex)
    {
        Init(error);
    }
    #endregion

    #region Private methods
    private void Init
    (
        OAuthError error
    )
    {
        OAuthError = error;

        if (!string.IsNullOrWhiteSpace(error?.Content))
        {
            Data.Add("Content", error.Content);
        }
    }

    /// <summary>
    /// Generates error message from an OAuth error object.
    /// </summary>
    /// <param name="error">
    /// OAuth error object.
    /// </param>
    /// <returns>
    /// Formatted error message.
    /// </returns>
    private static string FormatMessage
    (
        OAuthError error
    )
    {
        if (error == null)
        {
            return _defaultMessage;
        }

        if (string.IsNullOrEmpty(error.Error) &&
            string.IsNullOrEmpty(error.Description) &&
            !error.Code.HasValue)
        {
            return _defaultMessage;
        }

        if (string.IsNullOrEmpty(error.Error) &&
            string.IsNullOrEmpty(error.Description) &&
            error.Code.HasValue)
        {
            return $"Error code '{error.Code.Value}' returned.";
        }

        if (!string.IsNullOrEmpty(error.Error) &&
            !string.IsNullOrEmpty(error.Description))
        {
            string msg = $"Error '{error.Error}' returned: {error.Description}".TrimEnd();

            if (RegexEndsWithAlphaNumeric().IsMatch(msg))
            {
                msg += ".";
            }

            return msg;
        }

        return !string.IsNullOrEmpty(error.Error) 
            ? $"Error '{error.Error}' returned." 
            : error.Description ?? _defaultMessage;
    }

    [GeneratedRegex("[a-zA-Z0-9]$")]
    private static partial Regex RegexEndsWithAlphaNumeric();
    #endregion
}
