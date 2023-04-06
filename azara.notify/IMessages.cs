namespace azara.notify;

public interface IMessages : IDisposable
{
    void SendEmail(string email, string subject, string body);

    void CallNotificationApi(string url, string methood, string balance, string userId);
}