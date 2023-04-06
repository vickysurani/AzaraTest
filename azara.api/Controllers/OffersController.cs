namespace azara.api.Controllers;

public class OffersController : BaseController
{
    #region Object Declaration And Constructor

    public OffersController(
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

    #region 1. Insert Offers

    [Authorize, HttpPost(ActionsConsts.Offers.InsertOffer)]
    public async Task<IActionResult> OffersInsertAsync([FromBody] OffersInsertUpdateRequest request)
    {
        if (request == null) request = new OffersInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);
        var response = await helper.OffersInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Insert Offers

    #region 2. Offers Update

    [Authorize, HttpPost(ActionsConsts.Offers.UpdateOffer)]
    public async Task<IActionResult> OffersUpdateAsync([FromBody] OffersInsertUpdateRequest request)
    {
        if (request == null) request = new OffersInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);

        var response = await helper.OffersUpdate(request);

        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 2. Offers Update

    #region 3. Offers Get By Id

    [HttpPost(ActionsConsts.Offers.OfferGetById)]
    public async Task<IActionResult> OffersGetBtyIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);

        var response = await helper.OffersGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Offers Get By Id

    #region 4. Get Offers List

    [HttpPost(ActionsConsts.Offers.GetOfferList)]
    public async Task<IActionResult> OffersGetListAsync([FromBody] OffersGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);

        var response = await helper.OffersGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Offers List

    #region 5. Delete Offers

    [HttpPost(ActionsConsts.Offers.DeleteOffer)]
    public async Task<IActionResult> OffersDeleteAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);

        var response = await helper.OffersDelete(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 5. Delete Offers

    #region 6. Offers Status Update

    [Authorize, HttpPost(ActionsConsts.Offers.OfferStatusUpdate)]
    public async Task<IActionResult> OffersStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new OffersHelpers(DbContext, Crypto, Mapper);

        var response = await helper.OffersStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.OfferStatusUpdate,
           new
           {
               request.Id,
               request.Active
           });

        return OkResponse();
    }

    #endregion 6. Offers Status Update
}