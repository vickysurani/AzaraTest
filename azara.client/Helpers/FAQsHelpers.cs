namespace azara.client.Helpers;

public class FAQsHelpers
{
    #region FAQs List API

    internal static async Task<dynamic> FAQsList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.FAQs.FAQsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion FAQs List API
}