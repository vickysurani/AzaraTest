namespace azara.client.Helpers;

public class RewardHelper
{
    #region Reward List API

    internal static async Task<dynamic> RewardListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.RewardGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Reward List API

    #region Reward Get By Id

    internal static async Task<dynamic> RewardbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.RewardGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Reward Get By Id

    #region Reward List API

    internal static async Task<dynamic> POSRewardListApi(PaginationRequest request)
    {
        var url = $"{ApiEndPointConsts.Reward.POSRewardGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);


        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Reward List API
}