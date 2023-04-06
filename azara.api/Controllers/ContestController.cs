namespace azara.api.Controllers;

public class ContestController : BaseController
{
    #region Object Declaration And Constructor

    public ContestController(
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

    #region 1. Contest Insert

    [Authorize, HttpPost(ActionsConsts.Contest.InsertContest)]
    public async Task<IActionResult> ContestInsertAsync(
        [FromBody] ContestInsertRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ContestInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContestInsert,
            new
            {
                request.Id,
                request.ContestName,
                request.Image,
                request.Description,
                request.ContestType,
                request.Location,
                request.Latitude,
                request.Longitude
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Contest Insert

    #region 2. Contest Update

    [Authorize, HttpPost(ActionsConsts.Contest.UpdateContest)]
    public async Task<IActionResult> ContestUpdateAsync(
        [FromBody] ContestUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ContestUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContestUpdate,
            new
            {
                request.Id
            });

        return OkResponse(response);
    }

    #endregion 2. Contest Update

    #region 3. Contest Get By Id

    [HttpPost(ActionsConsts.Contest.ContestGetById)]
    public async Task<IActionResult> ContestGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Contest Get By Id

    #region 4. Get Contest List

    [HttpPost(ActionsConsts.Contest.GetContestList)]
    public async Task<IActionResult> ContestGetListAsync([FromBody] ContestGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Contest List

    #region 5. Contest Delete

    [HttpPost(ActionsConsts.Contest.DeleteContest)]
    public async Task<IActionResult> ContestDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContestDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 5. Contest Delete

    #region 6. Contest status Update

    [Authorize, HttpPost(ActionsConsts.Contest.ContestStatusUpdate)]
    public async Task<IActionResult> ContestStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContestStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 6. Contest status Update

    #region 7. Contest List without Pagination

    [HttpPost(ActionsConsts.Contest.ContestList)]
    public async Task<IActionResult> ContestGet([FromBody] ContestListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Store List without Pagination

    #region 8. Contest Insert As PerUserId

    [Authorize, HttpPost(ActionsConsts.Contest.InsertUserContest)]
    public async Task<IActionResult> InsertUserContestAsync([FromBody] ContestUserInsertRequest request)
    {
        if (request == null) request = new ContestUserInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);
        var response = await helper.UserContestInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }
    #endregion

    #region 9. Contest details Get By UserId

    [Authorize, HttpPost(ActionsConsts.Contest.ContestDetailsGetByUserId)]
    public async Task<IActionResult> ContestDetailsGetByUserIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContestHelpers(DbContext, Crypto, Mapper);

        var response = await helper.ContestDetailsGetByUserId(request);

        return OkResponse(response);
    }
    #endregion
}