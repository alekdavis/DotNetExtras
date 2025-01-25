namespace DotNetExtras.Exceptions;
/// <summary>
/// Base exception class that can be used to filter exceptions for the purpose of returning 
/// error information to the caller. If you derive an exception from this class,
/// then calling the <see cref="ExceptionExtensions.GetMessages{T}(Exception)"/>
/// extension method passing <see cref="SafeException"/> as the generic type,
/// will only return messages from the exceptions derived from this class.
/// This can help you control the error details sent to the other apps,
/// and make sure no sensitive info is leaked outside of your app.
/// </summary>
public class SafeException: Exception
{
    /// <inheritcoc/>
    public SafeException(): base() {}

    /// <inheritcoc/>
    public SafeException(string message): base(message) { }

    /// <inheritcoc/>
    public SafeException(string message, Exception innerException): base(message, innerException) { }
}