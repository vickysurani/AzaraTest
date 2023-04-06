namespace azara.api.Helpers;

public class SignalRHelpers : Hub
{
    #region Object Declarations And Constructor

    private IHubContext<SignalRHelpers> HubContext { get; set; }

    public SignalRHelpers(IHubContext<SignalRHelpers> HubContext)
    {
        this.HubContext = HubContext;
    }

    #endregion Object Declarations And Constructor

    #region Send Message

    public async Task SendMessage(
        string type,
        object data)
    {
        await HubContext.Clients.All.SendAsync(type, data);
    }

    #endregion Send Message
}