namespace azara.client.Helpers;

public class BlogHelpers
{
    #region Blog List API

    internal static async Task<dynamic> BlogListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Blog.BlogList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog List API

    #region Blog Get By Id

    internal static async Task<dynamic> BlogbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Blog.BlogGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog Get By Id
}