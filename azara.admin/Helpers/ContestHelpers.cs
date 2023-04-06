namespace azara.admin.Helpers;

public class ContestHelpers : IDisposable
{
    #region Contest Insert-Update

    internal static async Task<dynamic> ContestAddUpdateApi(ContestInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Contest.InsertContest}" : $"{ApiEndPointConsts.Contest.UpdateContest}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest Insert-Update

    #region Contest Get By Id

    internal static async Task<dynamic> ContestGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest Get By Id

    #region Contest List API

    internal static async Task<dynamic> ContestList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest List API

    #region Contest Delete

    internal static async Task<dynamic> DeleteContestApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest Delete

    #region Contest Status Update

    internal static async Task<dynamic> ContestStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}