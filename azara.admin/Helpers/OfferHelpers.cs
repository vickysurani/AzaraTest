namespace azara.admin.Helpers;

public class OfferHelpers
{
    #region Offers List API

    internal static async Task<dynamic> OffersList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Offers.GetOffersList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Offers List API

    #region Offers Insert-Update

    internal static async Task<dynamic> OffersAddUpdateApi(OffersInsertRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Offers.InsertOffers}" : $"{ApiEndPointConsts.Offers.UpdateOffers}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Offers Insert-Update

    #region Offers Delete

    internal static async Task<dynamic> DeleteOffersApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Offers.DeleteOffers}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Offers Delete

    #region Offers Get By Id

    internal static async Task<dynamic> OffersGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Offers.OffersGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Offers Get By Id

    #region Offers Status Update

    internal static async Task<dynamic> OffersStatusUpdate(BaseStatusUpdateRequest request)
    {
        var url = $"{ApiEndPointConsts.Offers.OffersStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Offers Status Update
}