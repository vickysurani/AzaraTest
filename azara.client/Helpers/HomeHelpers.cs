namespace azara.client.Helpers;

public class HomeHelpers
{
    #region Get Admin Message Count

    internal static async Task<dynamic> GetAdminMessCount()
    {
        var url = $"{ApiEndPointConsts.Chat.AdminMessageCount}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get Admin Message Count
}