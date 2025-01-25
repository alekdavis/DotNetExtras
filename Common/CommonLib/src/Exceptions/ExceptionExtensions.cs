using DotNetExtras.Text;
using System.Reflection;
using System.Text;

namespace DotNetExtras.Exceptions;

/// <summary>
/// Implements extension methods applicable to exceptions.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Gets messages from the immediate and inner exceptions as long as they 
    /// are derived from the specified type.
    /// </summary>
    /// <param name="ex">
    /// Immediate exception.
    /// </param>
    /// <typeparam name="T">
    /// Base type of the exception that will be included in the error message.
    /// </typeparam>
    /// <returns>
    /// Complete error message.
    /// </returns>
    /// <remarks>
    /// Use the <c>T</c> type to identify derivatives of which exceptions will be
    /// included in the complete error message.
    /// This can be useful if you only want to filter out system and third-party
    /// exceptions out of the error message and only include your own.
    /// For example, say you have <c>MyBaseException</c> and a number of exceptions
    /// that derive from it.
    /// If you specify the <c>MyBaseException</c> type as the generic type <c>T</c>,
    /// then only <c>MyBaseException</c> and exceptions derived from it will be
    /// included in the error message.
    /// </remarks>
    public static string GetMessages<T>
    (
        this Exception ex
    )
    where T : Exception
    {
        Exception? e = ex;

        if (e == null)
        {
            return "";
        }

        StringBuilder messages = new();

        if (e is AggregateException ae)
        {
            foreach (Exception ie in ae.Flatten().InnerExceptions)
            {
                if (messages.Length > 0)
                {
                    messages.Append(' ');
                }

                messages.Append(ie.GetMessages<T>());
            }
        }
        else
        {
            if (e is TargetInvocationException && e.InnerException != null)
            {
                e = e.InnerException;
            }

            while (e != null) 
            {
                if (e is T)
                {
                    string message = e.Message.ToSentence() ?? "";

                    if (!messages.ToString().EndsWith(message))
                    {
                        if (messages.Length > 0)
                        {
                            messages.Append(' ');
                        }

                        messages.Append(message);
                    }
                }

                e = e.InnerException;
            }
        }

        return messages.ToString();
    }

    /// <summary>
    /// Gets messages from the immediate and all inner exceptions.
    /// </summary>
    /// <param name="ex">
    /// Immediate exception.
    /// </param>
    /// <returns>
    /// Complete error message.
    /// </returns>
    public static string GetMessages
    (
        this Exception ex
    )
    {
        return ex.GetMessages<Exception>();
    }

    /// <summary>
    /// Gets messages from the immediate and all inner exceptions.
    /// </summary>
    /// <param name="ex">
    /// Immediate exception.
    /// </param>
    /// <returns>
    /// Complete error message.
    /// </returns>
    public static string GetSafeMessages
    (
        this Exception ex
    )
    {
        return ex.GetMessages<SafeException>();
    }
}
