namespace azara.notify.implementations;

public class Android_FCM
{
    private ILogManager LogManager { get; set; }

    private PushConfigs PushConfig { get; set; }

    public Android_FCM(
        ILogManager LogManager,
        PushConfigs PushConfig)
    {
        this.LogManager = LogManager;
        this.PushConfig = PushConfig;
    }

    ///<summary>
    ///Send push notification to android device
    ///</summary>
    /// <param name="data">Data in Json format</param>
    /// <param name="token"></param>
    public void Send(string data, string token, string notificationType)
    {
        try
        {
            WebRequest tRequest = WebRequest.Create(PushConfig.Android.HostName);
            tRequest.Method = "POST";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", PushConfig.Android.ApplicationId));
            tRequest.Headers.Add(string.Format("Sender: id={0}", PushConfig.Android.SenderId));

            var payload = new
            {
                to = token,
                token = token,
                data = new
                {
                    notificationType = notificationType,
                    title = PushConfig.Android.AppName,
                    body = data,
                }
            };

            string postbody = System.Text.Json.JsonSerializer.Serialize(payload).ToString();

            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;

            using Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            using WebResponse tResponse = tRequest.GetResponse();
            using Stream dataStreamResponse = tResponse.GetResponseStream();
            using StreamReader tReader = new StreamReader(dataStreamResponse);
            string sResponseFromServer = tReader.ReadToEnd();
            string str = sResponseFromServer;
        }
        catch (Exception ex)
        {
            this.LogManager.LogError(System.Text.Json.JsonSerializer.Serialize(new { class_name = this.GetType().Name, exception = ex }));
        }
    }
}