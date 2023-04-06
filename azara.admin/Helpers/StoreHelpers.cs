namespace azara.admin.Helpers;

public class StoreHelpers : IDisposable
{
    #region Store Insert-Update

    internal static async Task<dynamic> StoreAddUpdateApi(StoreInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Store.InsertStore}" : $"{ApiEndPointConsts.Store.UpdateStore}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store Insert-Update

    #region Store Delete

    internal static async Task<dynamic> DeleteStoreApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store Delete

    #region Store List API

    internal static async Task<dynamic> StoreList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store List API

    #region Store Get By Id

    internal static async Task<dynamic> StoreGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store Get By Id

    #region Store Status Update

    internal static async Task<dynamic> StoreStatusUpdate(BaseStatusUpdateRequest request)
    {
        var url = $"{ApiEndPointConsts.Store.StoreStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}