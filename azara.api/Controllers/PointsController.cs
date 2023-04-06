namespace azara.api.Controllers;

public class PointsController : BaseController
{
    #region Object Declaration And Constructor

    public PointsController(
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

    #region 1. Points Insert

    [Authorize, HttpPost(ActionsConsts.Points.InsertPoints)]
    public async Task<IActionResult> PointsInsertAsync(
        [FromBody] PointsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new PointsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);
        var response = await helper.PointsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointsInsert,
            new
            {
                request.Id,
                request.AvailablePoints,
                request.UsedPoints,
                request.PointName,
                request.UserId
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Points Insert

    #region 2. Points Update

    [Authorize, HttpPost(ActionsConsts.Points.UpdatePoints)]
    public async Task<IActionResult> PointsUpdateAsync(
        [FromBody] PointsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new PointsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);

        var response = await helper.PointsUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointsUpdate,
            new
            {
                request.Id,
            });

        return OkResponse(response);
    }

    #endregion 2. Points Update

    #region 3. Points Get By Id

    [Authorize, HttpPost(ActionsConsts.Points.PointsGetById)]
    public async Task<IActionResult> PointsGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);

        var response = await helper.PointsGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Points Get By Id

    #region 4. Get Points List

    [HttpPost(ActionsConsts.Points.PointsList)]
    public async Task<IActionResult> PointsGetListAsync([FromBody] PointsGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);

        var response = await helper.PointsGetList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Points List

    #region 5. Points Delete

    [HttpPost(ActionsConsts.Points.DeletePoints)]
    public async Task<IActionResult> PointsDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);

        var response = await helper.PointsDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointsDelete,
            new
            {
                request.Id,
                IsDeleted = true
            });

        return OkResponse(response);
    }

    #endregion 5. Points Delete

    #region 6. Point List without Pagination

    [HttpPost(ActionsConsts.Points.PointList)]
    public async Task<IActionResult> PointsGet([FromBody] PointsListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);

        var response = await helper.PointsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Point List without Pagination

    #region 7. Points assigned by admin for particualr userid

    [HttpPost(ActionsConsts.Points.PointAssignedByAdmin)]
    public async Task<IActionResult> PointAssignedByAdminAsync([FromBody] PointsAssignedByAdminRequest request)
    {
        if (request == null) request = new PointsAssignedByAdminRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);
        var response = await helper.PointsInsertByAdmin(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion

    #region 8. Rewards and Coupon points added in table

    [HttpPost(ActionsConsts.Points.PointsForRewardsAndCoupon)]
    public async Task<IActionResult> PointsForRewardsAndCouponAsync([FromBody] PointsAddedForRewardsAndCouponsRequest request)
    {
        if (request == null) request = new PointsAddedForRewardsAndCouponsRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new PointsHelpers(DbContext, Crypto);
        var response = await helper.PointsAddedForRewardsOrCoupons(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion
}