namespace azara.admin.Helpers;

public class PointsHelpers : IDisposable
{
    #region Points Insert Update API

    internal static async Task<dynamic> PointsInsertUpdateApi(PointsInsertUpdateRequest request)
    {
        var url = !string.IsNullOrWhiteSpace(request.PointId) ? $"{ApiEndPointConsts.Points.UpdatePoints}" : $"{ApiEndPointConsts.Points.InsertPoints}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Points Insert Update API

    #region Points List API

    internal static async Task<dynamic> PointsList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Points.PointsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Points List API

    #region Points Get By Id

    internal static async Task<dynamic> PointsGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Points.PointsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Points Get By Id


    #region point Assign API

    internal static async Task<dynamic> PointAssignUsers(string userid, string request)
    {
        var ApiRequest = new PointAssignRequest
        {
            UserId = Guid.Parse(userid),
            Points = request,
            //RememberMe = loginRequest.RememberMe,
        };

        var url = $"{ApiEndPointConsts.Points.PointAssignedByAdmin}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion point Assign API
    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}