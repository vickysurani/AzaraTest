using azara.admin.Models.Rewards.Response;

namespace azara.admin.Helpers;

public class RewardHelpers
{
    #region Rewards List API

    internal static async Task<dynamic> RewardsList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.GetRewardsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Rewards List API

    #region Rewards Insert-Update

    internal static async Task<dynamic> RewardAddUpdateApi(RewardInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Reward.InsertRewards}" : $"{ApiEndPointConsts.Reward.UpdateRewards}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Rewards Insert-Update

    #region Rewards Delete

    internal static async Task<dynamic> DeleteRewardsApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.DeleteRewards}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Rewards Delete

    #region Rewards Get By Id

    internal static async Task<dynamic> RewardsGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.RewardsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Rewards Get By Id

    #region Rewards Status Update
    internal static async Task<dynamic> RewardsStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.Reward.RewardsStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Rewards Status Update

    internal static async Task<dynamic> RewardCSVDataUpload(BaseFileUploadRequest request, string token)
    {
        var url = $"{ApiEndPointConsts.Reward.InsertBulkReward}";
        var response = await Service.PostUploadCSVFile(url, request.FileName, null, request.File, token);
        if (response.meta.statusCode != StatusCodeConsts.Success)
        {
            return new CallAPIList()
            {
                meta = response.meta,
            };
        }
        return new CallAPI()
        {
            meta = response.meta,
            data = response.data
        };
    }

    #region Coupon Insert CSV

    internal static async Task<dynamic> RewardCSVAddApi(CSVdata request)
    {
        var url = $"{ApiEndPointConsts.Reward.InsertCSVReward}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Insert CSV
}