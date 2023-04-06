namespace azara.api.Controllers;

public class ProductCategoryController : BaseController
{
    #region Object Declaration And Constructor

    public ProductCategoryController(
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

    #region 1. Product Category Insert

    [Authorize, HttpPost(ActionsConsts.ProductCategory.InsertProductCategory)]
    public async Task<IActionResult> ProductInsertAsync(
        [FromBody] ProductCategoryInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductCategoryInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);
        var response = await helper.ProductCategoryInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductCategoryInsert,
            new
            {
                request.ProductCategoryId,
                request.Name,
                request.Image,
                request.Status
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Product Category Insert

    #region 2. Product Category Update

    [Authorize, HttpPost(ActionsConsts.ProductCategory.UpdateProductCategory)]
    public async Task<IActionResult> ProductCategoryUpdateAsync(
        [FromBody] ProductCategoryInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductCategoryInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductCategoryUpdate,
            new
            {
                request.ProductCategoryId,
            });

        return OkResponse(response);
    }

    #endregion 2. Product Category Update

    #region 3. Product Category Get By Id

    [HttpPost(ActionsConsts.ProductCategory.ProductCategoryGetById)]
    public async Task<IActionResult> ProductCategoryGetByIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Product Category Get By Id

    #region 4. Get Product Category List

    [HttpPost(ActionsConsts.ProductCategory.ProductCategoryGetList)]
    public async Task<IActionResult> ProductCategoryGetListAsync([FromBody] ProductCategoryGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Product Category List

    #region 5. Product Category Delete

    [Authorize, HttpPost(ActionsConsts.ProductCategory.ProductCategoryDelete)]
    public async Task<IActionResult> ProductCategoryDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductCategoryDelete,
            new
            {
                request.Id,
                IsDeleted = true
            });

        return OkResponse(response);
    }

    #endregion 5. Product Category Delete

    #region 6. Product Category status Update

    [Authorize, HttpPost(ActionsConsts.ProductCategory.ProductCategoryStatusUpdate)]
    public async Task<IActionResult> ProductCategoryStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductCategoryStatusUpdate,
          new
          {
              request.Id,
              request.Active
          });

        return OkResponse();
    }

    #endregion 6. Product Category status Update

    #region 7. Product Category List without Pagination

    [HttpPost(ActionsConsts.ProductCategory.ProductCategoryList)]
    public async Task<IActionResult> StoreGet([FromBody] ProductCategoryListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductCategoryListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Product Category List without Pagination
}