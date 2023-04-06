namespace azara.admin.Helpers;

public class AdvertisementHelpers : IDisposable
{
    #region Advertisement Insert-Update

    internal static async Task<dynamic> AdvertisementAddUpdateApi(DealInsertUpdateRequest request)
    {
        var url = request.Id == null ? $"{ApiEndPointConsts.Advertisement.InsertAdvertisement}" : $"{ApiEndPointConsts.Advertisement.UpdateAdvertisement}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Advertisement Insert-Update

    #region Advertisement List API

    internal static async Task<dynamic> AdvertisementList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Advertisement.GetAdvertisementList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Advertisement List API

    #region Delete Advertisement

    internal static async Task<dynamic> DeleteAdvertisement(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Advertisement.DeleteAdvertisement}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Delete Advertisement

    #region Get By Id Advertisement

    internal static async Task<dynamic> GetAdvertisementItem(DealIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Advertisement.AdvertisementGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id Advertisement

    #region Advertisement Status Update

    internal static async Task<dynamic> AdvertisementStatusUpdate(BaseStatusUpdateRequest statusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.Advertisement.AdvertisementStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(statusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Advertisement Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}