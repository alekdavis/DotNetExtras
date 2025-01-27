namespace DotNetExtras.Common;
/// <summary>
/// Base exception class that can be used to filter exceptions for the purpose of returning 
/// error information to the caller. If you derive an exception from this class,
/// then calling the <see cref="ExceptionExtensions.GetMessages{T}(Exception,bool)"/>
/// extension method passing <see cref="SafeException"/> as the generic type,
/// will only return messages from the exceptions derived from this class.
/// This can help you control the error details sent to the other apps,
/// and make sure no sensitive info is leaked outside of your app.
/// </summary>
public class SafeException: Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    public SafeException(): base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    /// <param name="message">
    /// Error message.
    /// </param>
    public SafeException(string message): base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    /// <param name="message">
    /// Error message.
    /// </param>
    /// <param name="innerException">
    /// Inner exception.
    /// </param>
    public SafeException(string message, Exception innerException): base(message, innerException) { }
}