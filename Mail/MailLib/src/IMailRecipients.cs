namespace DotNetExtras.Mail;
/// <summary>
/// Encapsulates lists of email message addressees.
/// </summary>
public interface IMailRecipients
{
    /// <summary>
    /// Email To addresses.
    /// </summary>
    public IList<string>? To { get; set; }

    /// <summary>
    /// Email CC addresses.
    /// </summary>
    public IList<string>? Cc { get; set; }

    /// <summary>
    /// Email BCC addresses.
    /// </summary>
    public IList<string>? Bcc { get; set; }
}
