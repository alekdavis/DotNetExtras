namespace DotNetExtras.Mail;

/// <summary>
/// Defines properties of the mail message.
/// </summary>
public interface IMailMessage
{
    /// <summary>
    /// Returns the language code of the mail message.
    /// </summary>
    public string? Language { get; }

    /// <summary>
    /// Returns the subject of the mail message.
    /// </summary>
    public string? Subject { get; }

    /// <summary>
    /// Returns the body of the mail message.
    /// </summary>
    public string? Body { get; }
}
