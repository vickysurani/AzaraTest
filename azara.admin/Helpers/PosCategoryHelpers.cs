using azara.admin.Models.Poscategory.Request;

namespace azara.admin.Helpers
{
    public class PosCategoryHelpers : IDisposable
    {

        #region Pos Category List API

        internal static async Task<dynamic> PosCategoryListApi(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.PosCategory.GetListDepartmentPos}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Pos Category List API


        #region Pos Category Get By Id

        internal static async Task<dynamic> PosCategoryGetById(PosCategoryByIdRequest request)
        {
            var url = $"{ApiEndPointConsts.PosCategory.GetByIdDepartmentPos}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Pos Category Get By Id

        #region Update Pos Category Image

        internal static async Task<dynamic> UpdatePosCategoryImage(PosInventoryImageUpdateRequest request)
        {
            var url = $"{ApiEndPointConsts.PosCategory.UpdateDepartmentPos}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Update Pos Category Image


        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
