using azara.admin.Models.Rewards.Response;

namespace azara.admin.Helpers
{
    public class PunchcardHelper
    {
        #region Punchcard List API

        internal static async Task<dynamic> PunchcardList(ListRequest request)
        {
            var url = $"{ApiEndPointConsts.Punchcard.GetPunchcardList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard List API

        #region Punchcard Insert-Update

        internal static async Task<dynamic> PunchcardAddUpdateApi(PunchCardInsertUpdateRequest request)
        {
            var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Punchcard.InsertPunchcard}" : $"{ApiEndPointConsts.Punchcard.UpdatePunchcard}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard Insert-Update

        #region Punchcard Delete

        internal static async Task<dynamic> DeletePunchcardApi(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.Punchcard.DeletePunchcard}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard Delete

        #region Punchcard Get By Id

        internal static async Task<dynamic> PunchcardGetById(BaseIdRequest request)
        {
            var url = $"{ApiEndPointConsts.Punchcard.PunchcardGetById}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard Get By Id

        #region Punchcard Status Update
        internal static async Task<dynamic> PunchcardStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
        {
            var url = $"{ApiEndPointConsts.Punchcard.PunchcardStatusUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard Status Update

        internal static async Task<dynamic> PunchcardCSVDataUpload(BaseFileUploadRequest request, string token)
        {
            var url = $"{ApiEndPointConsts.Punchcard.InsertBulkPunchcard}";
            var response = await Service.PostUploadCSVFile(url, request.FileName, null, request.File, token);
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

        #region Punchcard Insert CSV

        internal static async Task<dynamic> PunchcardCSVAddApi(PunchcardCSVdata request)
        {
            var url = $"{ApiEndPointConsts.Punchcard.InsertCSVPunchcard}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion Punchcard Insert CSV
    }
}
