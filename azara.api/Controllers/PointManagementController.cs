using azara.models.Requests.PointManagement;

namespace azara.api.Controllers
{
    public class PointManagementController : BaseController
    {
        #region Object Declaration And Constructor

        public PointManagementController(
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

        #region 1. Insert

        [Authorize, HttpPost(ActionsConsts.PointManagement.PointInsert)]
        public async Task<IActionResult> PointsInsert(
            [FromBody] PointManagementInsertUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            try
            {
                if (request == null) request = new PointManagementInsertUpdateRequest();
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);

                using var helper = new PointManagementHelpers(DbContext, Crypto);
                var response = await helper.PointsInsert(request);

                if (response == null) return ErrorResponse();
                if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

                await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointManagementInsert,
                    new
                    {
                        request.Id,
                        request.Name,
                        request.Point,
                    });

                //DbContext.SaveChanges();

                return OkResponse();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion 1. Insert

        #region 2. Update

        [Authorize, HttpPost(ActionsConsts.PointManagement.PointUpdate)]
        public async Task<IActionResult> PointsUpdate(
            [FromBody] PointManagementInsertUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new PointManagementInsertUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PointManagementHelpers(DbContext, Crypto);

            var response = await helper.PointsUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointManagementUpdate,
                new
                {
                    request.Id,
                });

            return OkResponse(response);
        }

        #endregion 2. Update

        #region 3. Points Get By Id

        [HttpPost(ActionsConsts.PointManagement.PointGetById)]
        public async Task<IActionResult> PointsGetBtyIdAsync([FromBody] BaseIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PointManagementHelpers(DbContext, Crypto);

            var response = await helper.PointsGetById(request);

            return OkResponse(response);
        }

        #endregion 3. Points Get By Id

        #region 4. Get Points List With Pagination

        [HttpPost(ActionsConsts.PointManagement.PointList)]
        public async Task<IActionResult> PointsGetList([FromBody] PointManagementGetListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PointManagementHelpers(DbContext, Crypto);

            var response = await helper.PointsGetList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 4. Get Points List With Pagination

        #region 5. Point List without Pagination

        [HttpPost(ActionsConsts.PointManagement.PointLists)]
        public async Task<IActionResult> StoreGet([FromBody] PointManagementListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PointManagementHelpers(DbContext, Crypto);

            var response = await helper.PointsList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);

        }
        #endregion 5. Point List without Pagination

        #region 6. Points Delete

        [HttpPost(ActionsConsts.PointManagement.PointDelete)]
        public async Task<IActionResult> PointsDelete(
            [FromBody] BaseIdRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new PointManagementHelpers(DbContext, Crypto);

            var response = await helper.PointsDelete(request);
            if (response is null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.PointManagementDelete,
                new
                {
                    request.Id,
                    IsDeleted = true
                });

            return OkResponse(response);
        }

        #endregion 6. Points Delete
    }
}
