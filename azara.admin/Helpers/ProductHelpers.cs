using azara.admin.Models.Base.Request;

namespace azara.admin.Helpers;

public class ProductHelpers : IDisposable
{
    #region Product Insert API

    internal static async Task<dynamic> ProductInsertUpdateApi(ProductInsertUpdateRequest request)
    {
        var url = !string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Product.UpdateProduct}" : $"{ApiEndPointConsts.Product.InsertProduct}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Insert API

    #region Product List API

    internal static async Task<dynamic> ProductList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product List API

    #region Delete Product

    internal static async Task<dynamic> DeleteProduct(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Delete Product

    #region Get By Id Product

    internal static async Task<dynamic> GetProductItem(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id Product

    #region Upload Product Image

    internal static async Task<dynamic> UploadProductImage(BaseImageUploadRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.UploadProductImage}";
        var response = await Service.PostUploadFile(url, request.FileName, request.File, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Upload Product Image

    #region Get Store & Producy Category

    internal static async Task<dynamic> GetStoreProductCategory(LocationDetailRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.GetStoreProductCategoryList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get Store & Producy Category

    #region Get Reward dropdown

    internal static async Task<dynamic> GetRewardDropdown()
    {
        var url = $"{ApiEndPointConsts.Product.GetRewardata}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get Reward dropdown

    #region Get By Id Product

    internal static async Task<dynamic> GetPosInventoryItem(PosByIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.PosINventoryGetByID}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id Product

    #region Update Inventory Image

    internal static async Task<dynamic> UpdateInventoryImage(PosInventoryImageUpdateRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.PosInventoryImageUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Update Inventory Image

    #region Inventory Status Update

    internal static async Task<dynamic> InventoryStatusUpdate(BaseStringStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.PosInventory.InventoryStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Inventory Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}