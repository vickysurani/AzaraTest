namespace azara.api.Controllers.Service;

public class BlueCheckController : BaseController
{
    #region Object Declaration And Constructor

    public BlueCheckController(
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

    [AllowAnonymous, HttpPost("VerifyAgeWithBlueCheck")]
    public async Task<IActionResult> Verify(BlueCheckVerifyModel request)
    {
        if (request == null) request = new BlueCheckVerifyModel();

        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new BlueCheckHelpers(DbContext, Crypto, Mapper);

        var response = await helper.VerifyWithBlueCheck(request);

        //response.Token = new AdminTokenHelpers(Crypto).GetAccessToken(AuthConfigOptions.Value, response, request.UniqueId);

        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }
}