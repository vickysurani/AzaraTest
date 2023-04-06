namespace azara.client.Helpers;

public class PointsHelpers
{
    #region Points Get By Id

    internal static async Task<dynamic> PointsbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Points.PointsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Points Get By Id
}