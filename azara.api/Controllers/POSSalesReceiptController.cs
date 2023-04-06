namespace azara.api.Controllers
{
    public class POSSalesReceiptController : BaseController
    {
        #region Object Declaration And Constructor

        public POSSalesReceiptController(
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

        #region 1. POSSalesReceipt Add Update
        //Temptory Table to POSSalesReceipt Add/Update

        [HttpGet(ActionsConsts.POSSalesReceipt.POSSalesReceiptAddUpdate)]
        public async Task<IActionResult> POSSalesReceiptAddUpdate()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);


            using var helper = new POSSalesReceiptHelpers(DbContext, Crypto);

            var response = await helper.POSSalesReceiptAddUpdate();

            return OkResponse(response);
        }

        #endregion

        #region 2. Gives a reward based on sales receipt

        [HttpGet(ActionsConsts.POSSalesReceipt.POSSalesReceiptCustomerRewards)]
        public async Task<IActionResult> POSSalesReceiptCustomerRewards()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSSalesReceiptHelpers(DbContext, Crypto);

            await helper.POSSalesReceiptCustomerRewards();

            return OkResponse();
        }

        #endregion
    }
}
