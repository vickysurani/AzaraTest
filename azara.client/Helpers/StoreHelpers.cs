namespace azara.client.Helpers;

public class StoreHelpers
{
    #region Store List WithToken API

    internal static async Task<dynamic> StoreListApi()
    {
        var url = $"{ApiEndPointConsts.Store.StoreList_Get}";
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store List WithToken API

    #region Store List WithToken API

    internal static async Task<dynamic> StoreListWithoutTokenApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreList_Get}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store List WithToken API

    #region Store By Id WithToken API

    internal static async Task<dynamic> StoreGetByIdApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store By Id WithToken API
}