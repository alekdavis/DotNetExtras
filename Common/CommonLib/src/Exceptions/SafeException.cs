namespace DotNetExtras.Common.Exceptions;
/// <summary>
/// Use the <see cref="SafeException"/> class as the base exception for your custom exception classes,
/// so you can easily recognize them in code.
/// This can be handy in a few cases.
/// For example, calling the <see cref="Extensions.Extensions.GetMessages{T}(Exception, bool)"/>
/// extension method passing <see cref="SafeException"/> as the generic type
/// (or calling the <see cref="Extensions.Extensions.GetSafeMessages(Exception, bool)"/>
/// extension method),
/// will only return messages from your custom exceptions
/// which can help you control the error details sent to the other apps
/// and make sure sensitive information is not leaked outside of your application.
/// </summary>
[Serializable]
public class SafeException: Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    public SafeException(): base() 
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    /// <param name="message">
    /// Error message.
    /// </param>
    public SafeException
    (
        string message
    )
    : base(message) 
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    /// <param name="message">
    /// Error message.
    /// </param>
    /// <param name="innerException">
    /// Inner exception.
    /// </param>
    public SafeException
    (
        string message, 
        Exception innerException
    )
    : base(message, innerException)
    {
    }
}