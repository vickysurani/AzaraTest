namespace azara.admin.Helpers;

public class UserHelpers : IDisposable
{
    #region User Add-Update API

    internal static async Task<dynamic> UserAddUpdateApi(UserInsertUpdateRequest request)
    {
        var url = !string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.User.UserUpdate}" : $"{ApiEndPointConsts.User.UserAdd}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Add-Update API

    #region User List API

    internal static async Task<dynamic> UserList(UserGetListRequest request)
    {
        var url = $"{ApiEndPointConsts.User.UserGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User List API

    #region User List API

    internal static async Task<dynamic> PosUserList(PaginationRequest request)
    {
        var url = $"{ApiEndPointConsts.User.POSUserGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User List API

    #region Get By Id User

    internal static async Task<dynamic> PosUserGetById(PosByIdRequest request)
    {
        var url = $"{ApiEndPointConsts.User.PosUserGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id User

    #region User Delete

    internal static async Task<dynamic> DeleteUserApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.User.UserDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Delete

    #region User Get By Id

    internal static async Task<dynamic> UserGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.User.UserGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Get By Id

    #region User Status Update

    internal static async Task<dynamic> UserStatusUpdate(BaseStatusUpdateRequest request)
    {
        var url = $"{ApiEndPointConsts.User.UserStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}