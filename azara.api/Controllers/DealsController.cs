namespace azara.api.Controllers;

public class DealsController : BaseController
{
    #region Object Declaration And Constructor

    public DealsController(
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

    #region 1. Deals Insert

    [Authorize, HttpPost(ActionsConsts.Deals.InsertDeals)]
    public async Task<IActionResult> DealsInsertAsync(
        [FromBody] DealsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new DealsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto);
        var response = await helper.DealsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.DealsInsert,
            new
            {
                request.Id,
                request.Title,
                request.BannerImage,
                request.Description,
                request.StoreId,
                request.Position,
                request.URL,
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Deals Insert

    #region 2. Deals Update

    [Authorize, HttpPost(ActionsConsts.Deals.UpdateDeals)]
    public async Task<IActionResult> DealsUpdateAsync(
        [FromBody] DealsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new DealsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto);

        var response = await helper.DealsUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.DealsUpdate,
            new
            {
                request.Id,
            });

        return OkResponse(response);
    }

    #endregion 2. Deals Update

    #region 3. Get Deals List

    [Authorize, HttpPost(ActionsConsts.Deals.GetDealsList)]
    public async Task<IActionResult> DealsGetListAsync([FromBody] DealsGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto);

        var response = await helper.DealsGetList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 3. Get Deals List

    #region 4. Deals Delete

    [HttpPost(ActionsConsts.Deals.DeleteDeals)]
    public async Task<IActionResult> DealsDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto);

        var response = await helper.DealsDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.DealsDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 4. Deals Delete

    #region 5. Deals Get By Id

    [HttpPost(ActionsConsts.Deals.DealsGetById)]
    public async Task<IActionResult> DealsGetBtyIdAsync([FromBody] DealsIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto, Mapper);

        var response = await helper.GetDealsById(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 5. Deals Get By Id

    #region 6. Deals status Update

    [Authorize, HttpPost(ActionsConsts.Deals.DealsStatusUpdate)]
    public async Task<IActionResult> DealsStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto, Mapper);

        var response = await helper.DealsStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.DealsStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 6. Deals status Update

    #region 7. Deals List without Pagination

    [HttpPost(ActionsConsts.Deals.DealsList)]
    public async Task<IActionResult> DealsGet([FromBody] DealsListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new DealsHelpers(DbContext, Crypto);

        var response = await helper.DealsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Deals List without Pagination
}