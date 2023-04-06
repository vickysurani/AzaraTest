namespace azara.api.Controllers;

public class ProductController : BaseController
{
    #region Object Declaration And Constructor

    public ProductController(
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

    #region 1. Product Insert

    [Authorize, HttpPost(ActionsConsts.Product.InsertProduct)]
    public async Task<IActionResult> ProductInsertAsync(
        [FromBody] ProductInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        var adminId = GetUserId(User);
        using var helper = new ProductHelpers(DbContext, Crypto);
        var response = await helper.ProductInsert(request, adminId);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductInsert,
            new
            {
                request.Id,
                request.ImageList,
                request.Name,
                request.Description,
                request.OriginalPrice,
                request.DiscountedPrice,
                request.OfferId,
                request.StoreDropdownId,
                request.ProductCategoryId,
                request.ProductSubCategoryId,
                request.RewardsId,
                request.ModifiedBy
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Product Insert

    #region 2. Product Update

    [Authorize, HttpPost(ActionsConsts.Product.UpdateProduct)]
    public async Task<IActionResult> ProductUpdateAsync(
        [FromBody] ProductInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);

        var response = await helper.ProductUpdate(request);

        if (response == null) return ErrorResponse();

        new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductUpdate,
            new
            {
                request.Id
            });

        return OkResponse(response);
    }

    #endregion 2. Product Update

    #region 3. Product Get By Id

    [HttpPost(ActionsConsts.Product.ProductGetById)]
    public async Task<IActionResult> ProductGetBtyIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);

        var response = await helper.ProductGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Product Get By Id

    #region 4. Get Product List

    [HttpPost(ActionsConsts.Product.ProductGetList)]
    public async Task<IActionResult> ProductGetListAsync([FromBody] GetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);

        var response = await helper.ProductGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Product List

    #region 6. Product Delete

    [HttpPost(ActionsConsts.Product.ProductDelete)]
    public async Task<IActionResult> ProductDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);

        var response = await helper.ProductDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 6. Product Delete

    #region 7. Upload Product Image

    [Authorize, HttpPost(ActionsConsts.Product.UploadProductImage)]
    public async Task<IActionResult> UploadProductImage(
        [FromForm] FileWithNameUploadRequest request,
        [FromServices] IFileManagerLogic fileUpload)
    {
        if (!ModelState.IsValid) return ErrorResponse(ModelState);

        ValidateImageExtension(file.FileName);

        request.FileName = request.FileName + '_' + DateTime.UtcNow.ToString("ddMMyyyyhhmmss");

        var imageUrl = await fileUpload.UploadNew(request.File, BlobContainerConsts.Product, request.FileName, request.OldFileName);

        if (string.IsNullOrEmpty(imageUrl)) return ErrorResponse();

        return OkResponse(new { imageUrl });
    }

    #endregion 7. Upload Product Image

    #region 8. Get Store & Product Category

    [Authorize, HttpPost(ActionsConsts.Product.GetStoreProductCategoryList)]
    public async Task<IActionResult> GetStoreProductCategory([FromBody] LocationDetailRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);
        var response = await helper.GetStoreProductCategory(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 8. Get Store & Product Category

    #region 9. Product List without Pagination

    [HttpPost(ActionsConsts.Product.ProductList)]
    public async Task<IActionResult> ProductGet([FromBody] ProductListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductHelpers(DbContext, Crypto);

        var response = await helper.ProductListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Product List without Pagination
}