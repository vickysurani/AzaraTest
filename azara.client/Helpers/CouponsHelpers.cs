namespace azara.client.Helpers;

public class CouponsHelpers
{
    #region Coupons List API

    internal static async Task<dynamic> CouponsListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Coupons.CouponsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog List API

    #region Coupons Get By Id

    internal static async Task<dynamic> CouponsbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Coupons.CouponsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupons Get By Id

    #region Punchcard List API

    internal static async Task<dynamic> PunchcardListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Punchcard.PunchcardGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog List API

    #region Coupons Get By Id

    internal static async Task<dynamic> PunchcardbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Punchcard.PunchcardGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupons Get By Id


}