using azara.models.Requests.POSDepartment;
using azara.models.Responses.MyRewards;
using azara.models.Responses.POSDepartment;
using Azure;
using static azara.models.Constants.ActionsConsts;

namespace azara.api.Helpers;

public class MyRewardsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public MyRewardsHelpers(
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

    #region 1. Insert MyRewards

    public async Task<BaseResponse> MyRewardsInsert([FromBody] MyRewardsInsertUpdateRequest request)
    {
        try
        {
            var points = DbContext.Point.Where(x => x.UserId.Equals(request.UserId)).Sum(x => x.AvailablePoints - x.UsedPoints);
            var rewardPoints = DbContext.Rewards.FirstOrDefault(x => x.Id.Equals(request.RewardId));
            var couponPoints = DbContext.Coupons.FirstOrDefault(x => x.Id.Equals(request.CouponsId));
            var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.UserId));
            var notification = DbContext.Notification.FirstOrDefault(x => x.UserId.Equals(request.UserId));


            if (request.RewardId != null)
            {

                if ((rewardPoints != null && points >= rewardPoints.RewardsPoint) /*|| (couponPoints != null && points >= couponPoints.CouponsPoint)*/)
                {
                    string pointName;
                    int usedpoints;

                    if (rewardPoints != null)
                    {
                        usedpoints = rewardPoints.RewardsPoint;
                        pointName = "Redeem Reward";
                    }
                    else
                    {
                        usedpoints = couponPoints.CouponsPoint;
                        pointName = "Redeem Coupon";
                    }

                    if (DbContext.MyReward.Any(x => x.RewardId.Equals(request.RewardId) && x.UserId == request.UserId))
                        return await Task.FromResult(new BaseResponse { IsSuccess = true });

                    await DbContext.AddRangeAsync(new MyRewardEntity
                    {
                        UserId = request.UserId,
                        RewardId = request.RewardId,
                        CouponsId = request.CouponsId,
                        PunchCardId = request.PunchCardId,
                        IsUsed = false
                    });

                    await DbContext.Point.AddAsync(new PointsEntity
                    {
                        AvailablePoints = 0,
                        UsedPoints = usedpoints,
                        UserId = request.UserId,
                        PointName = pointName
                    });

                    user.Points = points - usedpoints;
                }
                else
                {
                    return new BaseResponse { IsSuccess = false, ErrorMessage = $"You don't have enough points to get this reward" };
                }
            }
            else if (request.CouponsId != null)
            {
                string pointName;
                int usedpoints;


                usedpoints = couponPoints.CouponsPoint;
                pointName = "Redeem Coupon";

                if (DbContext.MyReward.Any(x => x.CouponsId.Equals(request.CouponsId) && x.UserId == request.UserId))
                    return await Task.FromResult(new BaseResponse { IsSuccess = true });

                await DbContext.AddRangeAsync(new MyRewardEntity
                {
                    UserId = request.UserId,
                    RewardId = request.RewardId,
                    CouponsId = request.CouponsId,
                    PunchCardId = request.PunchCardId,
                    IsUsed = false
                });

                //user.Points = points - usedpoints;
            }
            else
            {
                var reward = request.RewardId != null ? "reward" : "coupon";

                return new BaseResponse { IsSuccess = false, ErrorMessage = $"You don't have enough points to get this {reward}" };
            }

            DbContext.SaveChanges();

            return new BaseResponse { IsSuccess = true };
        }
        catch (Exception e)
        {

            return new BaseResponse { ErrorMessage = e.Message };
        }
    }

    #endregion 1. Insert MyRewards

    #region 2. Reward Get By Id
    public async Task<MyRewardGetByIdResponse> MyRewardGetById([FromBody] MyRewardGetById request)
    {

        var myRewardData = await DbContext.MyReward.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);
        var rewardData = await DbContext.Rewards.FirstOrDefaultAsync(x => x.Id.Equals(myRewardData.RewardId));
        var couponsData = await DbContext.Coupons.FirstOrDefaultAsync(x => x.Id.Equals(myRewardData.CouponsId));
        var response = new MyRewardGetByIdResponse();

        if (myRewardData == null)
            throw new ApiException("error_rewards_not_found");

        if(rewardData != null)
        {
            response = new MyRewardGetByIdResponse
            {
                //UserId = s.UserId,
                Id = myRewardData.Id,
                StatusId =  rewardData.Id,
                RewardId = rewardData.Id,
                Name = rewardData.Name,
                Image = rewardData.Image,
                Description = rewardData.Description,
                Barcode = rewardData.Barcode,
                IsReward = true
            };
        }
        else
        {
            response = new MyRewardGetByIdResponse
            {
                //UserId = s.UserId,
                Id = myRewardData.Id,
                StatusId = couponsData.Id,
                CouponsId = couponsData.Id,
                Name =  couponsData.CouponTitle,
                Image = couponsData.CouponImage,
                Description = couponsData.Description,
                Barcode = couponsData.CouponCode,
                IsReward = false
            };
        }

        return response;
    }
    #endregion

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}