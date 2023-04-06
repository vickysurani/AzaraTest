namespace azara.api.Helpers;

public class CouponsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public CouponsHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper,
        ICSVService csvService)
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

    #region 1. Insert Coupons

    public async Task<BaseResponse> CouponsInsert([FromBody] CouponsInsertUpdateRequest request)
    {
        if (DbContext.Coupons.Any(x => x.CouponTitle.ToLower().Equals(request.CouponTitle.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_title_exist" });

        await DbContext.AddRangeAsync(new CouponsEntity
        {
            CouponTitle = request.CouponTitle,
            CouponImage = request.CouponImage,
            ExpiryDate = request.ExpiryDate,
            Description = request.Description,
            Amount = Convert.ToDecimal(request.Amount),
            CouponCode = request.CouponCode,
            Active = true,
            //CouponsPoint = request.CouponsPoint
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Coupons

    #region 2. Update Coupons

    public async Task<CouponsUpdateResponse> CouponsUpdate([FromBody] CouponsInsertUpdateRequest request)
    {
        var coupon = DbContext.Coupons.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (coupon == null)
            throw new ApiException("error_coupon_not_found");

        coupon.CouponTitle = request.CouponTitle ?? coupon.CouponTitle;
        coupon.CouponImage = request.CouponImage ?? coupon.CouponImage;
        coupon.ExpiryDate = DateTime.UtcNow.AddDays(21);
        coupon.Description = request.Description ?? coupon.Description;
        coupon.Amount = Convert.ToDecimal(request.Amount);
        coupon.CouponCode = request.CouponCode;
        coupon.Active = request.Active;
        coupon.Modified = DateTime.UtcNow;
        //coupon.CouponsPoint = request.CouponsPoint;

        var response = new CouponsUpdateResponse
        {
            CouponTitle = coupon.CouponTitle,
            CouponImage = coupon.CouponImage,
            ExpiryDate = DateTime.UtcNow.AddDays(21),
            Description = coupon.Description,
            Amount = coupon.Amount,
            CouponCode = coupon.CouponCode,
            Active = coupon.Active,
            //CouponsPoint = coupon.CouponsPoint,
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Coupons

    #region 3. Coupons Get By Id

    public async Task<CouponsGetByIdResponse> CouponsGetById([FromBody] BaseIdRequest request)
    {
        var coupon = await DbContext.Coupons.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (coupon == null)
            throw new ApiException("error_coupon_not_found");

        var response = new CouponsGetByIdResponse
        {
            CouponTitle = coupon.CouponTitle,
            CouponImage = coupon.CouponImage,
            ExpiryDate = coupon.ExpiryDate,
            Description = coupon.Description,
            Amount = coupon.Amount,
            CouponCode = coupon.CouponCode,
            Active = coupon.Active,
            //CouponsPoint = coupon.CouponsPoint
        };

        return response;
    }

    #endregion 3. Coupons Get By Id

    #region 4. Get Coupons List

    public async Task<PaginationResponse> CouponsGetListAsync([FromBody] CouponsGetListRequest request)
    {
        var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.CouponsId);

        var couponsList = await (
            from C in DbContext.Coupons

            where (string.IsNullOrWhiteSpace(request.CouponTitle.ToString().ToLower())
                || C.CouponTitle.ToLower().Contains(request.CouponTitle.ToLower()))

                && C.Deleted.Equals(request.IsDeleted)

            select new CouponsListResponse
            {
                Id = C.Id,
                CouponTitle = C.CouponTitle,
                CouponImage = C.CouponImage,
                ExpiryDate = C.ExpiryDate,
                Description = C.Description,
                Amount = C.Amount,
                CouponCode = C.CouponCode,
                Modified = C.Modified,
                Created = C.Created,
                Active = C.Active,
                IsUsed = rewards.Contains(C.Id),
                CouponsPoint = C.CouponsPoint
            }).OrderByDescending(x => x.Created).ToListAsync();

        if (couponsList == null)
            throw new ApiException("error_coupon_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    couponsList = couponsList.OrderByDescending(x => x.CouponTitle).ToList();
                else
                    couponsList = couponsList.OrderBy(x => x.CouponTitle).ToList();
                break;

            default:

                couponsList = couponsList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (couponsList != null && couponsList.Any() && request.IsDisplayActive)
            couponsList = couponsList.Where(pc => pc.Active.Equals(true)).ToList();

        var total = couponsList.Count();
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var contestPaginationList = couponsList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Coupons List

    #region 5. Coupons List Without Pagination

    public async Task<List<CouponsListResponse>> CouponsList([FromBody] CouponsListRequest request)
    {
        var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.CouponsId);

        var couponsList = await (
            from C in DbContext.Coupons

            where (string.IsNullOrWhiteSpace(request.CouponTitle.ToString().ToLower())
                || C.CouponTitle.ToLower().Contains(request.CouponTitle.ToLower()))

                && C.Deleted.Equals(request.IsDeleted)

            select new CouponsListResponse
            {
                Id = C.Id,
                CouponTitle = C.CouponTitle,
                CouponImage = C.CouponImage,
                ExpiryDate = C.ExpiryDate,
                Description = C.Description,
                Amount = C.Amount,
                CouponCode = C.CouponCode,
                Modified = C.Modified,
                Created = C.Created,
                Active = C.Active,
                IsUsed = rewards.Contains(C.Id),
                CouponsPoint = C.CouponsPoint
            }).OrderByDescending(x => x.Created).ToListAsync();

        if (couponsList == null)
            throw new ApiException("error_coupon_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    couponsList = couponsList.OrderByDescending(x => x.CouponTitle).ToList();
                else
                    couponsList = couponsList.OrderBy(x => x.CouponTitle).ToList();
                break;

            default:

                couponsList = couponsList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (couponsList != null && couponsList.Any() && request.IsDisplayActive)
            couponsList = couponsList.Where(pc => pc.Active.Equals(true)).ToList();

        return couponsList;
    }

    #endregion 5. Coupons List Without Pagination

    #region 6. Delete Coupons

    public async Task<BaseResponse> CouponsDelete([FromBody] BaseIdRequest request)
    {
        var coupon = DbContext.Coupons.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (coupon == null)
            throw new ApiException("error_coupon_not_found");

        if (coupon.Active == true && coupon.Deleted == false)
        {
            throw new ApiException("error_coupon_active");
        }
        else
        {
            coupon.Deleted = true;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Coupons

    #region 7. Coupons Status Update

    public async Task<BaseResponse> CouponsStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var couponData = await DbContext.Coupons.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (couponData == null)
            throw new ApiException("error_coupon_not_found");

        couponData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Coupons Status Update

    #region 8. Coupons Add Into MyRewards Based on UserId

    public async Task<List<CouponsGetByIdResponse>> CouponsAddIntoMyRewardsById(string userId)
    {
        var coupon = await (from C in DbContext.Coupons
                            join MR in DbContext.MyReward on C.Id equals MR.CouponsId
                            where MR.UserId == new Guid(userId) && !C.Deleted && C.Active
                            select new CouponsGetByIdResponse
                            {
                                CouponTitle = C.CouponTitle,
                                CouponImage = C.CouponImage,
                                ExpiryDate = C.ExpiryDate,
                                Description = C.Description,
                                Amount = C.Amount,
                                CouponCode = C.CouponCode,
                                Active = C.Active,
                                //CouponsPoint = C.CouponsPoint
                            }).ToListAsync();

        if (coupon == null)
            throw new ApiException("error_coupon_not_found");

        return coupon;
    }

    #endregion 8. Rewards Add Into MyRewards Based on UserId

    #region 9. Insert Coupons CSV

    public async Task<BaseResponse> CouponCSVsInsert([FromBody] CouponsDataList request)
    {
        foreach (var coupon in request.Details)
        {
            if (!DbContext.Coupons.Any(x => x.CouponTitle.ToLower().Equals(coupon.CouponTitle.ToLower())))
            {
                await DbContext.AddRangeAsync(new CouponsEntity
                {
                    CouponTitle = coupon.CouponTitle,
                    CouponImage = "jksdgfksd",
                    ExpiryDate = DateTime.UtcNow.AddDays(21),
                    Description = "sjhefgjlsdghf",
                    Amount = Convert.ToDecimal(coupon.Amount),
                    CouponCode = coupon.CouponCode,
                    Active = true,
                    //CouponsPoint = coupon.CouponsPoint
                });

                await DbContext.SaveChangesAsync();
            }
        }
        return new BaseResponse { IsSuccess = true, Data = "response_data_added_successfully" };
    }

    #endregion 9. Insert Coupons

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}