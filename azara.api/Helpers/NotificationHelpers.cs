using azara.models.Responses.Notification.Response;

namespace azara.api.Helpers;

public class NotificationHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public NotificationHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
    }

    #endregion Constructor

    #region Object Declarations And Constructor

    private IHubContext<SignalRHelpers> HubContext { get; set; }

    public NotificationHelpers(IHubContext<SignalRHelpers> HubContext = null)
    {
        this.HubContext = HubContext;
    }

    #endregion Object Declarations And Constructor

    #region Send Web Notification

    public async Task SendWebNotifications(
        string type,
        string message,
        string userIdCsv = null)
    {
        using var signalRHelp = new SignalRHelpers(HubContext);

        if (string.IsNullOrEmpty(userIdCsv))
        {
            await signalRHelp.SendMessage(type, new { message = message });
            return;
        }

        var userIdList = userIdCsv.Split(",");
        foreach (var userId in userIdList)
        {
            await signalRHelp.SendMessage(type, new { message = message, user = userId.Trim() });
        }
    }

    public async Task SendSignalRData(string type, object data)
    {
        using var signalRHelp = new SignalRHelpers(HubContext);
        await signalRHelp.SendMessage(type, data);
    }

    #endregion Send Web Notification

    #region Send User Inbox Notifications

    public async Task SendInboxNotifications(
        string messageType,
        string message,
        string userIdCsv
        )
    {
        using var signalRHelp = new SignalRHelpers(HubContext);

        var userIdList = userIdCsv.Split(",");
        foreach (var userId in userIdList)
        {
            await signalRHelp.SendMessage(NotificationTypeConsts.UserInbox,
                new
                {
                    type = messageType,
                    message = message,
                    user = userId.Trim()
                });
        }
    }

    #endregion Send User Inbox Notifications

    #region Get Notification By UserID 
    public async Task<List<NotificationGetByIdResponse>> GetNotificationGetById([FromBody] BaseIdRequest request)
    {
        var notification =  await (from n in DbContext.Notification
                                   where(n.UserId == request.Id)
                                   
        select new NotificationGetByIdResponse { 
            Title = n.Title,
            Description = n.Description,
            ReadableTime = n.ReadableTime,
            UserId = n.Id,
            Icon = n.Icon,
                                   
        }).OrderByDescending(x => x.ReadableTime).ToListAsync();
        
        return notification;
    } 
    #endregion

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}