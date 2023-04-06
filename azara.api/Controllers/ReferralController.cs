namespace azara.api.Controllers;

public class ReferralController : BaseController
{
    #region Object Declaration And Constructor

    public ReferralController(
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

    #region 1. Referral Insert

    [Authorize, HttpPost(ActionsConsts.Referral.InsertReferral)]
    public async Task<IActionResult> ReferralInsertAsync([FromBody] ReferralUpdateRequest request)
    {
        if (request == null) request = new ReferralUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ReferralHelpers(DbContext, Crypto);
        var response = await helper.ReferralInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Referral Insert

    #region 2. Referral Update

    [Authorize, HttpPost(ActionsConsts.Referral.UpdateReferral)]
    public async Task<IActionResult> ReferralUpdateAsync([FromBody] ReferralUpdateRequest request)
    {
        if (request == null) request = new ReferralUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ReferralHelpers(DbContext, Crypto);

        var response = await helper.ReferralUpdate(request);

        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 2. Referral Update

    #region 3. Referral Get by Id

    [HttpPost(ActionsConsts.Referral.ReferralGetById)]
    public async Task<IActionResult> ReferralGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new ReferralHelpers(DbContext, Crypto);

        var response = await helper.ReferralGetById(request);

        return OkResponse(response);
    }

    #endregion 3. Referral Get by Id
}