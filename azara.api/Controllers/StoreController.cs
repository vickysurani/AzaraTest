namespace azara.api.Controllers;

public class StoreController : BaseController
{
    #region Object Declaration And Constructor

    public StoreController(
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

    public IFormFile? file { get; set; }

    #endregion Object Declaration And Constructor

    #region 1. Store Insert

    [Authorize, HttpPost(ActionsConsts.Store.InsertStore)]
    public async Task<IActionResult> ProductInsertAsync(
        [FromBody] StoreInsertRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new StoreInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);
        var response = await helper.StoreInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.StoreInsert,
            new
            {
                request.Id,
                request.Name,
                request.Location,
                request.Address,
                request.EmailId,
                request.ContactNumber,
                request.Latitude,
                request.Longitude,
                request.Created
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Store Insert

    #region 2. Store Update

    [Authorize, HttpPost(ActionsConsts.Store.UpdateStore)]
    public async Task<IActionResult> StoreUpdateAsync(
        [FromBody] StoreUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new StoreUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.StoreUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.StoreInsert,
            new
            {
                request.Id,
            });

        return OkResponse(response);
    }

    #endregion 2. Store Update

    #region 3. Store Get By Id

    [HttpPost(ActionsConsts.Store.StoreGetById)]
    public async Task<IActionResult> StoreGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.StoreGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Store Get By Id

    #region 4. Get Store List

    [HttpPost(ActionsConsts.Store.StoreGetList)]
    public async Task<IActionResult> StoreGetListAsync([FromBody] StoreGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.StoreGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Store List

    #region 5. Delete Store

    [HttpPost(ActionsConsts.Store.StoreDelete)]
    public async Task<IActionResult> DeleteStore(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.DeleteStore(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.StoreInsert,
            new
            {
                request.Id,
                IsDeleted = true
            });

        return OkResponse(response);
    }

    #endregion 5. Delete Store

    #region 6. Upload Store Image

    [Authorize, HttpPost(ActionsConsts.Store.UploadStoreImage)]
    public async Task<IActionResult> UploadStoreImage(
        [FromForm] FileWithNameUploadRequest request,
        [FromServices] IFileManagerLogic fileUpload)
    {
        if (!ModelState.IsValid) return ErrorResponse(ModelState);

        ValidateImageExtension(file.FileName);

        request.FileName = request.FileName + '_' + DateTime.UtcNow.ToString("ddMMyyyyhhmmss");

        var imageUrl = await fileUpload.UploadNew(request.File, BlobContainerConsts.Store, request.FileName, request.OldFileName);

        if (string.IsNullOrEmpty(imageUrl)) return ErrorResponse();

        return OkResponse(new { imageUrl });
    }

    #endregion 6. Upload Store Image

    #region 7. Store List without Pagination

    [HttpPost(ActionsConsts.Store.StoreList)]
    public async Task<IActionResult> StoreGet([FromBody] StoreListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.StoreListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Store List without Pagination

    #region 8. Store status Update

    [Authorize, HttpPost(ActionsConsts.Store.StoreStatusUpdate)]
    public async Task<IActionResult> StoreStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new StoreHelpers(DbContext, Crypto);

        var response = await helper.StoreStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.StoreStatusUpdate,
          new
          {
              request.Id,
              request.Active
          });

        return OkResponse();
    }

    #endregion 8. Store status Update


}