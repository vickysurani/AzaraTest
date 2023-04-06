using azara.models.Requests.POSDepartment;
using azara.models.Responses.POSDepartment;

namespace azara.api.Controllers
{
    public class POSDepartmentController : BaseController
    {
        #region Object Declaration And Constructor

        public POSDepartmentController(
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

        #region Calculate Total Pages

        public static int CalculateTotalPages(
            int total,
            int? pageSize)
        {
            var pages = Convert.ToDecimal(total) / Convert.ToDecimal(pageSize);
            var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

            return response;
        }

        #endregion Calculate Total Pages

        #region 1. POSDepartment Get By Id
        [HttpPost(ActionsConsts.POSDepartment.GetByIdDepartmentPos)]
        public async Task<IActionResult> DepartmentGetById([FromBody] POSDepartmentGetByIdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponse(ModelState);
            }

            using var helper = new POSDepartmentHelper(DbContext, Crypto);

            var response = await helper.POSDepartmentGetByID(request);

            return OkResponse(response);
        }

        #endregion POSDepartmentList

        #region 2. POSDepartment GetList with Pagination

        [HttpPost(ActionsConsts.POSDepartment.GetListDepartmentPos)]
        public async Task<IActionResult> GetDepartmentListAsync([FromBody] POSDepartmentGetListRequest request)
        {
            var departmentList = from D in DbContext.POSDepartments
                                 where string.IsNullOrEmpty(request.SearchParam) || D.DepartmentName.Contains(request.SearchParam)
                                 select new POSDepartmentGetByIdResponse
                                 {
                                     Id = D.Id,
                                     ListId = D.ListId,
                                     TimeCreated = D.TimeCreated,
                                     TimeModified = D.TimeModified,
                                     DefaultMarginPercent = D.DefaultMarginPercent,
                                     DefaultMarkupPercent = D.DefaultMarkupPercent,
                                     DepartmentCode = D.DepartmentCode,
                                     TaxCode = D.TaxCode,
                                     DepartmentName = D.DepartmentName,
                                     StoreExchangeStatus = D.StoreExchangeStatus,
                                     Image = D.Image,
                                     IsActive = D.Active
                                 };

            var total = departmentList.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var departmentPaginationList = departmentList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = departmentPaginationList
            };

            var paginationResponse = new PaginationResponse
            {
                Total = response.Total,
                TotalPages = response.Total,
                OffSet = response.Offset,
                Details = response.Details
            };

            return OkResponse(paginationResponse);
        }


        #endregion 2. POSDepartment GetList with Pagination

        #region 3. POS Department Update
        [HttpPost(ActionsConsts.POSDepartment.UpdateDepartmentPos)]
        public async Task<IActionResult> DepartmentUpdateAsync(
            [FromBody] POSDepartmentUpdateRequest request, [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new POSDepartmentUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSDepartmentHelper(DbContext, Crypto);

            var response = await helper.POSDepartmentUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.POSCategoryUpdate,
            new
            {
                request.Id,
            });
            return OkResponse(response);
        }
        #endregion

        #region 4. Department List without Pagination
        [HttpPost(ActionsConsts.POSDepartment.DepartmentPosList)]
        public async Task<IActionResult> POSDepartmentListAsync([FromBody] POSDepartmentListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSDepartmentHelper(DbContext, Crypto);

            var response = await helper.POSDepartmentList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 4. Department List without Pagination

        #region 5. POSDepartment Add Update
        //Temptory Table to POSDepartment Add/Update

        [HttpGet(ActionsConsts.POSDepartment.POSDepartmentAddUpdate)]
        public async Task<IActionResult> POSDepartmentAddUpdate()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);


            using var helper = new POSDepartmentHelper(DbContext, Crypto);

            var response = await helper.POSDepartmentAddUpdate();

            return OkResponse(response);
        }

        #endregion

        #region 6. POSDepartment status Update

        [Authorize, HttpPost(ActionsConsts.POSDepartment.POSDepartmentStatusUpdate)]
        public async Task<IActionResult> POSDepartmentStatusUpdateAsync(
            [FromBody] BaseListIdStatusUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSDepartmentHelper(DbContext, Crypto);

            var response = await helper.POSDepartmentStatusUpdate(request);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.POSCategortStatus,
            new
            {
                request.Id,
            });

            return OkResponse();
        }

        #endregion
    }
}