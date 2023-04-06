using azara.models.Requests.SubAdmin;
using azara.models.Responses.SubAdmins;

namespace azara.api.Controllers
{
    public class SubAdminController : BaseController
    {
        #region Object Declaration And Constructor

        public SubAdminController(
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

        #region 1. Add SubAdmin

        [Authorize, HttpPost(ActionsConsts.AdminAccount.SubAdminAdd)]
        public async Task<IActionResult> SubAdminAdd(
            [FromBody] SubAdminAddRequest request,
            [FromServices] IMessages messages,
            [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigsOptions,
            [FromServices] IOptions<EmailConfigs> emailConfig)
        {
            try
            {
                if (request == null) request = new SubAdminAddRequest();
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);

                request.ModifiedBy = GetUserId(User).ToString();

                using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);
                var response = await helper.SubAdminAdd(request);

                CheckHelperResponse(response);

                // Send Email
                new EmailHelpers(Localizer, messages).SendSubAdminAddEmail(
                    request.EmailId,
                    request.UserName,
                    request.Name,
                    request.Password,
                    emailConfig.Value.LoginUrl);

                return OkResponse();
            }
            catch (Exception ex)
            {

                return OkResponse(ex);
            }
        }

        #endregion 1. Add SubAdmin

        #region 2. Get SubAdmin List

        [Authorize, HttpPost(ActionsConsts.AdminAccount.SubAdminList)]
        public async Task<IActionResult> SubAdminGetListAsync([FromBody] SubAdminListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            request.AdminId = GetUserId(User).ToString();

            using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);
            var helperResponse = await helper.SubAdminGetListAsync(request);

            if (helperResponse is null) return ErrorResponse();

            return OkResponse(helperResponse);
        }

        #endregion 2. Get SubAdmin List

        #region 3. Update SubAdmin

        [Authorize, HttpPost(ActionsConsts.AdminAccount.SubAdminUpdate)]
        public async Task<IActionResult> SubAdminUpdate(
            [FromBody] SubAdminUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new SubAdminUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            request.ModifiedBy = GetUserId(User).ToString();

            using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);
            var response = await helper.SubAdminUpdate(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.SubAdminRoleUpdate, new { request.Id });

            return OkResponse(response);

        }
        #endregion 3. Update SubAdmin

        #region 4. Get By Id SubAdmin

        [HttpPost(ActionsConsts.AdminAccount.GetSubAdminById)]
        public async Task<IActionResult> SubAdminById([FromBody] BaseGetByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);

            var helperResponse = await helper.SubAdminById(request);
            if (helperResponse is null) return ErrorResponse();

            var response = JsonConvert.DeserializeObject<SubAdminGetByIdResponse>(helperResponse.Data);

            return OkResponse(response);
        }

        #endregion 4. Get By Id SubAdmin

        #region 5. Delete SubAdmin

        [Authorize, HttpPost(ActionsConsts.AdminAccount.SubAdminDelete)]
        public async Task<IActionResult> SubAdminDelete(
            [FromBody] SubAdminDeleteRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            request.ModifiedBy = GetUserId(User).ToString();

            using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);
            var response = await helper.SubAdminDelete(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.SubAdminDelete,
                new
                {
                    request.Id,
                    IsDeleted = true
                });

            return OkResponse(response);
        }

        #endregion 5. Delete SubAdmin

        #region 6. Status Update SubAdmin

        [Authorize, HttpPost(ActionsConsts.AdminAccount.SubAdminStatusUpdate)]
        public async Task<IActionResult> SubAdminStatusUpdate(
            [FromBody] SubAdminStatusUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            //request.Id = GetUserId(User);
            request.ModifiedBy = GetUserId(User).ToString();

            using var helper = new SubAdminHelpers(DbContext, Crypto, Mapper);
            var helperResponse = await helper.SubAdminStatusUpdate(request);

            CheckHelperResponse(helperResponse);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.SubAdminStatusUpdate,
                new
                {
                    request.Id,
                    Active = request.Active
                });

            return OkResponse();
        }

        #endregion 6. Status Update SubAdmin
    }
}
