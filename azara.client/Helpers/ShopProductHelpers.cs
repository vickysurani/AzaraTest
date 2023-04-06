using azara.client.Models.Inventory.Request;
using azara.client.Models.POSSubCategory;
using azara.client.Models.ShopProduct.Request;

namespace azara.client.Helpers;

public class ShopProductHelpers
{
    #region Product List API

    internal static async Task<dynamic> ShopProductList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ShopProductList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product List API

    #region Product whithout List API

    internal static async Task<dynamic> ShopProductListWhithoutToken(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ShopProductList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product whithout List API

    #region ProductCategory List API

    internal static async Task<dynamic> ProductCategoryList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductCategoryList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ProductCategory List API

    #region product By Id WithToken API

    internal static async Task<dynamic> ProductGetByIdApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion product By Id WithToken API

    #region Product  Get By Subcategory 

    internal static async Task<dynamic> ProductSubCategorybyID(POSInventoryDistinctSubCategoryList request)
    {
        var url = $"{ApiEndPointConsts.PosCategory.ProductSubcategory}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    internal static async Task<dynamic> ProductSubCategory(POSCategoryByIdRequest request)
    {
        var url = $"{ApiEndPointConsts.PosCategory.ProductSubcategory}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    internal static async Task<dynamic> ProductBySubCategory(POSInventoryListRequest request)
    {
        var url = $"{ApiEndPointConsts.PosInventory.PosInventoryListNew}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ProductSubCategory Get By Id

    #region ProductSubCategory Get By Id

    internal static async Task<dynamic> ProductListByProductSubCategory(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Product.ProductListBySubCategoryId}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ProductSubCategory Get By Id

    #region PosProduct List API

    internal static async Task<dynamic> PosInventoryList(POSInventoryListRequest request)
    {
        var url = $"{ApiEndPointConsts.PosInventory.PosInventoryListNew}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    internal static async Task<dynamic> GetPosInventoryList()
    {
        var url = $"{ApiEndPointConsts.PosInventory.GetInventoryPosList}";
        //var stringContent = new StringContent(JsonConvert.SerializeObject(), Encoding.UTF8, "application/json");
        var response = await Service.GetAPIWithoutToken(url);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Pos Product List API

    #region Get By Id PosProduct

    internal static async Task<dynamic> GetPosInventoryItem(PosByIdRequest request)
    {
        var url = $"{ApiEndPointConsts.PosInventory.PosINventoryGetByID}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get By Id PosProduct

    #region PosInventoryList API

    internal static async Task<dynamic> PosInventoryList(ListRequest request)
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