namespace azara.api.Controllers;

public class RewardsController : BaseController
{
    #region Object Declaration And Constructor

    public RewardsController(
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

    #region 1. Insert Rewards

    [Authorize, HttpPost(ActionsConsts.Rewards.InsertRewards)]
    public async Task<IActionResult> RewardsInsertAsync(
        [FromBody] RewardsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new RewardsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        //request.UserId = GetUserId(User);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);
        var response = await helper.RewardsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.RewardsInsert,
            new
            {
                request.Id,
                request.Name,
                request.Image,
                request.Description,
                request.Barcode,
                request.PointId,
                request.UserId,
                request.Status,
                //request.RewardsExpiryDate
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Insert Rewards

    #region 2. Rewards Update

    [Authorize, HttpPost(ActionsConsts.Rewards.UpdateRewards)]
    public async Task<IActionResult> RewardsUpdateAsync(
        [FromBody] RewardsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new RewardsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.RewardsUpdate,
            new
            {
                request.Id
            });

        return OkResponse(response);
    }

    #endregion 2. Rewards Update

    #region 3. Rewards Get By Id

    [Authorize, HttpPost(ActionsConsts.Rewards.RewardsGetById)]
    public async Task<IActionResult> RewardsGetByIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Rewards Get By Id

    #region 4. Rewards Add Into MyRewards Based on UserId
    //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet(ActionsConsts.Rewards.RewardsAddIntoMyRewardsById)]
    public async Task<IActionResult> RewardsAddIntoMyRewardsByIdAsync()
    {
        //if (!ModelState.IsValid)
        //    return ErrorResponse(ModelState);

        var userId = GetUserId(User);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsAddIntoMyRewardsById(userId.ToString());

        return OkResponse(response);
    }

    #endregion 4. Rewards Add Into MyRewards Based on UserId

    #region 5. Get Rewards List

    [HttpPost(ActionsConsts.Rewards.GetRewardsList)]
    public async Task<IActionResult> RewardsGetListAsync([FromBody] RewardsGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 5. Get Rewards List

    #region 6. Delete Rewards

    [HttpPost(ActionsConsts.Rewards.DeleteRewards)]
    public async Task<IActionResult> RewardsDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.RewardsDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 6. Delete Rewards

    #region 7. Rewards Status Update

    [Authorize, HttpPost(ActionsConsts.Rewards.RewardsStatusUpdate)]
    public async Task<IActionResult> RewardsStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.RewardsStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 7. Rewards Status Update

    #region 8. Import Rewards CSV from QuickBooks
    [Authorize, HttpPost(ActionsConsts.Rewards.RewardsCSVImport)]
    public async Task<IActionResult> GetRewardsCSV([FromForm] IFormFileCollection file)

    {
        var response = CSVService.ReadCSV<ApiRewardsCSVExportRequest>(file[0].OpenReadStream());
        return OkResponse(response);
    }
    #endregion 8. Import Rewards CSV from QuickBooks

    #region 9. Insert Rewards Using CSV

    [Authorize, HttpPost(ActionsConsts.Rewards.InsertCSVRewards)]
    public async Task<IActionResult> RewardsCSVInsertAsync(
        [FromBody] RewardsDataList request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new RewardsDataList();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);
        var response = await helper.RewardsCSVsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        foreach (var r in request.Details)
        {
            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.InsertRewardsUsingCSV,
            new
            {
                r.Name,
                r.Barcode,
                r.RewardsPoint
            });
        }

        //DbContext.SaveChanges();

        return OkResponse("response_data_added_successfully");
    }

    #endregion 9. Insert Rewards Using CSV

    #region 10. Rewards List without Pagination

    [HttpPost(ActionsConsts.Rewards.RewardsList)]
    public async Task<IActionResult> RewardsGet([FromBody] RewardsListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new RewardsHelpers(DbContext, Crypto, Mapper, CSVService);

        var response = await helper.RewardsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 10. Rewards List without Pagination
}