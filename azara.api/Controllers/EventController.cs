namespace azara.api.Controllers;

public class EventController : BaseController
{
    #region Object Declaration And Constructor

    public EventController(
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

    #region 1. Event Insert

    [Authorize, HttpPost(ActionsConsts.Event.InsertEvent)]
    public async Task<IActionResult> EventInsertAsync(
        [FromBody] EventInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new EventInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);
        var response = await helper.InsertEvent(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //var helperResponse = JsonConvert.DeserializeObject<EventListResponse>(response.Data);


        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.EventInsert,
            new
            {
                request.Id,
                request.Name,
                request.Image,
                request.EventDate,
                request.EventStartTime,
                request.EventEndTime,
                request.EventLocation,
                request.Description,
                request.Latitude,
                request.Longitude,
                Active = true,
                Modified = DateTime.UtcNow
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Event Insert

    #region 1. Event Update

    [Authorize, HttpPost(ActionsConsts.Event.UpdateEvent)]
    public async Task<IActionResult> EventUpdateAsync(
        [FromBody] EventInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new EventInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);
        var response = await helper.UpdateEvent(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.EventUpdate,
            new
            {
                request.Id,
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Event Update

    #region 3. Event Delete

    [Authorize, HttpPost(ActionsConsts.Event.DeleteEvent)]
    public async Task<IActionResult> EventDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new BaseIdRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);
        var response = await helper.DeleteEvent(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.EventDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 3. Event Delete

    #region 4. Get Get List

    [HttpPost(ActionsConsts.Event.GetEvents)]
    public async Task<IActionResult> EventGetListAsync([FromBody] EventGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);

        var response = await helper.EventGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Get List

    #region 5. Event Get By Id

    [HttpPost(ActionsConsts.Event.GetEventById)]
    public async Task<IActionResult> EventGetBtyIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);

        var response = await helper.EventGetById(request);

        return OkResponse(response);
    }

    #endregion 5. Event Get By Id

    #region 6. Event status Update

    [Authorize, HttpPost(ActionsConsts.Event.DeleteStatusUpdate)]
    public async Task<IActionResult> EventStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);

        var response = await helper.EventStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.EventStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 6. Event status Update

    #region 7. Event List without Pagination

    [HttpPost(ActionsConsts.Event.EventList)]
    public async Task<IActionResult> EventGet([FromBody] EventListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);

        var response = await helper.EventList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Store List without Pagination

    #region 8. Events Insert As PerUserId

    [Authorize, HttpPost(ActionsConsts.Event.InsertUserEvents)]
    public async Task<IActionResult> InsertUserEventsAsync([FromBody] EventUsersIntertRequest request)
    {
        if (request == null) request = new EventUsersIntertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);
        var response = await helper.UserEventsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion

    #region 9. Events details Get By UserId

    [Authorize, HttpPost(ActionsConsts.Event.EventsDetailsGetByUserId)]
    public async Task<IActionResult> EventsDetailsGetByUserIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new EventHelpers(DbContext, Crypto, Mapper);

        var response = await helper.EventDetailsGetByUserId(request);

        return OkResponse(response);
    }

    #endregion
}