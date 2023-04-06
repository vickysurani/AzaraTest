namespace azara.api.Controllers;

public class ContactUsController : BaseController
{
    #region Object Declaration And Constructor

    public ContactUsController(
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

    #region 1. Contact Insert

    [AllowAnonymous, HttpPost(ActionsConsts.ContactUs.AddContactUs)]
    public async Task<IActionResult> ProductInsertAsync(
        [FromBody] ContactUsAddRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ContactUsAddRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContactUsHelpers(DbContext, Crypto);
        var response = await helper.ContactUs(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContactUsInsert,
            new
            {
                request.Name,
                request.EmailId,
                request.Description
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Contact Insert

    #region 2. Contact Get By Id

    [HttpPost(ActionsConsts.ContactUs.ContactGetById)]
    public async Task<IActionResult> ContactGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContactUsHelpers(DbContext, Crypto);

        var response = await helper.ContactUsGetById(request);

        return OkResponse(response);
    }

    #endregion 2. Contact Get By Id

    #region 3. Get Contact List

    [HttpPost(ActionsConsts.ContactUs.GetContactList)]
    public async Task<IActionResult> ContactGetListAsync([FromBody] ContactGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContactUsHelpers(DbContext, Crypto);

        var response = await helper.ContactGetList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 3. Get Contact List

    #region 4. Contact Delete

    [HttpPost(ActionsConsts.ContactUs.DeleteContact)]
    public async Task<IActionResult> ContactDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContactUsHelpers(DbContext, Crypto);

        var response = await helper.ContactDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ContactUsDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 4. Contact Delete

    #region 5. Contact List without Pagination

    [HttpPost(ActionsConsts.ContactUs.ContactList)]
    public async Task<IActionResult> ContactUsGet([FromBody] ContactListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ContactUsHelpers(DbContext, Crypto);

        var response = await helper.ContactUsList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Store List without Pagination
}