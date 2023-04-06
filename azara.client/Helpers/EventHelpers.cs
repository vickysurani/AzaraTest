using azara.client.Models.Event.Request;

namespace azara.client.Helpers;

public class EventHelpers
{
    #region Event List API

    internal static async Task<dynamic> EventListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.EventList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event List API

    #region Event Get By Id

    internal static async Task<dynamic> EventbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.EventGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event Get By Id

    #region Event Insert for User
    internal static async Task<dynamic> EventAddForUser(EventAddForUser request)
    {
        var url = $"{ApiEndPointConsts.Event.EventAddForUSer}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event Insert for User

    #region Events Get for User

    internal static async Task<dynamic> EventsGetForUser(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Event.EventsGetForUsre}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Events Get for User
}