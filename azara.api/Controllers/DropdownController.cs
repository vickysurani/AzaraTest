namespace azara.api.Controllers;

public class DropdownController : BaseController
{
    #region Object Declaration And Constructor

    public DropdownController(
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

    #region 1. Dropdown for Product Category
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.ProductCategoryDropdown)]
    public async Task<IActionResult> ProductCategorySelectForDropdown()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var helperResponse = await helper.ProductCategoryDropdown();

        var response = JsonConvert.DeserializeObject<List<ApiBaseDropdownResponse>>(helperResponse.Data);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 1. Dropdown for Product Category

    #region 2. Dropdown for Store
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.StoreDropdown)]
    public async Task<IActionResult> StoreSelectForDropdown()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var helperResponse = await helper.StoreDropdown();

        var response = JsonConvert.DeserializeObject<List<ApiBaseDropdownResponse>>(helperResponse.Data);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 2. Dropdown for Store

    #region 3. Dropdown for Product Sub Category
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.ProductSubCategoryDropdown)]
    public async Task<IActionResult> ProductSubCategorySelectForDropdown()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var response = await helper.ProductSubCategoryDropdown();

        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 3. Dropdown for Product Sub Category

    #region 4. Dropdown for Offers
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.OfferLabelDropdown)]
    public async Task<IActionResult> OfferSelectForDropdown()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var helperResponse = await helper.OfferDropdown();

        var response = JsonConvert.DeserializeObject<List<ApiBaseDropdownResponse>>(helperResponse.Data);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 4. Dropdown for Offers

    #region 5. Get Team Dropdown
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.Team)]
    public async Task<IActionResult> TeamDropdownAsync()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var helperResponse = await helper.TeamDropdown();

        var response = JsonConvert.DeserializeObject<List<ApiBaseDropdownResponse>>(helperResponse.Data);

        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }
    #endregion 5. Get Team Dropdown

    #region 6. Dropdown for Rewards
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.Dropdown.RewardsDropdown)]
    public async Task<IActionResult> RewardsSelectForDropdown()
    {
        using var helper = new DropdownHelpers(DbContext, Crypto);

        var helperResponse = await helper.RewardsDropdown();

        var response = JsonConvert.DeserializeObject<List<ApiBaseDropdownResponse>>(helperResponse.Data);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 6. Dropdown for Rewards
}