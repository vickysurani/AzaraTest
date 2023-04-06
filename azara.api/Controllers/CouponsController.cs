namespace azara.api.Controllers;

public class CouponsController : BaseController
{
    #region Object Declaration And Constructor

    public CouponsController(
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

    #region 1. Insert Coupons

    [Authorize, HttpPost(ActionsConsts.Coupons.InsertCoupons)]
    public async Task<IActionResult> CouponsInsertAsync(
        [FromBody] CouponsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new CouponsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);
        var helperResponse = await helper.CouponsInsert(request);

        if (helperResponse == null) return ErrorResponse();
        if (!helperResponse.IsSuccess) return ErrorResponse(helperResponse.ErrorMessage);

        //var response = JsonConvert.DeserializeObject<CouponsListResponse>(helperResponse.Data);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.CouponsInsert,
            new
            {
                request.Id,
                request.CouponTitle,
                request.CouponImage,
                request.ExpiryDate,
                request.Description,
                request.Amount,
                request.CouponCode,
                Modified = DateTime.UtcNow,
                request.Active,
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Insert Coupons

    #region 2. Coupons Update

    [Authorize, HttpPost(ActionsConsts.Coupons.UpdateCoupons)]
    public async Task<IActionResult> CouponsUpdateAsync(
        [FromBody] CouponsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new CouponsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.CouponsUpdate,
            new
            {
                request.Id
            });

        return OkResponse(response);
    }

    #endregion 2. Coupons Update

    #region 3. Coupons Get By Id

    [HttpPost(ActionsConsts.Coupons.CouponsGetById)]
    public async Task<IActionResult> CouponsGetBtyIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Coupons Get By Id

    #region 4. Get Coupons List

    [HttpPost(ActionsConsts.Coupons.GetCouponsList)]
    public async Task<IActionResult> CouponsGetListAsync([FromBody] CouponsGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Coupons List

    #region 5. Delete Coupons

    [HttpPost(ActionsConsts.Coupons.DeleteCoupons)]
    public async Task<IActionResult> CouponsDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.CouponsDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 5. Delete Coupons

    #region 6. Coupons Status Update

    [Authorize, HttpPost(ActionsConsts.Coupons.CouponsStatusUpdate)]
    public async Task<IActionResult> CouponsStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.CouponsStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 6. Coupons Status Update

    #region 7. Coupons Added Into My Rewards Based on UserId
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Coupons.CouponsAddIntoMyRewardsById)]
    public async Task<IActionResult> CouponsAddIntoMyRewardsByIdAsync()
    {
        //if (!ModelState.IsValid)
        //    return ErrorResponse(ModelState);

        var userId = GetUserId(User);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsAddIntoMyRewardsById(userId.ToString());

        return OkResponse(response);
    }
    #endregion 7. Coupons Added Into My Rewards Based on UserId

    #region 8. Import Coupons CSV from QuickBooks
    [Authorize, HttpPost(ActionsConsts.Coupons.CouponsCSVImport)]
    public async Task<IActionResult> GetCouponsCSV([FromForm] IFormFileCollection file)

    {
        var response = CSVService.ReadCSV<ApiCouponsCSVExportRequest>(file[0].OpenReadStream());
        return OkResponse(response);
    }
    #endregion 8. Import Coupons CSV from QuickBooks

    #region 9. Insert Coupons Using CSV

    [Authorize, HttpPost(ActionsConsts.Coupons.InsertCSVCoupons)]
    public async Task<IActionResult> CouponsCSVInsertAsync(
        [FromBody] CouponsDataList request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new CouponsDataList();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);
        var response = await helper.CouponCSVsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        foreach (var c in request.Details)
        {
            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.InsertCouponsUsingCSV,
            new
            {
                c.CouponTitle,
                c.CouponCode,
                c.CouponsPoint,
                c.Amount
            });
        }

        //DbContext.SaveChanges();

        return OkResponse("response_data_added_successfully");
    }

    #endregion 9. Insert Coupons Using CSV

    #region 10. Coupons List without Pagination

    [HttpPost(ActionsConsts.Coupons.CouponsList)]
    public async Task<IActionResult> CouponsGet([FromBody] CouponsListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new CouponsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.CouponsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 10. Coupons List without Pagination
}