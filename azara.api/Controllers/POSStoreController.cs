using azara.models.Requests.POSStore;
using azara.models.Responses.POSStore;

namespace azara.api.Controllers
{
    public class POSStoreController : BaseController
    {
        #region Object Declaration And Constructor
        public POSStoreController(IConfiguration configrations,
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

        #region 1. POSStore Get By Id

        [HttpPost(ActionsConsts.POSStore.GetByIdStorePos)]
        public async Task<IActionResult> StorePosGetById([FromBody] POSStoreGetByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var helper = new POSStoreHelper(DbContext, Crypto, Mapper);

            var response = await helper.POSStoreGetById(request);

            return OkResponse(response);
        }

        #endregion 1. POSStore Get By Id

        #region 2. POSStore Get List With Pagination
        [HttpPost(ActionsConsts.POSStore.GetListStorePos)]
        public async Task<IActionResult> GetStoreList([FromBody] POSStoreListRequest request)
        {

            var posstoreList = from S in DbContext.POSStores
                               select new POSStoreGetByIdResponse
                               {
                                   Name = S.Name,
                                   OnHandStore01 = S.OnHandStore01,
                                   OnHandStore02 = S.OnHandStore02,
                                   OnHandStore03 = S.OnHandStore03,
                                   OnHandStore04 = S.OnHandStore04,
                                   OnHandStore05 = S.OnHandStore05,
                                   OnHandStore06 = S.OnHandStore06,
                                   OnHandStore07 = S.OnHandStore07,
                                   OnHandStore08 = S.OnHandStore08,
                                   OnHandStore09 = S.OnHandStore09,
                                   OnHandStore10 = S.OnHandStore10,
                                   OnHandStore11 = S.OnHandStore11,
                                   OnHandStore12 = S.OnHandStore12,
                                   OnHandStore13 = S.OnHandStore13,
                                   OnHandStore14 = S.OnHandStore14,
                                   OnHandStore15 = S.OnHandStore15,
                                   OnHandStore16 = S.OnHandStore16,
                                   OnHandStore17 = S.OnHandStore17,
                                   OnHandStore18 = S.OnHandStore18,
                                   OnHandStore19 = S.OnHandStore19,
                                   OnHandStore20 = S.OnHandStore20,
                                   StoreNumber = S.StoreNumber,
                               };

            var total = posstoreList.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var posstoreListPagination = posstoreList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = posstoreListPagination
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

        #endregion 2. POSStore Get List With Pagination

        #region 3. POSStore Get List Without Pagination
        [HttpPost(ActionsConsts.POSStore.StorePosList)]
        public async Task<IActionResult> POSStoreListAsync([FromBody] POSStoreGetListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var helper = new POSStoreHelper(DbContext, Crypto, Mapper);

            var response = await helper.POSStoreGetListAsync(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);

        }

        #endregion 3. POSStore Get List Without Pagination

        #region 4. POSStore Update
        [HttpPost(ActionsConsts.POSStore.UpdateStorePos)]
        public async Task<IActionResult> POSStoreUpdateAsync([FromBody] POSStoreUpdateRequest request)
        {
            if (request == null) request = new POSStoreUpdateRequest();

            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var helper = new POSStoreHelper(DbContext, Crypto, Mapper);

            var response = await helper.POSStoreUpdate(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);


        }
        #endregion 4. POSStore Update

    }
}
