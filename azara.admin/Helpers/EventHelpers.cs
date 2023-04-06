namespace azara.admin.Helpers;

public class EventHelpers : IDisposable
{
    #region Event Insert-Update

    internal static async Task<dynamic> EventAddUpdateApi(EventInsertUpdateRequest request)
    {
        var url = request.Id == null ? $"{ApiEndPointConsts.Event.InsertEvent}" : $"{ApiEndPointConsts.Event.UpdateEvent}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event Insert-Update

    #region Get By Id Event

    internal static async Task<dynamic> GetEventItem(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.GetEventById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id Event

    #region Get List API

    internal static async Task<dynamic> EventList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.GetEvents}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get List API

    #region Event Status Update

    internal static async Task<dynamic> EventStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.Event.DeleteStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event Status Update

    #region Store Delete

    internal static async Task<dynamic> DeleteStoreApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.DeleteEvent}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Store Delete

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}