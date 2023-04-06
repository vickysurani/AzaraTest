namespace azara.client.Helpers;

public class ContestHelpers
{
    #region Contest List API

    internal static async Task<dynamic> ContestListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest List API

    #region Contest Get By Id

    internal static async Task<dynamic> ContestbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Contest Get By Id

    #region Contest Insert for User
    internal static async Task<dynamic> ContestAddForUser(ContestAddForUser request)
    {
        var url = $"{ApiEndPointConsts.Contest.InsertUserContest}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    #endregion

    #region Contest Get for User

    internal static async Task<dynamic> ContestGetForUser(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Contest.ContestGetForUser}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Events Get for User
}