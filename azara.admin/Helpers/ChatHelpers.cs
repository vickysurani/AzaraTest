namespace azara.admin.Helpers;

public class ChatHelpers
{
    #region Chat Get By Id

    internal static async Task<dynamic> ChatbyId(BaseRequiredIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Chat.ChatGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Chat Get By Id

    #region Chat Insert

    internal static async Task<dynamic> ChatInsertApi(ChatInsertRequest request)
    {
        var url = $"{ApiEndPointConsts.Chat.ChatInsert}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Chat Insert

    #region Chat List API

    internal static async Task<dynamic> ChatList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Chat.ChatList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Chat List API
}