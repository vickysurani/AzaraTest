namespace azara.client.Helpers;

public class DealsHelpers
{
    #region Deals List WithToken API

    internal static async Task<dynamic> DealsListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Deals.DealsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Deals List WithToken API

    #region Deals Get By Id

    internal static async Task<dynamic> DealsbyID(AdvertisementIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Deals.DealsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Deals Get By Id
}