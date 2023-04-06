namespace azara.admin.Helpers;

public class ContactRequestHelpers
{
    #region ContactRequest List API

    internal static async Task<dynamic> ContactRequestList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.ContactRequest.ContactRequestGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ContactRequest List API

    #region Delete Contact Request

    internal static async Task<dynamic> DeleteContactRequest(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ContactRequest.ContactRequestDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Delete Contact Request

    #region Get By Id ContactRequest

    internal static async Task<dynamic> GetContactRequest(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ContactRequest.ContactRequestGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id ContactRequest
}