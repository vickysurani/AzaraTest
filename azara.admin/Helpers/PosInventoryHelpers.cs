namespace azara.admin.Helpers
{
    public class PosInventoryHelpers
    {
        #region PosInventoryList API

        internal static async Task<dynamic> PosInventoryList(PosInventoryGetListRequest request)
        {
            var url = $"{ApiEndPointConsts.PosInventory.PosInventoryListNew}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }

        #endregion PosInventoryList API
    }
}
