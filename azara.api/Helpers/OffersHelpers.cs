namespace azara.api.Helpers;

public class OffersHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public OffersHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper)
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

    #region 1. Insert Offers

    public async Task<BaseResponse> OffersInsert([FromBody] OffersInsertUpdateRequest request)
    {
        if (DbContext.Offers.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

        await DbContext.AddRangeAsync(new OffersEntity
        {
            Name = request.Name,
            Image = request.Image,
            Description = request.Description,
            Amount = Convert.ToDecimal(request.Amount),
            Created = DateTime.UtcNow
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Offers

    #region 2. Update Offers

    public async Task<OffersUpdateResponse> OffersUpdate([FromBody] OffersInsertUpdateRequest request)
    {
        var offer = DbContext.Offers.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (offer == null)
            throw new ApiException("error_offers_not_found");

        offer.Name = request.Name ?? offer.Name;
        offer.Image = request.Image ?? offer.Image;
        offer.Description = request.Description ?? offer.Description;
        offer.Amount = Convert.ToDecimal(request.Amount);
        offer.Modified = DateTime.UtcNow;

        var response = new OffersUpdateResponse
        {
            Name = offer.Name,
            Image = offer.Image,
            Description = offer.Description,
            Amount = offer.Amount,
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Offers

    #region 3. Offers Get By Id

    public async Task<OffersGetByIdResponse> OffersGetById([FromBody] BaseIdRequest request)
    {
        var offer = await DbContext.Offers.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (offer == null)
            throw new ApiException("error_offers_not_found");

        var response = new OffersGetByIdResponse
        {
            Name = offer.Name,
            Image = offer.Image,
            Description = offer.Description,
            Amount = offer.Amount,
            Active = offer.Active
        };

        return response;
    }

    #endregion 3. Offers Get By Id

    #region 4. Get Offers List

    public async Task<PaginationResponse> OffersGetListAsync([FromBody] OffersGetListRequest request)
    {
        var offersList = await (
            from O in DbContext.Offers

            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || O.Name.ToLower().Contains(request.Name.ToLower()))

                && O.Deleted.Equals(request.IsDeleted)

            select new
            {
                O.Id,
                O.Name,
                O.Image,
                O.Description,
                O.Amount,
                O.Modified,
                O.Active
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (offersList == null)
            throw new ApiException("error_offers_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    offersList = offersList.OrderByDescending(x => x.Name).ToList();
                else
                    offersList = offersList.OrderBy(x => x.Name).ToList();
                break;

            default:

                offersList = offersList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        var total = offersList.Count();
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var contestPaginationList = offersList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = contestPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 4. Get Offers List

    #region 5. Delete Offers

    public async Task<BaseResponse> OffersDelete([FromBody] BaseIdRequest request)
    {
        var offer = DbContext.Offers.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (offer == null)
            throw new ApiException("error_offers_not_found");

        if (offer.Deleted == false)
        {
            offer.Deleted = true;
        }

        offer.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Delete Offers

    #region 6. Offers Status Update

    public async Task<BaseResponse> OffersStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var offerData = await DbContext.Offers.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (offerData == null)
            throw new ApiException("error_offers_not_found");

        offerData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Offers Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}