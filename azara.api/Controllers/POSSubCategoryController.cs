using azara.models.Requests.POSInventory;
using azara.models.Requests.POSSubCategory;
using azara.models.Responses.POSSubCategory;

namespace azara.api.Controllers
{

    public class POSSubCategoryController : BaseController
    {
        #region Object Declaration And Constructor

        public POSSubCategoryController(
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

        #region 1. Insert And Update into POSSubCategory from quickbooks
        //get the data from posinventory and load into the sub category 

        [HttpGet(ActionsConsts.POSSubCategory.POSSubCategoryInsertAndUpdate)]
        public async Task<IActionResult> POSSubCategoryInsertAndUpdate()
        {
            
            var helper = new POSSubCategoryHelpers(DbContext, Crypto);

            var response = await helper.POSSubCategoryInsertAndUpdate();

            return OkResponse(response);
        }

        #endregion

        #region 2. POSSubCategory List Witout Pagination
        [HttpPost(ActionsConsts.POSSubCategory.POSSubCategoryList)]
        public async Task<IActionResult> POSSubCategoryListAsync([FromBody] POSInventoryDistinctSubCategoryList request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSSubCategoryHelpers(DbContext, Crypto);

            var response = await helper.POSSubCategoryList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }
        #endregion

        #region 3.POSSubCategory List With Pagination
        [HttpPost(ActionsConsts.POSSubCategory.POSSubCategoryGetList)]
        public async Task<IActionResult> GetPOSSubCategoryListAsync([FromBody] POSInventotyGetListRequest request)
        {           
            var groupedCustomers = await DbContext.POSSubCategory
             .GroupBy(c => new { c.Name, c.DepartmentName })             
             .Select(g => new POSSubCategoryListResponse
             {
                 Name = g.Key.Name,
                 DepartmentListName = g.Key.DepartmentName,
                 Image = g.FirstOrDefault().Image,
                 Id = g.FirstOrDefault().Id,
                 Active = g.FirstOrDefault().Active,
                 Attribute = g.FirstOrDefault().Attribute,
                 DepartmentListId = g.FirstOrDefault().DepartmentListId
             })
            .Where(x => string.IsNullOrEmpty(request.SearchParam) || x.Name.Contains(request.SearchParam))
            .ToListAsync();


            var total = groupedCustomers.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var subCategoryPaginationList = groupedCustomers.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = subCategoryPaginationList
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
        #endregion

        #region 4. POSSubCategory Get By Id
        [HttpPost(ActionsConsts.POSSubCategory.POSSubCategoryGetById)]
        public async Task<IActionResult> POSSubCategoryById([FromBody] POSSubCategoryByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSSubCategoryHelpers(DbContext, Crypto);

            var response = await helper.POSSubCategoryGetById(request);

            return OkResponse(response);
        }
        #endregion

        #region 5. POSSubCategory Update 
        [HttpPost(ActionsConsts.POSSubCategory.POSSubCategoryUpdate)]
        public async Task<IActionResult> POSSubCategoryUpdateAsync(
            [FromBody] POSSubCategoryUpdateRequest request)
        {
            if (request == null) request = new POSSubCategoryUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSSubCategoryHelpers(DbContext, Crypto);

            var response = await helper.POSSubCategoryUpdate(request);

            if (response == null) return ErrorResponse();

            return OkResponse(response);
        }
        #endregion

        #region POSSubCategory Status Update
        [HttpPost(ActionsConsts.POSSubCategory.POSSubCategoryStatusUpdate)]
        public async Task<IActionResult> POSSubCategoryStatusUpdateAsync(
           [FromBody] BaseIntStatusUpdateRequest request,
           [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSSubCategoryHelpers(DbContext, Crypto);

            var response = await helper.POSSubCategoryStatusUpdate(request);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.POSCategortStatus,
            new
            {
                request.Id,
            });

            return OkResponse();
        }
        #endregion


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
    }


}
