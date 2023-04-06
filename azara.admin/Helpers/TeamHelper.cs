using azara.admin.Models.TeamManagement.Request;

namespace azara.admin.Helpers
{
    public class TeamHelper
    {
        #region Team Default Permissions List
        internal static async Task<dynamic> GetTeamsDefaultPermission(string token)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.DefaultPermissionList}";
            var response = await Service.GetAPIWithToken(url, token);
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

        #endregion

        #region Team Update
        internal static async Task<dynamic> TeamUpdate(TeamInsertRequest request, string token)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.TeamUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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

        #endregion

        #region New Team Insert
        internal static async Task<dynamic> TeamAdd(TeamInsertRequest request, string token)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.TeamInsert}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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

        #endregion New Team Insert

        #region Team List API

        internal static async Task<dynamic> Teamlist(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.TeamList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Team List API

        #region Team Get By Id

        internal static async Task<dynamic> TeamGetById(BaseGetByIdRequest request)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.TeamGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Team Get By Id 

        #region Team Get By Id

        internal static async Task<dynamic> TeamGetByBaseId(TeamDetailsGetByIdRequest request)
        {
            var url = $"{ApiEndPointConsts.TeamManagement.TeamGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Team Get By Id
    }
}
