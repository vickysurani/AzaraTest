using azara.models.Requests.PrivacyPolicy;

namespace azara.api.Controllers
{
    public class PrivacyPolicyController : BaseController
    {
        #region Object Declaration And Constructor

        public PrivacyPolicyController(
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

        #region 1. PrivacyPolicy Insert

        [Authorize, HttpPost(ActionsConsts.PrivacyPolicy.PrivacyPolicyInsert)]
        public async Task<IActionResult> PrivacyPolicyInsertAsync(
            [FromBody] PrivacyPolicyInsertRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new PrivacyPolicyInsertRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PrivacyPolicyHelpers(DbContext, Crypto);
            var response = await helper.PrivacyPolicyInsert(request);

            if (response == null) return ErrorResponse();
            if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

            //DbContext.SaveChanges();
            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PrivacyPolicyInsert,
                new
                {
                    request.Id,
                    request.Description,
                });


            return OkResponse();
        }

        #endregion 1. PrivacyPolicy Insert 

        #region 2. PrivacyPolicy Update

        [Authorize, HttpPost(ActionsConsts.PrivacyPolicy.PrivacyPolicyUpdate)]
        public async Task<IActionResult> PrivacyPolicyUpdateAsync(
            [FromBody] PrivacyPolicyInsertRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new PrivacyPolicyInsertRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PrivacyPolicyHelpers(DbContext, Crypto);

            var response = await helper.PrivacyPolicyUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PrivacyPolicyUpdate,
                new
                {
                    request.Id,
                });

            return OkResponse(response);
        }

        #endregion 2. Blog Update

        #region 3. Get PrivacyPolicy List

        [HttpPost(ActionsConsts.PrivacyPolicy.PrivacyPolicyList)]
        public async Task<IActionResult> PrivacyPolicyGetListAsync([FromBody] PrivacyPolicyGetListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PrivacyPolicyHelpers(DbContext, Crypto);

            var response = await helper.PrivacyPolicyGetList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 3. Get PrivacyPolicy List

        #region 4. PrivacyPolicy Delete

        [HttpPost(ActionsConsts.PrivacyPolicy.PrivacyPolicyDelete)]
        public async Task<IActionResult> PrivacyPolicyDeleteAsync(
            [FromBody] BaseIdRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PrivacyPolicyHelpers(DbContext, Crypto);

            var response = await helper.PrivacyPolicyDelete(request);
            if (response is null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PrivacyPolicyDelete,
               new
               {
                   request.Id,
                   IsDeleted = true
               });

            return OkResponse(response);
        }

        #endregion 5. PrivacyPolicy Delete
    }
}
