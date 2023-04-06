using azara.models.Responses.Dashboard;

namespace azara.api.Controllers
{
    public class DashboardController : BaseController
    {
        #region Object Declaration And Constructor

        public DashboardController(
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

        #region 1. Dashboard Count
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [Authorize, HttpGet(ActionsConsts.Dashboard.DashboardCount)]
        public async Task<IActionResult> RetrieveDashboardCountAsync()
        {
            using var helper = new DashboardHelpers(DbContext, Crypto);
            var helperResponse = await helper.DashboardData();

            if (helperResponse == null) return ErrorResponse();
            if (!helperResponse.IsSuccess) return ErrorResponse(helperResponse.ErrorMessage);

            var response = JsonConvert.DeserializeObject<DashboardResponse>(helperResponse.Data);

            return OkResponse(response);
        }

        #endregion
    }
}
