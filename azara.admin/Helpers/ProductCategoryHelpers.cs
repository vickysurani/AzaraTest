using azara.admin.Models.ProductSubCategory.Request;

namespace azara.admin.Helpers;

public class ProductCategoryHelpers : IDisposable
{
    #region Product Category Insert API

    internal static async Task<dynamic> ProductCategoryInsertUpdateApi(ProductCategoryInsertUpdateRequest request)
    {
        var url = !string.IsNullOrWhiteSpace(request.ProductCategoryId) ? $"{ApiEndPointConsts.ProductCategory.UpdateProductCategory}" : $"{ApiEndPointConsts.ProductCategory.InsertProductCategory}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }



    #endregion Product Category Insert 
    #region Product Category Insert API

    internal static async Task<dynamic> ProductSubCategoryInsertUpdateApi(PosSubCetegoryInsertUpdateRequest request)
    {
        var url =  $"{ApiEndPointConsts.ProductCategory.UpdateProductCategory}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Category Insert API

    #region Product Category List API

    internal static async Task<dynamic> ProductCategoryListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.PosSubCategoryGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Category List API

    #region Delete Product Category

    internal static async Task<dynamic> DeleteProductCategoryApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.ProductCategoryDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Delete Product Category

    #region Product subCategory Get By Id

    internal static async Task<dynamic> ProductSubCategoryGetById(PosByIntIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.PosSubProductCategoryGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Category Get By Id

    #region Product Category Get By Id

    internal static async Task<dynamic> ProductCategoryGetById(PosByIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.PosSubProductCategoryGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Category Get By Id

    #region Product Category Status Update

    internal static async Task<dynamic> ProductCategoryStatusUpdate(BaseIntStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.ProductCategoryStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Category Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}