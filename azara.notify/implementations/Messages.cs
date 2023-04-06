namespace azara.notify.implementations;

public class Messages : IMessages
{
    #region Object Declarations And Constructor

    private EmailConfigs EmailConfig { get; set; }

    public Messages(IOptions<EmailConfigs> EmailConfigOption) => this.EmailConfig = EmailConfigOption.Value;

    #endregion Object Declarations And Constructor

    /// <summary>
    /// Send an email.
    /// </summary>
    /// <param name="email">Received email id</param>
    /// <param name="subject">Mail Subject</param>
    /// <param name="body">Mail HTML body</param>
    public void SendEmail(string email, string subject, string body)
    {
        using SendEmailViaSmtp smtp = new(EmailConfig);
        smtp.Send(email, subject, body);
    }

    public void CallNotificationApi(string url, string methood, string balance, string userId)
    {
        var client = new WebClient();
        client.DownloadString(string.Format(url, methood, balance, userId));
    }

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}