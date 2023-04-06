using azara.models.Requests.POSCustomer;
using azara.models.Requests.POSInventory;
using azara.models.Responses.POSCustomer;

namespace azara.api.Controllers
{

    public class POSCustomerController : BaseController
    {
        #region Object Declaration And Constructor
        public POSCustomerController(
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

        #region 1. POSCustomer Get By Id

        [HttpPost(ActionsConsts.POSCustomer.GetByIdPOSCustomer)]
        public async Task<IActionResult> GetByIdPOSCustomerAsync([FromBody] POSCustomerGetByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSCustomerHelpers(DbContext, Crypto);

            var response = await helper.POSCustomerGetById(request);

            return OkResponse(response);
        }

        #endregion 1. POSCustomer Get By Id

        #region 2. POS Customer GetList with Pagination

        [HttpPost(ActionsConsts.POSCustomer.GetListCustomerPos)]
        public async Task<IActionResult> GetListCustomerPosAsync([FromBody] POSInventotyGetListRequest request)
        {
            #region Entity Framework

            var customerList = DbContext.POSCustomer.Where(x => string.IsNullOrEmpty(request.SearchParam) ||
                                                             x.FirstName.Contains(request.SearchParam))
                                                      .Select(x => new POSCustomerGetList
                                                      {
                                                          Id = x.Id,
                                                          ListId = x.ListId,
                                                          TimeCreated = x.TimeCreated,
                                                          TimeModified = x.TimeModified,
                                                          AccountBalance = x.AccountBalance,
                                                          AccountLimit = x.AccountLimit,
                                                          AmountPastDue = x.AmountPastDue,
                                                          CompanyName = x.CompanyName,
                                                          CustomerDiscType = x.CustomerDiscType,
                                                          CustomerType = x.CustomerType,
                                                          CustomerID = x.CustomerID,
                                                          DefaultShipAddress = x.DefaultShipAddress,
                                                          EMail = x.EMail,
                                                          FirstName = x.FirstName,
                                                          FullName = x.FullName,
                                                          IsAcceptingChecks = x.IsAcceptingChecks,
                                                          IsNoShipToBilling = x.IsNoShipToBilling,
                                                          IsOkToEMail = x.IsOkToEMail,
                                                          IsRewardsMember = x.IsRewardsMember,
                                                          IsUsingChargeAccount = x.IsUsingChargeAccount,
                                                          IsUsingWithQB = x.IsUsingWithQB,
                                                          LastName = x.LastName,
                                                          LastSale = x.LastSale,
                                                          Notes = x.Notes,
                                                          Phone = x.Phone,
                                                          Phone2 = x.Phone2,
                                                          Phone3 = x.Phone3,
                                                          Phone4 = x.Phone4,
                                                          PriceLevelNumber = x.PriceLevelNumber,
                                                          Salutation = x.Salutation,
                                                          StoreExchangeStatus = x.StoreExchangeStatus,
                                                          TaxCategory = x.TaxCategory,
                                                          WebNumber = x.WebNumber,
                                                          BillAddressCity = x.BillAddressCity,
                                                          BillAddressCountry = x.BillAddressCountry,
                                                          BillAddressPostalCode = x.BillAddressPostalCode,
                                                          BillAddressStreet = x.BillAddressStreet,
                                                          BillAddressState = x.BillAddressState,
                                                          BillAddressStreet2 = x.BillAddressStreet2,
                                                          CustomFieldOther = x.CustomFieldOther,
                                                          CustomFieldSalesLocation = x.CustomFieldSalesLocation,
                                                          CustomFieldSerial = x.CustomFieldSerial,
                                                          CustomFieldSerial1 = x.CustomFieldSerial1,
                                                          CustomFieldSerial2 = x.CustomFieldSerial2
                                                      }).AsNoTracking().ToList();

            var total = customerList.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var customerPaginationList = customerList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = customerPaginationList
            };

            var paginationResponse = new PaginationResponse
            {
                Total = response.Total,
                TotalPages = response.Total,
                OffSet = response.Offset,
                Details = response.Details
            };

            return OkResponse(paginationResponse);

            #endregion
        }

        #endregion 2. POS Customer GetList with Pagination

        #region 3. POSCustomer Add Update from POSCustomertemp to customer
        //Temptory Table to POSCustomer Add/Update

        [HttpGet(ActionsConsts.POSCustomerData.POSCustomerAddUpdate)]
        public async Task<IActionResult> POSCustomerAddUpdate()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);


            using var helper = new POSCustomerHelpers(DbContext, Crypto);

            var response = await helper.POSCustomerAddUpdate();

            return OkResponse(response);
        }

        #endregion
    }
}
