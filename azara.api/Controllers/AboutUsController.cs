using azara.models.Requests.AboutUs;

namespace azara.api.Controllers
{

    public class AboutUsController : BaseController
    {
        #region Object Declaration And Constructor

        public AboutUsController(
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

        #region 1. AboutUs Insert

        [Authorize ,HttpPost(ActionsConsts.AboutUs.AboutUsInsert)]
        public async Task<IActionResult> AboutUsInsertAsync(
            [FromBody] AboutUsInsertRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new AboutUsHelpers(DbContext, Crypto);
            var response = await helper.AboutUsInsert(request);

            return OkResponse();
        }

        #endregion 1. About Insert

        #region 2. AboutUs Update
        [Authorize, HttpPost(ActionsConsts.AboutUs.AboutUsUpdate)]
        public async Task<IActionResult> AboutUsUpdateAsync([FromBody] AboutUsUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            request ??= new AboutUsUpdateRequest();

            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new AboutUsHelpers(DbContext, Crypto);
            var response = await helper.AboutUsUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.AboutUsUpdate,
                new
                {
                    request.Id
                });

            return OkResponse(response);
        }
        #endregion

        #region 3. Get About Us List

        [HttpPost(ActionsConsts.AboutUs.AboutUsList)]
        public async Task<IActionResult> AboutUsGetListAsync([FromBody] AboutUsGetRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new AboutUsHelpers(DbContext, Crypto);

            var response = await helper.AboutUsList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 3. Get FAQs List

        #region 4. AboutUs Delete
        [HttpPost(ActionsConsts.AboutUs.AboutUsDelete)]
        public async Task<IActionResult> AboutUsDeleteAsync([FromBody] BaseIdRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new AboutUsHelpers(DbContext, Crypto);
            var response = await helper.AboutUsDelete(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.AboutUsDelete,
                new
                {
                    request.Id
                });

            return OkResponse(response);
        }
        #endregion

        #region 5. About Us Get By Id
        [HttpPost(ActionsConsts.AboutUs.AboutUsGetById)]
        public async Task<IActionResult> AboutusGetBtyIdAsync([FromBody] BaseIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new AboutUsHelpers(DbContext, Crypto);

            var response = await helper.AboutusGetById(request);

            return OkResponse(response);
        }
        #endregion
    }
}
