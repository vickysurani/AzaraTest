using azara.admin.Models.AboutUs;

namespace azara.admin.Helpers
{
    public class AboutUsHelpers
    {
        #region AboutUs List API

        internal static async Task<dynamic> AboutUsList(AboutUsGetRequest request)
        {
            var url = $"{ApiEndPointConsts.AboutUs.AboutUsList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Ablout Us List API

        #region AboutUs Insert Update API

        internal static async Task<dynamic> AboutUsInsertUpdate(AboutUsGetByIDResponse request)
        {
            var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.AboutUs.AboutUsInsert}" : $"{ApiEndPointConsts.AboutUs.AboutUsUpdate}"; 
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent,TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion About us update API

        #region About Us Get By Id

        internal static async Task<dynamic> AboutUsGetById(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.AboutUs.AboutUsGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion About us Get By Id

        #region About us Delete

        internal static async Task<dynamic> DeleteAboutUsApi(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.AboutUs.AboutUsDelete}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion About Us Delete

    }
}
