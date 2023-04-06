namespace azara.api.Helpers;

public class RewardsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    ICSVService Service { get; set; }

    public RewardsHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper,
        ICSVService CSVService)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
        this.Service = CSVService;
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

    #region 1. Insert Rewards

    public async Task<BaseResponse> RewardsInsert([FromBody] RewardsInsertUpdateRequest request)
    {
        if (DbContext.Rewards.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

        await DbContext.AddRangeAsync(new RewardsEntity
        {
            Name = request.Name,
            Image = request.Image,
            Description = request.Description,
            Barcode = request.Barcode,
            PointId = request.PointId,
            Active = true,
            //RewardsExpiryDate = request.RewardsExpiryDate,
            RewardsPoint = request.RewardsPoint
            //IsUsed = request.IsUsed
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Rewards

    #region 2. Update Rewards

    public async Task<RewardsUpdateResponse> RewardsUpdate([FromBody] RewardsInsertUpdateRequest request)
    {
        var rewards = DbContext.Rewards.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (rewards == null)
            throw new ApiException("error_rewards_not_found");

        rewards.Name = request.Name ?? rewards.Name;
        rewards.Image = request.Image ?? rewards.Image;
        rewards.Description = request.Description ?? rewards.Description;
        rewards.Barcode = request.Barcode ?? rewards.Barcode;
        rewards.UserId = request.UserId ?? rewards.UserId;
        rewards.PointId = request.PointId ?? rewards.PointId;
        rewards.Active = request.Status;
        //rewards.RewardsExpiryDate = request.RewardsExpiryDate;
        rewards.RewardsPoint = request.RewardsPoint;
        //request.IsUsed = request.IsUsed;

        var response = new RewardsUpdateResponse
        {
            Name = rewards.Name,
            Image = rewards.Image,
            Description = rewards.Description,
            Barcode = rewards.Barcode,
            UserId = rewards.UserId,
            PointId = rewards.PointId,
            Status = rewards.Active,
            //RewardsExpiryDate = rewards.RewardsExpiryDate,
            RewardsPoint = rewards.RewardsPoint
            //IsUsed = rewards.IsUsed
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Rewards

    #region 3. Rewards Get By Id
    public async Task<RewardsGetByIdResponse> RewardsGetById([FromBody] BaseIdRequest request)
    {
        var rewards = await DbContext.Rewards.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (rewards == null)
            throw new ApiException("error_rewards_not_found");

        var response = new RewardsGetByIdResponse
        {
            Name = rewards.Name,
            Image = rewards.Image,
            Description = rewards.Description,
            Barcode = rewards.Barcode,
            UserId = rewards.UserId,
            PointId = rewards.PointId,
            Created = rewards.Created,
            Status = rewards.Active,
            //RewardsExpiryDate = rewards.RewardsExpiryDate,
            RewardsPoint = rewards.RewardsPoint
        };

        return response;
    }
    #endregion 3. Rewards Get By Id

    #region 4. Rewards Add Into MyRewards Based on UserId

    public async Task<List<RewardsGetByIdResponse>> RewardsAddIntoMyRewardsById(string userId)
    {
        var rewards = await DbContext.MyReward
            .Where(w => w.UserId.Equals(new Guid(userId)) && w.Active)
            .Include(i => i.RewardsEntity)
            .Include(i => i.CouponsEntity)
            .Select(s => new RewardsGetByIdResponse
            {
                Id = s.Id,
                UserId = s.UserId,
                IsReward = s.RewardId != null ? true : false,
                StatusId = s.RewardId != null ? s.RewardId : s.CouponsId,
                RewardId = s.RewardId,
                IsUsed = s.IsUsed,
                CouponExpiryDate = s.CouponsEntity != null ? s.CouponsEntity.ExpiryDate : null,
                CouponCode = s.CouponsEntity != null ? s.CouponsEntity.CouponCode : null,
                //CouponImage = s.CouponsEntity != null ? s.CouponsEntity.CouponImage : null,
                Name = s.RewardsEntity.Name != null ? s.RewardsEntity.Name : s.CouponsEntity.CouponTitle,
                Image = s.RewardsEntity != null ? s.RewardsEntity.Image : s.CouponsEntity != null ? s.CouponsEntity.CouponImage : null,
                Description = s.RewardsEntity != null ? s.RewardsEntity.Description : null,
                PointId = s.RewardsEntity != null ? s.RewardsEntity.PointId : null,
                Created = s.RewardsEntity != null ? s.RewardsEntity.Created : DateTime.Now,
                Status = s.RewardsEntity != null ? s.RewardsEntity.Active : false,
                //RewardsExpiryDate = s.RewardsEntity != null ? s.RewardsEntity.RewardsExpiryDate : null,
                Barcode = s.RewardsEntity != null ? s.RewardsEntity.Barcode : s.CouponsEntity!=null ? s.CouponsEntity.CouponCode : null,
            }).Where(s => s.IsUsed.Equals(false)).ToListAsync();

        if (rewards == null)
            throw new ApiException("error_rewards_not_found");

        return rewards;
    }

    #endregion 4. Rewards Add Into MyRewards Based on UserId

    #region 5. Get Rewards List

    public async Task<PaginationResponse> RewardsGetListAsync([FromBody] RewardsGetListRequest request)
    {
        //request.UserId = new Guid("DF6D0A98-DAD1-4C10-BABF-86EB3EDA0F2A");

        var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.RewardId);

        var rewardsList = await (
            from R in DbContext.Rewards

            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || R.Name.ToLower().Contains(request.Name.ToLower()))

                && R.Deleted.Equals(request.IsDeleted) //&& !rewards.Contains(R.Id)

            select new RewardsListResponse
            {
                Id = R.Id,
                Name = R.Name,
                Image = R.Image,
                Description = R.Description,
                Barcode = R.Barcode,
                UserId = R.UserId,
                PointId = R.PointId,
                Modified = R.Modified,
                Status = R.Active,
                Created = R.Created,
                //RewardsExpiryDate = R.RewardsExpiryDate,
                //IsUsed = DbContext.MyRewards.Any(a => a.RewardId.HasValue && a.RewardId.Equals(R.Id))
                IsUsed = rewards.Contains(R.Id),
                RewardsPoint = R.RewardsPoint
            }).OrderByDescending(x => x.Created).ToListAsync();

        if (rewardsList == null)
            throw new ApiException("error_rewards_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    rewardsList = rewardsList.OrderByDescending(x => x.Name).ToList();
                else
                    rewardsList = rewardsList.OrderBy(x => x.Name).ToList();
                break;

            default:

                rewardsList = rewardsList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (rewardsList != null && rewardsList.Any() && request.IsDisplayActive)
            rewardsList = rewardsList.Where(pc => pc.Status.Equals(true)).ToList();

        var total = rewardsList.Count();
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var rewardsPaginationList = rewardsList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = rewardsPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 5. Get Rewards List

    #region 6. Rewards Rewards List Without Pagination

    public async Task<List<RewardsListResponse>> RewardsList([FromBody] RewardsListRequest request)
    {
        //request.UserId = new Guid("DF6D0A98-DAD1-4C10-BABF-86EB3EDA0F2A");

        var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.RewardId);

        var rewardsList = await (
            from R in DbContext.Rewards

            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || R.Name.ToLower().Contains(request.Name.ToLower()))

                && R.Deleted.Equals(request.IsDeleted) //&& !rewards.Contains(R.Id)

            select new RewardsListResponse
            {
                Id = R.Id,
                Name = R.Name,
                Image = R.Image,
                Description = R.Description,
                Barcode = R.Barcode,
                UserId = R.UserId,
                PointId = R.PointId,
                Modified = R.Modified,
                Status = R.Active,
                Created = R.Created,
                //RewardsExpiryDate = R.RewardsExpiryDate,
                //IsUsed = DbContext.MyRewards.Any(a => a.RewardId.HasValue && a.RewardId.Equals(R.Id))
                IsUsed = rewards.Contains(R.Id),
                RewardsPoint = R.RewardsPoint
            }).OrderByDescending(x => x.Created).ToListAsync();

        if (rewardsList == null)
            throw new ApiException("error_rewards_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    rewardsList = rewardsList.OrderByDescending(x => x.Name).ToList();
                else
                    rewardsList = rewardsList.OrderBy(x => x.Name).ToList();
                break;

            default:

                rewardsList = rewardsList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (rewardsList != null && rewardsList.Any() && request.IsDisplayActive)
            rewardsList = rewardsList.Where(pc => pc.Status.Equals(true)).ToList();

        return rewardsList;
    }

    #endregion 6. Rewards Rewards List Without Pagination

    #region 7. Delete Rewards

    public async Task<BaseResponse> RewardsDelete([FromBody] BaseIdRequest request)
    {
        try
        {
            var reward = DbContext.Rewards.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (reward == null)
                throw new ApiException("error_rewards_not_found");

            if (reward.Active == true && reward.Deleted == false)
            {
                throw new ApiException("error_rewards_active");
            }
            else
            {
                reward.Deleted = true;
            }

            DbContext.SaveChanges();
            return new BaseResponse { IsSuccess = true };
        }
        catch (Exception ex)
        {

            return new BaseResponse { ErrorMessage = "Error" };
        }
    }

    #endregion 7. Delete Rewards

    #region 8. Rewards Status Update

    public async Task<BaseResponse> RewardsStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var rewardData = await DbContext.Rewards.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (rewardData == null)
            throw new ApiException("error_rewards_not_found");

        rewardData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 8. Rewards Status Update

    #region 9. Insert Rewards CSV

    public async Task<BaseResponse> RewardsCSVsInsert([FromBody] RewardsDataList request)
    {
        foreach (var rewards in request.Details)
        {
            if (!DbContext.Rewards.Any(x => x.Name.ToLower().Equals(rewards.Name.ToLower())))
            {
                await DbContext.AddRangeAsync(new RewardsEntity
                {
                    Name = rewards.Name,
                    Image = "Test",
                    Description = "Testing",
                    Barcode = rewards.Barcode,
                    RewardsPoint = rewards.RewardsPoint,
                    //RewardsExpiryDate = DateTime.UtcNow.AddDays(21),
                    Active = true
                });

                await DbContext.SaveChangesAsync();
            }
        }

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 9. Insert Rewards CSV

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}