namespace azara.api.Controllers
{
    public class POSCustomerRewardsController : BaseController
    {
        #region Object Declaration And Constructor

        public POSCustomerRewardsController(
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

        #region 2. Get POSCustomerReward List

        [HttpPost(ActionsConsts.POSCustomerData.POSCustomerRewardList)]
        public async Task<IActionResult> POSCustomerRewardListAsync([FromBody] PaginationRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSCustomerRewardsHelper(DbContext, Crypto);

            var response = await helper.POSCustomerRewardsGetListAsync(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 2. Get POSCustomerReward List

        #region 3. Get POSCustomer List

        [HttpPost(ActionsConsts.POSCustomerData.POSCustomernewList)]
        public async Task<IActionResult> POSCustomernewListAsync([FromBody] PaginationRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSCustomerRewardsHelper(DbContext, Crypto);

            var response = await helper.POSCustomerGetListAsync(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 3. Get POSCustomer List


        #region 4. Get POSInventory List

        [HttpPost(ActionsConsts.POSCustomerData.POSInventoryListNew)]
        public async Task<IActionResult> POSInventoryListAsync([FromBody] PaginationRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSCustomerRewardsHelper(DbContext, Crypto);

            var response = await helper.POSInventoryAsync(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 3. Get POSInventory List
    }
}
