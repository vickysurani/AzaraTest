namespace azara.api.Helpers;

public class DealsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public DealsHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper = null)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
    }

    #endregion Constructor

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

    #region 1. Insert Deals

    public async Task<BaseResponse> DealsInsert([FromBody] DealsInsertUpdateRequest request)
    {
        if (DbContext.Deals.Any(x => x.Title.ToLower().Equals(request.Title.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_title_exist" });

        await DbContext.AddRangeAsync(new DealsEntity
        {
            Title = request.Title,
            BannerImage = request.BannerImage,
            Description = request.Description,
            StoreId = request.StoreId,
            Position = request.Position.All(a => Char.IsDigit(a)) ? Convert.ToInt32(request.Position) : 0,
            URL = request.URL
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Deals

    #region 2. Update Deals

    public async Task<DealsUpdateResponse> DealsUpdate([FromBody] DealsInsertUpdateRequest request)
    {
        var ad = DbContext.Deals.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (ad == null)
            throw new ApiException("error_advertisement_not_found");

        // Validation for duplicate Name
        if (!ad.Title.Equals(request.Title) && DbContext.Deals.Any(x => x.Title.ToLower().Equals(request.Title.ToLower())))
            throw new ApiException("error_advertisement_title_exist");

        ad.Title = request.Title ?? ad.Title;
        ad.BannerImage = request.BannerImage ?? ad.BannerImage;
        ad.Description = request.Description ?? ad.Description;
        ad.StoreId = request.StoreId ?? ad.StoreId;
        ad.Position = request.Position.All(a => Char.IsDigit(a)) ? Convert.ToInt32(request.Position) : 0;
        ad.URL = request.URL;

        var response = new DealsUpdateResponse
        {
            Title = ad.Title,
            BannerImage = ad.BannerImage,
            Description = ad.Description,
            StoreId = ad.StoreId
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Deals

    #region 3. Get Deals List

    public async Task<PaginationResponse> DealsGetList([FromBody] DealsGetListRequest request)
    {
        var adList = await (
            from A in DbContext.Deals

            where (string.IsNullOrWhiteSpace(request.Title.ToString().ToLower())
                || A.Title.ToLower().Contains(request.Title.ToString().ToLower()))

                && A.Deleted.Equals(request.IsDeleted)

            select new DealsListResponse
            {
                Id = A.Id,
                Title = A.Title,
                BannerImage = A.BannerImage,
                Position = A.Position,
                Active = A.Active,
                URL = A.URL,
                Modified = A.Modified
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (adList == null)
            throw new ApiException("error_advertisement_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    adList = adList.OrderByDescending(x => x.Title).ToList();
                else
                    adList = adList.OrderBy(x => x.Title).ToList();
                break;

            default:

                adList = adList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (adList != null && adList.Any() && request.IsDisplayActive)
            adList = adList.Where(pc => pc.Active.Equals(true)).ToList();

        var total = adList.Count();
        var totalPages = DealsHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = adList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = eventPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 3. Get Deals List

    #region 4. Deals List without Pagination

    public async Task<List<DealsListResponse>> DealsList([FromBody] DealsListRequest request)
    {
        var adList = await (
            from A in DbContext.Deals

            where (string.IsNullOrWhiteSpace(request.Title.ToString().ToLower())
                || A.Title.ToLower().Contains(request.Title.ToString().ToLower()))

                && A.Deleted.Equals(request.IsDeleted)

            select new DealsListResponse
            {
                Id = A.Id,
                Title = A.Title,
                BannerImage = A.BannerImage,
                Position = A.Position,
                Active = A.Active,
                URL = A.URL,
                Modified = A.Modified
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (adList == null)
            throw new ApiException("error_advertisement_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    adList = adList.OrderByDescending(x => x.Title).ToList();
                else
                    adList = adList.OrderBy(x => x.Title).ToList();
                break;

            default:

                adList = adList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (adList != null && adList.Any() && request.IsDisplayActive)
            adList = adList.Where(pc => pc.Active.Equals(true)).ToList();

        return adList;
    }

    #endregion 4. Deals List without Pagination

    #region 5. Delete Deals

    public async Task<BaseResponse> DealsDelete([FromBody] BaseIdRequest request)
    {
        var ad = DbContext.Deals.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (ad == null)
            throw new ApiException("error_advertisement_not_found");

        if (ad.Active == true && ad.Deleted == false)
        {
            throw new ApiException("error_advertisement_active");
        }
        else
        {
            ad.Deleted = true;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Delete Deals

    #region 6. Get Deals

    public async Task<DealsUpdateResponse> GetDealsById([FromBody] DealsIdRequest request)
    {
        var advertisementData = await DbContext.Deals.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (advertisementData == null)
            throw new ApiException("error_advertisement_not_found");

        var response = new DealsUpdateResponse();
        Mapper.Map(advertisementData, response);

        List<string> storeIds = JsonConvert.DeserializeObject<List<string>>(response.StoreId);
        if (storeIds != null && storeIds.Any())
        {
            response.StoreDetails = await DbContext.Stores.Where(w => storeIds.Contains(w.Id.ToString()) && !w.Deleted && w.Active).Select(s => new StoreDetail
            {
                Description = s.Description,
                Image = !string.IsNullOrWhiteSpace(s.Image) ? JsonConvert.DeserializeObject<List<string>>(s.Image).FirstOrDefault() : string.Empty,
                Location = s.Location,
                StoreId = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        if (!string.IsNullOrWhiteSpace(advertisementData.StoreId) && !string.IsNullOrWhiteSpace(request.LocationDetail) && response.StoreDetails != null && response.StoreDetails.Count > 0)
        {
            foreach (var storeData in response.StoreDetails)
            {
                var store = await DbContext.Stores.FirstOrDefaultAsync(x => x.Id.Equals(storeData.StoreId) && !x.Deleted);
                if (store != null && !string.IsNullOrWhiteSpace(store.Longitude) && !string.IsNullOrWhiteSpace(store.Latitude))
                {
                    if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(store.Latitude), Convert.ToDouble(store.Longitude)))
                        storeData.IsNearestLocation = true;
                }
            }
        }

        return response;
    }

    #endregion 6. Get Deals

    #region 7. Deals Status Update

    public async Task<BaseResponse> DealsStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var adsData = await DbContext.Deals.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (adsData == null)
            throw new ApiException("error_advertisement_not_found");

        adsData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Deals Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}