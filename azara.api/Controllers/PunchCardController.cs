using azara.models.Requests.PunchCard;

namespace azara.api.Controllers
{
    public class PunchCardController : BaseController
    {
        #region Object Declaration And Constructor

        public PunchCardController(
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

        #region 1. Insert PunchCard

        [Authorize, HttpPost(ActionsConsts.PunchCard.InsertPunchCard)]
        public async Task<IActionResult> PunchCardInsertAsync(
            [FromBody] PunchCardInsertUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new PunchCardInsertUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);
            var response = await helper.PunchCardInsert(request);

            if (response == null) return ErrorResponse();
            if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PunchCardInsert,
            new
            {
                request.Id,
                request.PunchCardName,
                request.Image,
                request.Description,
                request.PunchCardPromos,
                request.ExpiryDate,
                request.Active,
                request.Modified
            });

            //DbContext.SaveChanges();

            return OkResponse();
        }

        #endregion 1. Insert PunchCard

        #region 2. PunchCard Update

        [Authorize, HttpPost(ActionsConsts.PunchCard.UpdatePunchCard)]
        public async Task<IActionResult> PunchCardUpdateAsync(
            [FromBody] PunchCardInsertUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new PunchCardInsertUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PunchCardUpdate,
            new
            {
                request.Id,
            });

            return OkResponse(response);
        }

        #endregion 2. PunchCard Update

        #region 3. PunchCard Get By Id

        [HttpPost(ActionsConsts.PunchCard.PunchCardGetById)]
        public async Task<IActionResult> PunchCardGetBtyIdAsync([FromBody] BaseIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardGetById(request);

            return OkResponse(response);
        }

        #endregion 3. PunchCard Get By Id

        #region 4. Get PunchCard List

        [HttpPost(ActionsConsts.PunchCard.GetPunchCardList)]
        public async Task<IActionResult> PunchCardGetListAsync([FromBody] PunchCardGetListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardGetListAsync(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 4. Get PunchCard List

        #region 5. Delete PunchCard

        [HttpPost(ActionsConsts.PunchCard.DeletePunchCard)]
        public async Task<IActionResult> PunchCardDeleteAsync(
            [FromBody] BaseIdRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardDelete(request);
            if (response is null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PunchCardDelete,
            new
            {
                request.Id,
                IsDeleted = true
            });

            return OkResponse(response);
        }

        #endregion 5. Delete PunchCard

        #region 6. PunchCard Status Update

        [Authorize, HttpPost(ActionsConsts.PunchCard.PunchCardStatusUpdate)]
        public async Task<IActionResult> PunchCardStatusUpdateAsync(
            [FromBody] BaseStatusUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardStatusUpdate(request);

            CheckHelperResponse(response);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PunchCardStatusUpdate,
            new
            {
                request.Id,
                request.Active
            });

            return OkResponse();
        }

        #endregion 6. PunchCard Status Update

        #region 7. PunchCard Added Into My Rewards Based on UserId
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [Authorize, HttpGet(ActionsConsts.PunchCard.PunchCardAddIntoMyRewardsById)]
        public async Task<IActionResult> PunchCardAddIntoMyRewardsByIdAsync()
        {
            var userId = GetUserId(User);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardAddIntoMyRewardsById(userId.ToString());

            return OkResponse(response);
        }
        #endregion 7. PunchCard Added Into My Rewards Based on UserId

        #region 8. Import PunchCard CSV from QuickBooks

        [Authorize, HttpPost(ActionsConsts.PunchCard.PunchCardCSVImport)]
        public async Task<IActionResult> GetPunchCardCSV([FromForm] IFormFileCollection file)

        {
            var response = CSVService.ReadCSV<ApiPunchCardCSVExportRequest>(file[0].OpenReadStream());
            return OkResponse(response);
        }
        #endregion 8. Import PunchCard CSV from QuickBooks

        #region 9. Insert PunchCard Using CSV

        [Authorize, HttpPost(ActionsConsts.PunchCard.InsertCSVPunchCard)]
        public async Task<IActionResult> PunchCardCSVInsertAsync([FromBody] PunchCardDataList request)
        {
            if (request == null) request = new PunchCardDataList();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);
            var response = await helper.PunchCardCSVsInsert(request);

            if (response == null) return ErrorResponse();
            if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

            //DbContext.SaveChanges();

            return OkResponse();
        }

        #endregion 9. Insert PunchCard Using CSV

        #region 10. PunchCard List without Pagination

        [HttpPost(ActionsConsts.PunchCard.PunchCardList)]
        public async Task<IActionResult> PunchCardGet([FromBody] PunchCardListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PunchCardHelpers(DbContext, Crypto, Mapper, CSVService);

            var response = await helper.PunchCardList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);

        }
        #endregion 10. PunchCard List without Pagination
    }
}
