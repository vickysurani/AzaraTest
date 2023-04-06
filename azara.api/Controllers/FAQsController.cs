namespace azara.api.Controllers;

public class FAQsController : BaseController
{
    #region Object Declaration And Constructor

    public FAQsController(
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

    #region 1. FAQs Insert

    [Authorize, HttpPost(ActionsConsts.FAQs.InsertFAQs)]
    public async Task<IActionResult> FAQsInsertAsync(
        [FromBody] FAQsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new FAQsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);
        var response = await helper.FAQsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.FAQsInsert,
            new
            {
                request.Id,
                request.Question,
                request.Answer
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. FAQs Insert

    #region 2. FAQs Update

    [Authorize, HttpPost(ActionsConsts.FAQs.UpdateFAQs)]
    public async Task<IActionResult> FAQsUpdateAsync(
        [FromBody] FAQsInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new FAQsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);

        var response = await helper.FAQsUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.FAQsUpdate,
            new
            {
                request.Id
            });

        return OkResponse(response);
    }

    #endregion 2. FAQs Update

    #region 3. Get FAQs List

    [HttpPost(ActionsConsts.FAQs.GetFAQsList)]
    public async Task<IActionResult> FAQsGetListAsync([FromBody] FAQsGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);

        var response = await helper.FAQsGetList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 3. Get FAQs List

    #region 4. FAQs Get By Id

    [HttpPost(ActionsConsts.FAQs.FAQsGetById)]
    public async Task<IActionResult> FAQsGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);

        var response = await helper.FAQsGetById(request);

        return OkResponse(response);
    }

    #endregion 4. FAQs Get By Id

    #region 5. FAQs Delete

    [HttpPost(ActionsConsts.FAQs.DeleteFAQs)]
    public async Task<IActionResult> FAQsDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);

        var response = await helper.FAQsDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.FAQsDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 5. FAQs Delete

    #region 6. FAQs List without Pagination

    [HttpPost(ActionsConsts.FAQs.FAQsList)]
    public async Task<IActionResult> FAQsGet([FromBody] FAQsListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new FAQsHelpers(DbContext, Crypto);

        var response = await helper.FAQsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 6. FAQs List without Pagination
}