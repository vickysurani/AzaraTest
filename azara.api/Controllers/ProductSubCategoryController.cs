namespace azara.api.Controllers;

public class ProductSubCategoryController : BaseController
{
    #region Object Declaration And Constructor

    public ProductSubCategoryController(
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

    #region 1. Product Sub Category Insert

    [Authorize, HttpPost(ActionsConsts.ProductSubCategory.InsertProductSubCategory)]
    public async Task<IActionResult> ProductSubCategoryInsertAsync(
        [FromBody] ProductSubCategoryInsertRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductSubCategoryInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);
        var response = await helper.ProductSubCategoryInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductSubCategoryInsert,
            new
            {
                request.Id,
                request.Name,
                request.Image,
                request.Status,
                request.ProductCategoryId
            });

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Product Sub Category Insert

    #region 2. Product Sub Category Update

    [HttpPost(ActionsConsts.ProductSubCategory.UpdateProductSubCategory)]
    public async Task<IActionResult> ProductSubCategoryUpdateAsync(
        [FromBody] ProductSubCategoryUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new ProductSubCategoryUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductSubCategoryUpdate,
            new
            {
                request.Id,
            });

        return OkResponse(response);
    }

    #endregion 2. Product Sub Category Update

    #region 3. Product Get By Id

    [HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryGetById)]
    public async Task<IActionResult> ProductSubCategoryGetByIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Product Get By Id

    #region 4. Get Product Sub Category List

    [Authorize, HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryGetList)]
    public async Task<IActionResult> ProductSubCategoryGetListAsync([FromBody] ProductSubCategoryGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Product Sub Category List

    #region 5. Product Sub Category Delete

    [Authorize, HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryGetDelete)]
    public async Task<IActionResult> ProductSubCategoryDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductSubCategoryDelete,
            new
            {
                request.Id,
                IsDeleted = true,
            });

        return OkResponse(response);
    }

    #endregion 5. Product Sub Category Delete

    #region 6. Product Sub Category status Update

    [Authorize, HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryStatusUpdate)]
    public async Task<IActionResult> ProductSubCategoryStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.ProductSubCategoryStatusUpdate,
          new
          {
              request.Id,
              request.Active
          });

        return OkResponse();
    }

    #endregion 6. Product Sub Category status Update

    #region 7. Get ProductSubCategory By ProductCategoryId

    [HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryListByProductCategoryId)]
    public async Task<IActionResult> GetProductSubCategoryListByProductCategoryId([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.GetProductCategoryList(request);

        return OkResponse(response);
    }

    #endregion 7. Get ProductSubCategory By ProductCategoryId

    #region 8. Get ProductSubCategory & Product

    [HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryListByProductId)]
    public async Task<IActionResult> GetProductSubCategoryProduct([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);
        var response = await helper.GetProductSubCategoryProduct(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 8. Get ProductSubCategory & Product

    #region 9. ProductSubCategory List without Pagination

    [HttpPost(ActionsConsts.ProductSubCategory.ProductSubCategoryList)]
    public async Task<IActionResult> ProductSubCategoryGet([FromBody] ProductSubCategoryListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ProductSubCategoryHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 9. ProductSubCategory List without Pagination
}