namespace azara.admin.Helpers;

public class DropdownHelpers
{
    #region Get Producy Sub Category

    internal static async Task<dynamic> GetProductCategory()
    {
        var url = $"{ApiEndPointConsts.Dropdown.ProductCategoryDropdown}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get Producy Sub Category
}