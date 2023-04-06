namespace azara.notify.implementations;

public class SendEmailViaSmtp : IDisposable
{
    #region Object Declaration And Constructor

    private EmailConfigs emailConfig;

    public SendEmailViaSmtp(EmailConfigs emailConfigs) => this.emailConfig = emailConfigs;

    #endregion Object Declaration And Constructor

    #region Send

    public void Send(string receiverEmail, string subject, string body, string EmailCC = null, string EmailBCC = null)
    {
        if (string.IsNullOrEmpty(receiverEmail)) throw new Exception($"Email can not be null.");
        if (string.IsNullOrEmpty(emailConfig.SenderEmail)) throw new Exception($"Please specify the Sender Email in AppSettings.");
        if (string.IsNullOrEmpty(emailConfig.SmtpAddress)) throw new Exception($"Please specify the SMTP Address in AppSettings.");
        if (string.IsNullOrEmpty(emailConfig.Password)) throw new Exception($"Please specify the Password in AppSettings.");
        if (string.IsNullOrEmpty(emailConfig.Port.ToString())) throw new Exception($"Please specify the Port in AppSettings.");

        try
        {
            using var mail = new MailMessage
            {
                From = new MailAddress(emailConfig.SenderEmail, emailConfig.DisplayName)
            };

            // Send Emails to CC
            if (!string.IsNullOrEmpty(EmailCC))
            {
                var ccList = EmailCC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in ccList) mail.CC.Add(EmailCC);
            }

            // Send Emails to BCC
            if (!string.IsNullOrEmpty(EmailBCC))
            {
                var ccList = EmailBCC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in ccList) mail.Bcc.Add(EmailBCC);
            }

            mail.To.Add(receiverEmail);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            using SmtpClient smtp = new SmtpClient(emailConfig.SmtpAddress, emailConfig.Port);
            smtp.Credentials = new NetworkCredential(emailConfig.SenderEmail, emailConfig.Password);
            smtp.EnableSsl = emailConfig.EnableSSL;
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.ToString());
            //this.Logger.LogError(ex.Message, ex);
        }
    }

    #endregion Send

    #region House Keeping

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing) emailConfig = null;
    }

    #endregion House Keeping
}