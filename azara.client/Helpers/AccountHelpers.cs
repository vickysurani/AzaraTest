namespace azara.client.Helpers;

public class AccountHelpers
{
    #region Login API

    internal static async Task<dynamic> LoginAPI(LoginRequest loginRequest)
    {
        var ApiRequest = new LoginRequest
        {
            EmailId = loginRequest.EmailId,
            Password = loginRequest.Password
        };

        var url = $"{ApiEndPointConsts.Account.UserLogin}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.Default, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Login API

    #region SignUp API

    internal static async Task<dynamic> SignUPApi(SignUpRequest signUpRequest)
    {
        var url = $"{ApiEndPointConsts.Account.UserSignUp}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(signUpRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion SignUp API

    #region ChaangePassword API

    internal static async Task<dynamic> ChaangePasswordPApi(ChangePasswordRequest chnagePassword)
    {
        var url = $"{ApiEndPointConsts.Account.ChangePassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(chnagePassword), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ChaangePassword API

    #region ForgotPassword API

    internal static async Task<dynamic> ForgotPasswordApi(ForgotPasswordRequest forgotPasswordRequest)
    {
        var url = $"{ApiEndPointConsts.Account.ForgotPassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(forgotPasswordRequest), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ForgotPassword API

    #region ContactUs API

    internal static async Task<dynamic> ContactUs(ContactUsRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.ContactUs}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion ContactUs API

    #region LogOut API

    internal static async Task<dynamic> LogoutApi()
    {
        var url = $"{ApiEndPointConsts.Account.Logout}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion LogOut API

    #region Get User Profile Data API

    internal static async Task<dynamic> GetProfilApi()
    {
        var url = $"{ApiEndPointConsts.Account.GetProfile}";
        var response = await Services.Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get User Profile Data API

    #region Get referal Code of user API

    internal static async Task<dynamic> GetReferalDataApi(StringIdERequest request)
    {
        var url = $"{ApiEndPointConsts.Account.GetReferalData}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Get referal Code of user API

    #region Facebook Login

    internal static async Task FaceBookLogin()
    {
        var url = $"{ApiEndPointConsts.FaceBook.FacebookLogin}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);
    }

    #endregion Facebook Login

    #region Reset Password API

    internal static async Task<dynamic> ResetPasswordApi(ResetPasswordRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.ResetPassword}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Reset Password API

    #region User Update

    internal static async Task<dynamic> Profileupdate(SignUpRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.ProfileUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Update

    #region Product Request API

    internal static async Task<dynamic> ProductRequestApi(ProductRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.UesrProductRequest}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Product Request API

    #region User Email Verify

    internal static async Task<dynamic> IsUserValid(CheckUserValidRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.IsUserValid}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion User Email Verify

    #region Profile Image Upload

    //internal static async Task<dynamic> ProfileImageUpload(ImageProfileUpdateRequest request)
    //{
    //    var url = $"{ApiEndPointConsts.Account.ImageUpload}";
    //    var response = await Services.Service.PostUploadFile(url, request.File, TokenResponse.Token);

    //    return response.meta.statusCode != StatusCodeConsts.Success
    //        ? new CallAPIList() { meta = response.meta }
    //        : new CallAPI() { meta = response.meta, data = response.data };
    //}

    #endregion Profile Image Upload
}