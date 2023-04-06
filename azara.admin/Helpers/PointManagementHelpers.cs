using azara.admin.Models.PointManagement.Request;

namespace azara.admin.Helpers
{
    public class PointManagementHelpers : IDisposable
    {
        #region PointManagement Insert-Updates

        internal static async Task<dynamic> PointManagementAddUpdateApi(PointManagementInsertRequest request)
        {
            var url = request.Id == null ? $"{ApiEndPointConsts.PointManagement.PointManagementInsert}" : $"{ApiEndPointConsts.PointManagement.PointManagementUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion PointManagement Insert-Update

        #region Get By Id PointManagement

        internal static async Task<dynamic> GetPointManagementItem(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.PointManagement.PointManagementGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Get By Id PointManagement

        #region Get List API

        internal static async Task<dynamic> PointManagementList(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.PointManagement.PointManagementList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Get List API

        #region PointManagement Status Update

        internal static async Task<dynamic> PointManagementStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
        {
            var url = $"{ApiEndPointConsts.PointManagement.PointManagementStatusUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion PointManagement Status Update

        #region Store Delete

        internal static async Task<dynamic> DeletePointManagementApi(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.PointManagement.PointManagementDelete}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Store Delete

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
