namespace azara.api.Controllers;

public class BlogController : BaseController
{
    #region Object Declaration And Constructor

    public BlogController(
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

    #region 1. Blog Insert

    [Authorize, HttpPost(ActionsConsts.Blog.InsertBlog)]
    public async Task<IActionResult> BlogInsertAsync(
        [FromBody] BlogInsertRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new BlogInsertRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);
        var response = await helper.BlogInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();
        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BlogInsert,
            new
            {
                request.Id,
                request.Image,
                request.Title,
                request.PublishedDate,
                request.AuthorName,
                request.Descriptions,
                request.Created
            });


        return OkResponse();
    }

    #endregion 1. Blog Insert

    #region 2. Blog Update

    [Authorize, HttpPost(ActionsConsts.Blog.UpdateBlog)]
    public async Task<IActionResult> BlogUpdateAsync(
        [FromBody] BlogUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new BlogUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);

        var response = await helper.BlogUpdate(request);

        if (response == null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BlogUpdate,
            new
            {
                request.Id,
            });

        return OkResponse(response);
    }

    #endregion 2. Blog Update

    #region 3. Blog Get By Id

    [HttpPost(ActionsConsts.Blog.BlogGetById)]
    public async Task<IActionResult> BlogGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);

        var response = await helper.BlogGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Blog Get By Id

    #region 4. Get Blog List

    [HttpPost(ActionsConsts.Blog.GetBlogList)]
    public async Task<IActionResult> BlogGetListAsync([FromBody] BlogGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);

        var response = await helper.BlogGetList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Get Blog List

    #region 5. Blog Delete

    [HttpPost(ActionsConsts.Blog.DeleteBlog)]
    public async Task<IActionResult> BlogDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);

        var response = await helper.BlogDelete(request);
        if (response is null) return ErrorResponse();

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BlogDelete,
           new
           {
               request.Id,
               IsDeleted = true
           });

        return OkResponse(response);
    }

    #endregion 5. Blog Delete

    #region 6. Upload Blog Image

    [Authorize, HttpPost(ActionsConsts.Blog.UploadBlogImage)]
    public async Task<IActionResult> UploadBlogImage(
        [FromForm] FileWithNameUploadRequest request,
        [FromServices] IFileManagerLogic fileUpload)
    {
        if (!ModelState.IsValid) return ErrorResponse(ModelState);

        ValidateImageExtension(file.FileName);

        request.FileName = request.FileName + '_' + DateTime.UtcNow.ToString("ddMMyyyyhhmmss");

        var imageUrl = await fileUpload.UploadNew(request.File, BlobContainerConsts.Blog, request.FileName, request.OldFileName);

        if (string.IsNullOrEmpty(imageUrl)) return ErrorResponse();

        return OkResponse(new { imageUrl });
    }

    #endregion 6. Upload Blog Image

    #region 7. Blog List without Pagination

    [HttpPost(ActionsConsts.Blog.BlogList)]
    public async Task<IActionResult> StoreGet([FromBody] BlogListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlogHelpers(DbContext, Crypto);

        var response = await helper.BlogList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 7. Blog List without Pagination
}