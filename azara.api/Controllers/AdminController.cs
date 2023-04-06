using azara.models.Responses.Menu;

namespace azara.api.Controllers;

public class AdminController : BaseController
{
    #region Object Declaration And Constructor

    public AdminController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService CSVService) : base(
            Localizer,
            Crypto,
            DbContext,
            Mapper,
            FileManagerLogic,
            CSVService)
    {
    }

    #endregion Object Declaration And Constructor

    #region 1. Admin Login

    [HttpPost(ActionsConsts.AdminAccount.AdminLogin)]
    public async Task<IActionResult> AdminLoginAsync(
        [FromBody] AdminLoginRequest request,
        [FromServices] IOptions<AuthConfigs> AuthConfigOptions)
    {
        if (request == null) request = new AdminLoginRequest();
        if (ModelState.IsValid) return ErrorResponse(ModelState);

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        request.UniqueId = Guid.NewGuid().ToString();
        var response = await helper.Login(request);

        if (response == null) return ErrorResponse();

        // Get Token
        response.Token = new AdminTokenHelpers(Crypto).GetAccessToken(AuthConfigOptions.Value, response, request.UniqueId);

        return OkResponse(response);
    }

    #endregion 1. Admin Login

    #region 2. Forgot Password

    [AllowAnonymous]
    [HttpPost(ActionsConsts.AdminAccount.ForgotPassword)]
    public async Task<IActionResult> AdminForgotPasswordAsync([FromBody] AdminForgotPasswordRequest request, [FromServices] IMessages messages)
    {
        if (request == null) request = new AdminForgotPasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        var helperResponse = await new AdminHelpers(DbContext, Crypto, Mapper).ForgotPassword(request);

        if (helperResponse == null) return ErrorResponse();

        string tokenUrl = $"{ActionsConsts.AdminBaseRoute}admin/reset-password/{helperResponse.AdminId}/{helperResponse.Token}";
        string token = $"<a href='{tokenUrl}'>Click here.</a>";

        // Send Email to admin
        new EmailHelpers(base.Localizer, messages).SendForgotPasswordEmail(helperResponse.Email, helperResponse.UserName, token, isAdmin: true);

        return OkResponse(string.Format(Localizer["response_forgot_password"].Value, helperResponse.Email));
    }

    #endregion 2. Forgot Password

    #region 3. Reset Password

    [AllowAnonymous]
    [HttpPost(ActionsConsts.AdminAccount.ResetPassword)]
    public async Task<IActionResult> AdminResetPasswordAsync([FromBody] AdminResetPasswordRequest request)
    {
        if (request == null) request = new AdminResetPasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var response = await helper.ResetPassword(request);

        if (response == null) return ErrorResponse();

        return OkResponse();
    }

    #endregion 3. Reset Password

    #region 4. Change Password

    [Authorize, HttpPost(ActionsConsts.AdminAccount.ChangePassword)]
    public async Task<IActionResult> UserChangePasswordAsync(
        [FromBody] AdminChangePasswordRequest request)
    {
        if (request == null) request = new AdminChangePasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        if (request.CurrentPassword.Equals(request.ConfirmPassword))
            return ErrorResponse("error_current_password_same_with_new_password");

        request.AdminId = GetUserId(User);
        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var response = await helper.ChangePassword(request);

        if (response == null) return ErrorResponse();

        if (!string.IsNullOrEmpty(response.ErrorMessage))
        {
            return ErrorResponse(response.ErrorMessage);
        }

        return OkResponse("response_password_changed_successfully");
    }

    #endregion 4. Change Password

    #region 5. Logout
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.AdminAccount.Logout)]
    public async Task<IActionResult> AdminLogoutAsync()
    {
        var adminId = GetUserId(User).ToString();
        var uniqueId = GetUniqueId(User);

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var response = await helper.DeleteToken(new AdminDeleteTokenRequest
        {
            AdminId = adminId,
            UniqueId = uniqueId
        });

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess)
        {
            if (response.ErrorMessage.Equals("error_token_expired"))
                return ErrorResponse(response.ErrorMessage, 401);
        }

        return OkResponse();
    }

    #endregion 5. Logout

    #region 6. Admin IsValid For Reset Password

    [AllowAnonymous]
    [HttpPost(ActionsConsts.AdminAccount.IsAdminValid)]
    public async Task<IActionResult> IsAdminValidAsync([FromBody] CheckAdminResetRequest request)
    {
        if (request == null) request = new CheckAdminResetRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var response = await helper.IsValidAdmin(request);

        if (response == null) return ErrorResponse();

        return OkResponse();
    }

    #endregion 6. Admin IsValid For Reset Password

    #region 7. Get Admin Profile
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.AdminAccount.GetAdminProfile)]
    public async Task<IActionResult> GetAdminProfile()
    {
        Guid adminId = GetUserId(User);
        if (adminId == new Guid() && adminId == Guid.Empty)
            return ErrorResponse();

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var response = await helper.GetAdminProfile(adminId);

        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 7. Get Admin Profile

    #region 8. Update Profile

    [Authorize, HttpPost(ActionsConsts.AdminAccount.UpdateProfile)]
    public async Task<IActionResult> EditProfileAsync([FromBody] AdminUpdateProfileRequest request,
         [FromServices] IOptions<AuthConfigs> AuthConfigOptions)
    {
        if (request == null) request = new AdminUpdateProfileRequest();

        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        //request.UniqueId = GetUniqueId(User);

        var response = await helper.EditProfile(request);

        //response.Token = new AdminTokenHelpers(Crypto).GetAccessToken(AuthConfigOptions.Value, response, request.UniqueId);

        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 8. Update Profile

    #region 9. Get all Menus
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.AdminAccount.DefaultPermissionList)]
    public async Task<IActionResult> GetDefaultPermission()
    {
        using var helper = new AdminHelpers(DbContext, Crypto, Mapper);
        var helperResponse = await helper.GetDefaultPermission();

        var response = JsonConvert.DeserializeObject<List<MenuResponse>>(helperResponse.Data);
        if (response == null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion
}