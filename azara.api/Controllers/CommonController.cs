using azara.models.Responses.Common;

namespace azara.api.Controllers;

public class CommonController : BaseController
{
    #region Object Declaration And Constructor

    public CommonController(
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

    #region 1. Upload Image

    [AllowAnonymous]
    [HttpPost(ActionsConsts.FileConst.UploadFile)]
    public async Task<IActionResult> UploadProductImage(
        [FromForm] FileNewWithNameUploadRequest request,
        [FromServices] IFileManagerLogic fileUpload)
    {
        if (!ModelState.IsValid) return ErrorResponse(ModelState);

        if (request.OldFileName != null && request.OldFileName.Count > 0)
            foreach (var oldFileName in request.OldFileName)
                await fileUpload.Delete(request.FolderName, oldFileName);

        List<FileResponse> fileResponseList = new List<FileResponse>();

        if (request.File != null && request.File.Count > 0)
        {
            foreach (var file in request.File)
            {
                ValidateImageExtension(file.FileName);

                FileInfo fileInfo = new(file.FileName);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

                FileResponse fileResponse = await fileUpload.UploadNewFile(file, request.FolderName, fileName);
                if (fileResponse != null)
                {
                    fileResponseList.Add(fileResponse);
                }
            }
        }

        if (fileResponseList == null) return ErrorResponse();

        return OkResponse(fileResponseList);
    }

    #endregion 1. Upload Image

    #region 2. Notification Get By UserId

    [HttpPost(ActionsConsts.Notification.GetNotificationByUserId)]
    public async Task<IActionResult> NotificationGetByUserIdAsync([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new NotificationHelpers(DbContext, Crypto, Mapper);

        var response = await helper.GetNotificationGetById(request);

        return OkResponse(response);
    }
    #endregion

}