namespace azara.api.Helpers;

public class PointsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public PointsHelpers(
        AzaraContext DbContext,
        ICrypto Crypto)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
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

    #region 1. Insert Points

    public async Task<BaseResponse> PointsInsert([FromBody] PointsInsertUpdateRequest request)
    {
        var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.UserId));

        await DbContext.AddRangeAsync(new PointsEntity
        {
            AvailablePoints = request.AvailablePoints,
            UsedPoints = request.UsedPoints,
            PointName = request.PointName,
            UserId = request.UserId ?? new Guid(),
        });

        user.Points = user.Points + request.AvailablePoints;

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Points

    #region 2. Update Points

    public async Task<PointUpdateResponse> PointsUpdate([FromBody] PointsInsertUpdateRequest request)
    {
        var point = DbContext.Point.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (point == null)
            throw new ApiException("error_points_not_found");

        point.AvailablePoints = request.AvailablePoints;
        point.UsedPoints = request.UsedPoints;
        point.PointName = request.PointName ?? point.PointName;
        point.UserId = request.UserId ?? point.UserId;

        var response = new PointUpdateResponse
        {
            AvailablePoints = point.AvailablePoints,
            UsedPoints = point.UsedPoints,
            PointName = point.PointName,
            UserId = point.UserId ?? new Guid()
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Points

    #region 3. Points Get By Id

    public async Task<PointsGetListResponse> PointsGetById([FromBody] BaseRequiredIdRequest request)
    {
        var pointData = await DbContext.Point.Where(x => x.UserId.Equals(request.Id) && !x.Deleted)
            .Select(s => new
            {
                Id = s.Id,
                UserName = s.UserEntity.FirstName,
                AvailablePoints = s.AvailablePoints,
                UsedPoints = s.UsedPoints
            }).ToListAsync();

        if (pointData == null || request.Id == new Guid())
            throw new ApiException("error_points_not_found");

        var pointDetails = await DbContext.Point.Where(w => w.UserId.Equals(request.Id)).Select(s => new PointHistory
        {
            IsSubTract = s.UsedPoints > 0,
            Point = s.AvailablePoints > 0 ? s.AvailablePoints : s.UsedPoints,
            PointName = s.PointName,
            PointRedeeemDate = s.Created
        }).OrderByDescending(o => o.PointRedeeemDate).ToListAsync();

        if (pointData == null)
            throw new ApiException("error_points_not_found");

        var response = new PointsGetListResponse
        {
            UserId = request.Id,
            //UserName = pointData.AsQueryable().Single().UserName,
            AvailablePointCount = pointData.Sum(s => s.AvailablePoints) - pointData.Sum(s => s.UsedPoints),
            PointHistory = pointDetails
        };

        return response;
    }

    #endregion 3. Points Get By Id

    #region 4. Get Points List

    public async Task<PaginationResponse> PointsGetList([FromBody] PointsGetListRequest request)
    {
        var pointList = await (from P in DbContext.Point
                               join U in DbContext.User on P.UserId equals U.Id

                               where (string.IsNullOrWhiteSpace(request.UserName.ToLower())
                               || U.FirstName.ToLower().Contains(request.UserName.ToLower()))

                               && P.Deleted.Equals(request.IsDeleted)

                               group P by U.Id into s

                               select new
                               {
                                   UserId = s.Key,
                                   UserName = s.FirstOrDefault().UserEntity.FirstName + " " + s.FirstOrDefault().UserEntity.LastName,
                                   AvailablePoints = s.FirstOrDefault().AvailablePoints,
                                   UsedPoints = s.FirstOrDefault().UsedPoints,
                                   Point = s.Sum(s => s.AvailablePoints) - s.Sum(s => s.UsedPoints),
                                   LastPointName = s.OrderByDescending(o => o.Created).FirstOrDefault().PointName,
                                   RedeemDate = s.OrderByDescending(o => o.Created).FirstOrDefault().Created,
                                   Modified = s.OrderByDescending(o => o.Modified).FirstOrDefault().Modified
                               }).OrderByDescending(x => x.Modified).ToListAsync();

        if (pointList == null)
            throw new ApiException("error_points_not_found");

        var total = pointList.Count();
        var totalPages = PointsHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = pointList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Points List

    #region 5. Points List Without Pagination

    public async Task<List<PointsListResponse>> PointsList([FromBody] PointsListRequest request)
    {
        var pointList = await DbContext.Point.Where(w => !w.Deleted).GroupBy(g => g.UserId)
                .Select(s => new PointsListResponse
                {
                    UserId = s.Key.Value,
                    UserName = s.FirstOrDefault().UserEntity.FirstName + " " + s.FirstOrDefault().UserEntity.LastName,
                    AvailablePoints = s.FirstOrDefault().AvailablePoints,
                    UsedPoints = s.FirstOrDefault().UsedPoints,
                    Point = s.Sum(s => s.AvailablePoints) - s.Sum(s => s.UsedPoints),
                    LastPointName = s.OrderByDescending(o => o.Created).FirstOrDefault().PointName,
                    RedeemDate = s.OrderByDescending(o => o.Created).FirstOrDefault().Created,
                    Modified = s.OrderByDescending(o => o.Modified).FirstOrDefault().Modified
                }).OrderByDescending(x => x.Modified).ToListAsync();

        if (pointList == null)
            throw new ApiException("error_points_not_found");

        return pointList;
    }

    #endregion 5. Points List Without Pagination

    #region 6. Delete Points

    public async Task<BaseResponse> PointsDelete([FromBody] BaseIdRequest request)
    {
        var point = DbContext.Point.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (point == null)
            throw new ApiException("error_points_not_found");

        if (point.Deleted == false)
        {
            point.Deleted = true;
        }

        point.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Points

    #region 7. Points assigned by admin for particualr userid

    public async Task<BaseResponse> PointsInsertByAdmin([FromBody] PointsAssignedByAdminRequest request)
    {
        await DbContext.AddRangeAsync(new PointsEntity
        {
            AvailablePoints = request.Points,
            UsedPoints = 0,
            PointName = "Added By Admin",
            UserId = request.UserId ?? new Guid(),
        });

        var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.UserId));

        user.Points = user.Points + request.Points;

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Points assigned by admin for particualr userid

    #region 8. Rewards and Coupon points added in table

    public async Task<BaseResponse> PointsAddedForRewardsOrCoupons([FromBody] PointsAddedForRewardsAndCouponsRequest request)
    {

        if (request.StatusCode == "1234")
        {

            if (request.Status == "Rewards")
            {
                var reward = DbContext.Rewards.FirstOrDefault(x => x.Id.Equals(request.StatusId));
                //await DbContext.AddRangeAsync(new PointsEntity
                //{
                //    AvailablePoints = 1,
                //    UsedPoints = 0,
                //    PointName = "Reward Point",
                //    UserId = request.UserId ?? new Guid(),
                //});

                await DbContext.Notification.AddAsync(new NotificationEntity
                {
                    Title = "Reward Redeem",
                    Description = $"Your {reward.Name} reward successfully redeem at Vape Club store.",
                    ReadableTime = DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                    UserId = request.UserId,
                    Modified = DateTime.UtcNow,
                    Icon = "images/coupon-redeem-icon.svg",
                });
            }
            if (request.Status == "Coupons")
            {
                var coupon = DbContext.Coupons.FirstOrDefault(x => x.Id.Equals(request.StatusId));

                //await DbContext.AddRangeAsync(new PointsEntity
                //{
                //    AvailablePoints = 1,
                //    UsedPoints = 0,
                //    PointName = "Coupon Point",
                //    UserId = request.UserId ?? new Guid(),
                //});

                await DbContext.Notification.AddAsync(new NotificationEntity
                {
                    Title = "Coupon Redeem",
                    Description = $"Your {coupon.CouponTitle} coupon successfully redeem at Vape Club store.",
                    ReadableTime = DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                    UserId = request.UserId,
                    Modified = DateTime.UtcNow,
                    Icon = "images/coupon-redeem-icon.svg",
                });
            }

            var userRewards = DbContext.MyReward.FirstOrDefault(x => x.RewardId.Equals(request.StatusId) || x.CouponsId.Equals(request.StatusId));
            userRewards.IsUsed = true;

            var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.UserId));

            user.Points = user.Points + request.Points;



            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };

        }
        else
        {
            return new BaseResponse { ErrorMessage = " Please Enter Valdi Code" };
        }


    }

    #endregion 8. Rewards and Coupon points added in table

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}