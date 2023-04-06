using azara.admin.Models.Coupons.Response;

namespace azara.admin.Helpers;

public class CouponsHelpers : IDisposable
{
    #region Coupon Insert-Update

    internal static async Task<dynamic> CouponAddUpdateApi(CouponsInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Coupons.InsertCoupons}" : $"{ApiEndPointConsts.Coupons.UpdateCoupons}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Insert-Update

    #region Coupon Get By Id

    internal static async Task<dynamic> CouponsGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Coupons.CouponsGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Get By Id

    #region Coupon List API

    internal static async Task<dynamic> CouponsListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Coupons.GetCouponsList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon List API

    #region Coupon Delete

    internal static async Task<dynamic> DeleteCouponsApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Coupons.DeleteCoupons}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Delete

    #region Coupon Status Update

    internal static async Task<dynamic> CouponsStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.Coupons.CouponsStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose

    internal static async Task<dynamic> CSVDataUpload(BaseFileUploadRequest request, string token)
    {
        var url = $"{ApiEndPointConsts.Coupons.InsertBulkCoupons}";
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

    #region Coupon Insert CSV

    internal static async Task<dynamic> CouponCSVAddApi(AdjustmentFileUploadResponseData request)
    {
        var url = $"{ApiEndPointConsts.Coupons.InsertCSVCoupons}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Coupon Insert CSV
}