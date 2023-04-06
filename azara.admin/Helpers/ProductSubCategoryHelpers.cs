using azara.admin.Models.Base.Request;

namespace azara.admin.Helpers;

public class ProductSubCategoryHelpers
{
    #region Product Sub Category Insert- Update API

    internal static async Task<dynamic> ProductSubCategoryInsertUpdateApi(ProductSubCategoryInsertUpdateRequest request)
    {
        var url = !string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.ProductSubCategory.UpdateProductSubCategory}" : $"{ApiEndPointConsts.ProductSubCategory.InsertProductSubCategory}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category Insert- Update API

    #region Product Sub Category List API

    internal static async Task<dynamic> ProductSubCategoryListApi(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductSubCategory.ProductSubCategoryGetList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category List API

    #region Delete Product Sub Category

    internal static async Task<dynamic> DeleteProductSubCategoryApi(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductSubCategory.ProductSubCategoryDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Delete Product Sub Category

    #region Product Sub Category Get By Id

    internal static async Task<dynamic> ProductSubCategoryGetById(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductSubCategory.ProductSubCategoryGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category Get By Id

    #region Product Sub Category Status Update

    internal static async Task<dynamic> ProductSubCategoryStatusUpdate(BaseStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.ProductSubCategory.ProductSubCategoryStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category Status Update

    #region Product Sub Category Get By ProductCategoryId

    internal static async Task<dynamic> GetProductSubCategoryListByProductCategoryId(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.ProductSubCategory.ProductSubCategoryListByProductCategoryId}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category Get By ProductCategoryId

    #region POS Subcategory Status Update

    internal static async Task<dynamic> POSSubcategoryStatusUpdate(BaseStringStatusUpdateRequest statusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.PosCategory.StatusUpdateDepartmentPos}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(statusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Event Status Update

    #region Product Sub Category Status Update

    internal static async Task<dynamic> ProductCategoryStatusUpdate(BaseIntStatusUpdateRequest subAdminStatusUpdateRequest)
    {
        var url = $"{ApiEndPointConsts.ProductCategory.POSSubCategoryStatusUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(subAdminStatusUpdateRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Sub Category Status Update
}