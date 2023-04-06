namespace azara.admin.Helpers;

public class FAQsHelpers
{
    #region FAQs Insert-Update

    internal static async Task<dynamic> FAQsAddUpdateApi(FAQsRequestResponse request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.FAQs.InsertFAQs}" : $"{ApiEndPointConsts.FAQs.UpdateFAQs}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion FAQs Insert-Update

    #region FAQs Delete

    internal static async Task<dynamic> DeleteFAQsApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.FAQs.FAQsDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion FAQs Delete

    #region FAQs List API

    internal static async Task<dynamic> FAQsList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.FAQs.FAQsGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion FAQs List API

    #region FAQs Get By Id

    internal static async Task<dynamic> FAQsGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.FAQs.FAQsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion FAQs Get By Id

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}