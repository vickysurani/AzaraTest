using azara.models.Requests.Banner;

namespace azara.api.Controllers
{
    public class BannerController : BaseController
    {
        #region Object Declaration And Constructor

        public BannerController(
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

        #region 1. Banner Persist

        [Authorize, HttpPost(ActionsConsts.Banner.BannerInsertUpdate)]
        public async Task<IActionResult> BannerInsert(
        [FromBody] BannerInsertUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new BannerInsertUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var role = GetUserRole(User);

            if (role.Equals(MasterRoleConst.Admin))
                return ErrorResponse("bad_response_forbid_error_access");
            if (role.Equals(MasterRoleConst.Subadmin))
                return ErrorResponse("bad_response_forbid_error_access");

            using var helper = new BannerHelpers(DbContext, Crypto);
            var response = await helper.BannerInsert(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BannerImageInsert,
            new
            {
                request.Id,
                request.BannerImage1,
                request.BannerImage2,
                request.BannerImage3,
                request.Status,
                request.Created,
                request.ModifiedBy
            });

            //DbContext.SaveChanges();

            return OkResponse();
        }

        #endregion 1. Banner Persist 

        #region 2. Banner Get By Id

        [Authorize, HttpPost(ActionsConsts.Banner.BannerGetById)]
        public async Task<IActionResult> BannerGetById([FromBody] BaseRequiredIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var role = GetUserRole(User);

            if (role.Equals(MasterRoleConst.Admin))
                return ErrorResponse("bad_response_forbid_error_access");
            if (role.Equals(MasterRoleConst.Subadmin))
                return ErrorResponse("bad_response_forbid_error_access");

            using var helper = new BannerHelpers(DbContext, Crypto);

            var response = await helper.BannerGetById(request);
            if (response is null) ErrorResponse();

            return OkResponse(response);
        }

        #endregion 2. Banner Get By Id

        #region 3. Banner Get List

        [HttpPost(ActionsConsts.Banner.BannerGetList)]
        public async Task<IActionResult> BannerGetList([FromBody] BannerGetListRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");

            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var role = GetUserRole(User);

            if (role.Equals(MasterRoleConst.Admin))
                return ErrorResponse("bad_response_forbid_error_access");
            if (role.Equals(MasterRoleConst.Subadmin))
                return ErrorResponse("bad_response_forbid_error_access");

            using var helper = new BannerHelpers(DbContext, Crypto);

            var response = await helper.BannerGetList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 3. Banner Get List

        #region 4. Upload Banner Image

        [Authorize, HttpPost(ActionsConsts.Banner.BannerImageUpload)]
        public async Task<IActionResult> UploadBannerImage(
            [FromForm] FileWithNameUploadRequest request,
            [FromServices] IFileManagerLogic fileUpload)
        {
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            ValidateImageExtension(file.FileName);

            request.FileName = request.FileName + '_' + DateTime.UtcNow.ToString("ddMMyyyyhhmmss");

            var imageUrl = await fileUpload.UploadNew(request.File, BlobContainerConsts.Banner, request.FileName, request.OldFileName);

            if (string.IsNullOrEmpty(imageUrl)) return ErrorResponse();

            return OkResponse(new { imageUrl });
        }

        #endregion 4. Upload Banner Image

        #region 5. Update Banner Image
        [HttpPost(ActionsConsts.Banner.BannerUpdateImage)]
        public async Task<IActionResult> BannerUpdateImage(
       [FromBody] BannerInsertRequest request)
        {
            if (request == null) request = new BannerInsertRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new BannerHelpers(DbContext, Crypto);
            var response = await helper.BannerUpdateImage(request);

            if (response == null) return ErrorResponse();

            return OkResponse(response);
        }
        #endregion Bannner Insert Image-1

        // #region 6. Bannner Insert Image-2
        // [HttpPost(ActionsConsts.Banner.BannerInsertImage2)]
        // public async Task<IActionResult> BannerInsertImage2(
        //[FromBody] BannerInsertRequest request,
        //[FromServices] IHubContext<SignalRHelpers> hubContext)
        // {
        //     if (request == null) request = new BannerInsertRequest();
        //     if (!ModelState.IsValid)
        //         return ErrorResponse(ModelState);

        //     var role = GetUserRole(User);

        //     if (role.Equals(MasterRoleConst.Admin))
        //         return ErrorResponse("bad_response_forbid_error_access");
        //     if (role.Equals(MasterRoleConst.Subadmin))
        //         return ErrorResponse("bad_response_forbid_error_access");

        //     using var helper = new BannerHelpers(DbContext, Crypto);
        //     var response = await helper.BannerInsertImage2(request);

        //     CheckHelperResponse(response);

        //     await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BannerImageInsert,
        //     new
        //     {
        //         request.Id,
        //         request.BannerImage2,
        //         request.Created,
        //         request.ModifiedBy
        //     });

        //     DbContext.SaveChanges();

        //     return OkResponse();
        // }
        // #endregion Bannner Insert Image-2

        // #region 7. Bannner Insert Image-3
        // [HttpPost(ActionsConsts.Banner.BannerInsertImage3)]
        // public async Task<IActionResult> BannerInsertImage3(
        //[FromBody] BannerInsertRequest request,
        //[FromServices] IHubContext<SignalRHelpers> hubContext)
        // {
        //     if (request == null) request = new BannerInsertRequest();
        //     if (!ModelState.IsValid)
        //         return ErrorResponse(ModelState);

        //     //var role = GetUserRole(User);

        //     //if (role.Equals(MasterRoleConst.Admin))
        //     //    return ErrorResponse("bad_response_forbid_error_access");
        //     //if (role.Equals(MasterRoleConst.Subadmin))
        //     //    return ErrorResponse("bad_response_forbid_error_access");

        //     using var helper = new BannerHelpers(DbContext, Crypto);
        //     var response = await helper.BannerInsertImage3(request);

        //     CheckHelperResponse(response);

        //     await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.BannerImageInsert,
        //     new
        //     {
        //         request.Id,
        //         request.BannerImage3,
        //         request.Created,
        //         request.ModifiedBy
        //     });

        //     DbContext.SaveChanges();

        //     return OkResponse();
        // }
        // #endregion Bannner Insert Image-3

        //#region 8.Banner Image Get By Id 
        //[HttpPost(ActionsConsts.Banner.BannerImageGetById)]
        //public async Task<IActionResult> BannerImageGetById([FromBody] BaseRequiredIdRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return ErrorResponse(ModelState);

        //    var role = GetUserRole(User);

        //    if (role.Equals(MasterRoleConst.Admin))
        //        return ErrorResponse("bad_response_forbid_error_access");
        //    if (role.Equals(MasterRoleConst.Subadmin))
        //        return ErrorResponse("bad_response_forbid_error_access");

        //    using var helper = new BannerHelpers(DbContext, Crypto);

        //    var response = await helper.BannerGetById(request);
        //    if (response is null) ErrorResponse();

        //    return OkResponse(response);
        //}
        //#endregion



    }
}
