using azara.models.Requests.Team;
using azara.models.Responses.Team;

namespace azara.api.Controllers
{
    public class TeamController : BaseController
    {
        #region Object Declaration And Constructor

        public TeamController(
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

        #region 1. Team Insert

        [Authorize, HttpPost(ActionsConsts.Team.TeamInsert)]
        public async Task<IActionResult> TeamInsertAsync(
            [FromBody] TeamInsertRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            request.ModifiedBy = GetUserId(User).ToString();

            using var helper = new TeamHelpers(DbContext, Crypto);
            var response = await helper.TeamInsert(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.TeamInsert,
                new
                {
                    request.Name,
                    request.PermissionJson
                });

            //DbContext.SaveChanges();

            return OkResponse();
        }

        #endregion 1. Team Insert

        #region 2. Team Update

        [Authorize, HttpPost(ActionsConsts.Team.TeamUpdate)]
        public async Task<IActionResult> TeamUpdateAsync(
            [FromBody] TeamUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            request.ModifiedBy = GetUserId(User).ToString();

            using var helper = new TeamHelpers(DbContext, Crypto);

            var response = await helper.TeamUpdate(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.TeamUpdate,
                new
                {
                    request.Id,
                });

            return OkResponse(response);
        }

        #endregion 2. Team Update

        #region 3. Team Get By Id

        [Authorize, HttpPost(ActionsConsts.Team.TeamGetById)]
        public async Task<IActionResult> TeamGetById([FromBody] BaseGetByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new TeamHelpers(DbContext, Crypto);

            var helperResponse = await helper.TeamGetById(request);
            CheckHelperResponse(helperResponse);

            var response = JsonConvert.DeserializeObject<TeamGetByIdResponse>(helperResponse.Data);

            return OkResponse(response);
        }

        #endregion 3. Team Get By Id

        #region 4. Team Delete

        [HttpPost(ActionsConsts.Team.TeamDelete)]
        public async Task<IActionResult> TeamDeleteAsync(
        [FromBody] BaseIdRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new TeamHelpers(DbContext, Crypto);

            var response = await helper.TeamDelete(request);
            if (response is null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.TeamDelete,
                new
                {
                    request.Id,
                    IsDeleted = true,
                });

            return OkResponse(response);
        }

        #endregion 4. Team Delete

        #region 5. Team Status Update

        [Authorize, HttpPost(ActionsConsts.Team.TeamStatusUpdate)]
        public async Task<IActionResult> TeamStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new TeamHelpers(DbContext, Crypto);

            var response = await helper.TeamStatusUpdate(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.TeamStatusUpdate,
               new
               {
                   request.Id,
                   request.Active
               });

            return OkResponse();
        }

        #endregion 5. Team Status Update

        #region 6. Team Get List

        [Authorize, HttpPost(ActionsConsts.Team.TeamGetList)]
        public async Task<IActionResult> TeamGetListAsync([FromBody] TeamGetListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new TeamHelpers(DbContext, Crypto);

            var response = await helper.TeamGetList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 6. Team Get List
    }
}
