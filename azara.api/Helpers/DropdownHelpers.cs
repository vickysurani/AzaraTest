namespace azara.api.Helpers;

public class DropdownHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public DropdownHelpers(
        AzaraContext DbContext,
        ICrypto Crypto)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
    }

    #endregion Constructor

    #region 1. Dropdown for Product Category

    public async Task<BaseResponse> ProductCategoryDropdown()
    {
        var response = DbContext.ProductCategories
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new
            {
                x.Name,
                x.Id
            });

        return new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) };
    }

    #endregion 1. Dropdown for Product Category

    #region 2. Dropdown for Store

    public async Task<BaseResponse> StoreDropdown()
    {
        var response = DbContext.Stores
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new
            {
                x.Name,
                x.Id
            });

        return new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) };
    }

    #endregion 2. Dropdown for Store

    #region 3. Dropdown for Product Sub Category

    public async Task<List<ApiBaseDropdownResponse>> ProductSubCategoryDropdown()
    {
        return await DbContext.ProductSubCategory
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new ApiBaseDropdownResponse
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
    }

    #endregion 3. Dropdown for Product Sub Category

    #region 4. Dropdown for Offers

    public async Task<BaseResponse> OfferDropdown()
    {
        var response = DbContext.Offers
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new
            {
                x.Name,
                x.Id
            });

        return new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) };
    }

    #endregion 4. Dropdown for Offers

    #region 5. Get Team Dropdown

    public async Task<BaseResponse> TeamDropdown()
    {
        var response = DbContext.Team
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new
            {
                x.Name,
                x.Id
            });

        return new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) };
    }

    #endregion 5. Get Team Dropdown

    #region 6. Dropdown for Rewards
    public async Task<BaseResponse> RewardsDropdown()
    {
        var response = DbContext.Rewards
            .Where(x => !x.Deleted && x.Active)
            .Select(x => new
            {
                x.Name,
                x.Id
            });

        return new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) };
    }

    #endregion 6. Dropdown for Rewards

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}