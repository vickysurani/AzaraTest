using azara.admin.Models.Sub_admin.Request;

namespace azara.admin.Helpers
{
    public class SubadminHelper
    {
        #region Team List API

        internal static async Task<dynamic> SubadminList(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.Subadmin.SubadminList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Team List API

        #region Get TEam dropdown

        internal static async Task<dynamic> GetTeamDropdown()
        {
            var url = $"{ApiEndPointConsts.Subadmin.TeamDromdown}";
            var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Get Reward dropdown

        #region Sub Admin Update
        internal static async Task<dynamic> SubAdminAddUpdate(SubAdminPersistRequest subAdminPersistRequest, string token)
        {
            var url = subAdminPersistRequest.Id == null ? $"{ApiEndPointConsts.Subadmin.SubadminInsert}" : $"{ApiEndPointConsts.Subadmin.SubadminUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminPersistRequest), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Edit Sub Admin API

        #region Delete Subadmin 

        internal static async Task<dynamic> DeleteSubadmin(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.Subadmin.SubadminDelete}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Delete Subadmin

        #region Subadmin Status Update

        internal static async Task<dynamic> SubadminStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
        {
            var url = $"{ApiEndPointConsts.Subadmin.SubadminStatusUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Subadmin Status Update

        #region Get By Id Subadmin

        internal static async Task<dynamic> GetSubadminByID(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.Subadmin.SubadminGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }
        #endregion Get By Id Subadmin

    }
}
