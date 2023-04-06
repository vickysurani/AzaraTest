using azara.admin.Models.Banner.Request;
using azara.models.Requests.Banner;

namespace azara.admin.Helpers
{
    public class BannerImageHelpers
    {
        #region Banner Insert-Update

        internal static async Task<dynamic> BannerAddUpdateApi(BannerImageRequest request)
        {
            var url = $"{ApiEndPointConsts.Event.InsertEvent}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        internal static async Task<dynamic> BannerUpdateImage(BannerInsertRequest request)
        {
            var url = $"{ApiEndPointConsts.BannerImage.BannerUpdateImage}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Banner Insert-Update

        #region Get List API

        internal static async Task<dynamic> BannerList(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.BannerImage.BannerImageList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Get List API
    }
}
