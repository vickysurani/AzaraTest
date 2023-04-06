namespace azara.api.Controllers;

public class ChatController : BaseController
{
    #region Object Declaration And Constructor

    public ChatController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService CSVService) : base(
            Localizer,
            Crypto,
            DbContext,
            Mapper,
            FileManagerLogic,
            CSVService)
    {
    }

    #endregion Object Declaration And Constructor

    #region 1. User Chat Insert

    [Authorize, HttpPost(ActionsConsts.Chat.InsertChat)]
    public async Task<IActionResult> ChatInsertAsync(
        [FromBody] UserChatInsertRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new UserChatInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ChatHelpers(DbContext, Crypto);
        var response = await helper.ChatInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        if (request.AdminId.HasValue)
            await new NotificationHelpers(hubContext).SendSignalRData(NotificationTypeConsts.UserChat, new { Message = request.Message, Id = request.UserId });
        else
            await new NotificationHelpers(hubContext).SendSignalRData(NotificationTypeConsts.AdminChat, new { Message = request.Message, Id = request.UserId });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. User Chat Insert

    #region 2. Chat Get By Id

    [Authorize, HttpPost(ActionsConsts.Chat.ChatGetById)]
    public async Task<IActionResult> ChatGetByIdtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ChatHelpers(DbContext, Crypto);

        var response = await helper.ChatGetById(request);

        return OkResponse(response);
    }

    #endregion 2. Chat Get By Id

    #region 3. Chat List

    [Authorize, HttpPost(ActionsConsts.Chat.ChatList)]
    public async Task<IActionResult> GetChatListAsync([FromBody] ChatListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ChatHelpers(DbContext, Crypto);

        var response = await helper.GetChatListAsync(request);

        return OkResponse(response);
    }

    #endregion 3. Chat List

    #region 4. Admin Message Count
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Chat.AdminMessageCount)]
    public async Task<IActionResult> GetAdminMessageCountAsync()
    {
        Guid userId = GetUserId(User);
        if (userId == new Guid() && userId == Guid.Empty)
            return ErrorResponse();

        using var helper = new ChatHelpers(DbContext, Crypto);

        var response = await helper.GetAdminMessCount(userId);

        return OkResponse(response);
    }

    #endregion 4. Admin Message Count
}