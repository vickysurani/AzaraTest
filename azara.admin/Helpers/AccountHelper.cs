namespace azara.admin.Helpers;

public class AccountHelper
{
    #region Login

    internal static async Task<dynamic> LoginAPI(LoginRequest loginRequest)
    {
        var ApiRequest = new LoginRequest
        {
            EmailId = loginRequest.EmailId,
            Password = loginRequest.Password
        };

        var url = $"{ApiEndPointConsts.Admin.AdminLogin}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.Default, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Login

    #region ForgotPassword API

    internal static async Task<dynamic> ForgotPasswordApi(ForgotPasswordRequest forgotPasswordRequest)
    {
        var url = $"{ApiEndPointConsts.Admin.ForgotPassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(forgotPasswordRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ForgotPassword API

    #region Logout

    internal static async Task<dynamic> LogoutApi()
    {
        var url = $"{ApiEndPointConsts.Admin.Logout}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        //return response.meta.statusCode != StatusCodeConsts.Success
        //    ? new CallAPIList() { meta = response.meta }
        //    : new CallAPI() { meta = response.meta, data = response.data };
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

    #endregion Logout

    #region Reset Password

    internal static async Task<dynamic> ResetPasswordApi(AdminResetPasswordRequest resetPasswordRequest)
    {
        var url = $"{ApiEndPointConsts.Admin.ResetPassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Reset Password

    #region Admin IsValid For Reset Password

    internal static async Task<dynamic> IsAdminValidApi(CheckAdminResetRequest resetPasswordRequest)
    {
        var url = $"{ApiEndPointConsts.Admin.IsAdminValid}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Admin IsValid For Reset Password

    #region Get Admin Profile Data API

    internal static async Task<dynamic> GetProfilApi()
    {
        var url = $"{ApiEndPointConsts.Admin.Admindetails}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get Admin Profile Data API

    #region Admin Update

    internal static async Task<dynamic> AdminUpdate(AdminEditRequestResponse request)
    {
        var url = $"{ApiEndPointConsts.Admin.AdminUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Admin Update

    #region ChaangePassword API

    internal static async Task<dynamic> ChaangePasswordPApi(ChangePasswordRequest chnagePassword)
    {
        var url = $"{ApiEndPointConsts.Admin.ChangePassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(chnagePassword), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ChaangePassword API

    #region Menu List API

    internal static async Task<dynamic> MenuList()
    {
        var url = $"{ApiEndPointConsts.Admin.MenuList}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Menu List API
}