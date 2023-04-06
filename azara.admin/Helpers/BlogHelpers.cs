namespace azara.admin.Helpers;

public class BlogHelpers : IDisposable
{
    #region Blog Insert-Update

    internal static async Task<dynamic> BlogAddUpdateApi(BlogInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Blog.InsertBlog}" : $"{ApiEndPointConsts.Blog.UpdateBlog}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog Insert-Update

    #region Blog Get By Id

    internal static async Task<dynamic> BlogGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Blog.BlogGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog Get By Id

    #region Blog List API

    internal static async Task<dynamic> BlogList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Blog.BlogGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog List API

    #region Blog Delete

    internal static async Task<dynamic> DeleteBlogApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Blog.BlogDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Blog Delete

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}