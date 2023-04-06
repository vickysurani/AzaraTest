namespace azara.client.Helpers
{
    public class POSDepartmentListHelper
    {
        #region 1. Department List API
        internal static async Task<dynamic> DepartmentList(POSDepartmentListRequest request)
        {
            var url = $"{ApiEndPointConsts.POSDepartment.POSDepartmentList}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
           ? new CallAPIList() { meta = response.meta }
           : new CallAPI() { meta = response.meta, data = response.data };
        }
        #endregion 2. Department List API
    }
}
