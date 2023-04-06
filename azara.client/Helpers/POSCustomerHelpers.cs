namespace azara.client.Helpers;

public class POSCustomerHelpers
{
    #region Customer POS Insert

    internal static async Task<dynamic> CustomerAddUpdateApi()
    {
        var url = $"{ApiEndPointConsts.POSCustomerData.POSCustomerInsert}";
        //var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Customer POS Insert

    #region Customer POS List API

    internal static async Task<dynamic> CustomerListApi()
    {
        var url = $"{ApiEndPointConsts.POSCustomerData.POSCustomerList}";
        //var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Customer POS List API

    #region Customer POS List API

    internal static async Task<dynamic> CompanyListAPI()
    {
        var url = $"{ApiEndPointConsts.POSCustomerData.POSCompanyList}";
        //var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Customer POS List API

    #region Customer POS List API

    internal static async Task<dynamic> InventoryAPI()
    {
        var url = $"{ApiEndPointConsts.POSCustomerData.POSInventoryList}";
        //var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Customer POS List API
}