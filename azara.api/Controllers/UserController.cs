using azara.api.Helpers.Service.POSService;
using azara.models.Requests.QuickBooks;

namespace azara.api.Controllers;

public class UserController : BaseController
{
    #region Object Declaration And Constructor

    private BaseUrlConfigs BaseUrlConfigs { get; set; }

    private readonly IService Service;


    public UserController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        IOptions<BaseUrlConfigs> BaseUrlConfigsOptions,
        ICSVService CSVService,
        IService service) : base(
            Localizer,
            Crypto,
            DbContext,
            Mapper,
            FileManagerLogic,
            CSVService)
    {
        BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        Service = service;
    }

    #endregion Object Declaration And Constructor

    #region 1. User SignIn

    [AllowAnonymous, HttpPost(ActionsConsts.User.UserLogin)]
    public async Task<IActionResult> UserLoginAsync(
        [FromBody] UserSignInRequest request,
        [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigsOptions,
        [FromServices] IOptions<AuthConfigs> AuthConfigOptions)
    //[FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (request == null) request = new UserSignInRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        request.UniqueId = Guid.NewGuid().ToString();

        var helperResponse = await helper.Login(request);

        if (helperResponse == null) return ErrorResponse();
        if (!helperResponse.IsSuccess) return ErrorResponse(helperResponse.ErrorMessage);

        var response = JsonConvert.DeserializeObject<UserSignInResponse>(helperResponse.Data);

        //if (!string.IsNullOrEmpty(response.Image))
        //    response.Image = string.Format(baseUrlConfigsOptions.Value.BlobImageUrl, BlobContainerConsts.Profile, response.Image);

        // Get Token
        response.Token = new UserTokenHelpers(Crypto).GetAccessToken(AuthConfigOptions.Value, response);

        //await new NotificationHelpers(hubContext).SendWebNotifications(NotificationTypeConsts.UserLogin, "Congratulations your account has been successfully created.", request.Id.ToString());

        return OkResponse(response);
    }

    #endregion 1. User SignIn

    #region 2. User SignUp

    [AllowAnonymous, HttpPost(ActionsConsts.User.UserSignUp)]
    public async Task<IActionResult> UserRegisterAsync([FromBody] UserSignUpRequest request, [FromServices] IMessages messages)
    {
        if (request == null) request = new UserSignUpRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        var response = await helper.SignUp(request);

        if (response == null) return ErrorResponse();

        string tokenUrl = $"{ActionsConsts.ClientBaseRoute}sign-in/{GenericHelpers.Encipher(request.EmailId, 3)}";
        string token = $"<a href='{tokenUrl}'> click here.</a>";
        // Send Email to admin
        new EmailHelpers(base.Localizer, messages).SendForgotPasswordEmail(response.EmailId, response.FirstName, token, isAdmin: false, isSignUp: true);

        Service.InsertCustomer(DbContext, new POSCustomerInsertRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EMail = request.EmailId,
            Phone = request.MobileNumber,
            Phone2 = request.MobileNumber,
            Phone3 = request.MobileNumber,
            Phone4 = request.MobileNumber

        });

        DbContext.SaveChanges();

        return OkResponse(response);
    }

    #endregion 2. User SignUp

    #region 3. Forgot Password

    [AllowAnonymous, HttpPost(ActionsConsts.User.ForgotPassword)]
    public async Task<IActionResult> UserForgotPasswordAsync(
        [FromBody] UserForgotPasswordRequest request,
        [FromServices] IMessages messages)
    {
        if (request == null) request = new UserForgotPasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        var helperResponse = await new UserHelpers(DbContext, Crypto, Mapper).ForgotPassword(request);

        if (helperResponse == null) return ErrorResponse();

        string tokenUrl = $"{ActionsConsts.ClientBaseRoute}reset-password/{GenericHelpers.Encipher(helperResponse.EmailId, 3).Replace('.', '-')}";
        string token = $"<a href='{tokenUrl}'>Click here.</a>";
        // Send Email to admin
        new EmailHelpers(base.Localizer, messages).SendForgotPasswordEmail(helperResponse.EmailId, helperResponse.UserName, token, isUser: true);

        return OkResponse(string.Format(Localizer["response_forgot_password"].Value, helperResponse.EmailId));
    }

    #endregion 3. Forgot Password

    //#region 4. User Otp Verify

    //[Authorize, HttpPost(ActionsConsts.User.OtpVerify)]
    //public async Task<IActionResult> AdminOtpVerifyAsync([FromBody] UserOtpVerifyRequest request)
    //{
    //    var email = GetUserEmail(User);
    //    request.EmailId = email;

    //    using var helper = new UserHelpers(DbContext, Crypto, Mapper);
    //    var response = await helper.OtpVerify(request);

    //    if (response == null) return ErrorResponse();
    //    if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

    //    return OkResponse();
    //}

    //#endregion 4. User Otp Verify

    #region 5. Reset Password

    [HttpPost(ActionsConsts.User.ResetPassword)]
    public async Task<IActionResult> AdminResetPasswordAsync([FromBody] UserResetPasswordRequest request)
    {
        if (request == null) request = new UserResetPasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        var response = await helper.ResetPassword(request);

        if (response == null) return ErrorResponse();

        return OkResponse();
    }

    #endregion 5. Reset Password

    #region 6. Change Password

    [Authorize, HttpPost(ActionsConsts.User.ChangePassword)]
    public async Task<IActionResult> UserChangePasswordAsync(
        [FromBody] UserChangePasswordRequest request,
        [FromServices] IMessages messages)
    {
        if (request == null) request = new UserChangePasswordRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        if (request.CurrentPassword.Equals(request.ConfirmPassword))
            return ErrorResponse("error_current_password_same_with_new_password");

        request.UserId = GetUserId(User);
        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        var response = await helper.ChangePassword(request);

        if (response == null) return ErrorResponse();

        if (!string.IsNullOrEmpty(response.ErrorMessage))
        {
            return ErrorResponse(response.ErrorMessage);
        }
        // Send Email
        //new EmailHelpers(base.Localizer, messages).SendUserChangePasswordEmail(
        //    GetUserEmail(User),
        //    GetUserName(User));

        return OkResponse("response_password_changed_successfully");
    }

    #endregion 6. Change Password

    #region 7. Update Profile

    [Authorize, HttpPost(ActionsConsts.User.UserUpdate)]
    public async Task<IActionResult> EditProfileAsync(
        [FromBody] UserUpdateProfileRequest request,
        [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
    {
        if (request == null) request = new UserUpdateProfileRequest();

        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        //request.UniqueId = GetUniqueId(User);

        var response = await helper.EditProfile(request);

        //response.Token = new AdminTokenHelpers(Crypto).GetAccessToken(AuthConfigOptions.Value, response, request.UniqueId);

        if (response == null) return ErrorResponse();

        //if (string.IsNullOrEmpty(response.Image))
        //    response.Image = string.Format(baseUrlConfigs.Value.BlobImageUrl, BlobContainerConsts.Profile, response.Image);

        return OkResponse(response);
    }

    #endregion 7. Update Profile

    #region 8. Logout

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.User.Logout)]
    public async Task<IActionResult> UserLogoutAsync()
    {
        var adminId = GetUserId(User).ToString();
        var uniqueId = GetUniqueId(User);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        var response = await helper.DeleteToken(new UserDeleteTokenRequest
        {
            UserId = adminId,
            UniqueId = uniqueId
        });

        if (response == null) return ErrorResponse();

        return OkResponse();
    }

    #endregion 8. Logout

    #region 9. Get Profile

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [Authorize, HttpGet(ActionsConsts.User.UserProfile)]
    public async Task<IActionResult> GetUserProfileAsync(
        [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
    {
        Guid userId = GetUserId(User);
        if (userId == new Guid() && userId == Guid.Empty)
            return ErrorResponse();

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var helperResponse = await helper.GetUserProfile(userId);

        if (helperResponse == null) return ErrorResponse();

        //if (!string.IsNullOrEmpty(helperResponse.Image))
        //    helperResponse.Image = string.Format(baseUrlConfigs.Value.BlobImageUrl, BlobContainerConsts.Profile, helperResponse.Image);

        return OkResponse(helperResponse);
    }

    #endregion 9. Get Profile

    #region 10. User Get List

    [Authorize, HttpPost(ActionsConsts.User.UserGetList)]
    public async Task<IActionResult> UserGetListAsync([FromBody] UserGetListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var response = await helper.UserGetListAsync(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 10. User Get List

    #region 11. User Get By Id

    [Authorize, HttpPost(ActionsConsts.User.UserGetById)]
    public async Task<IActionResult> UserGetBtyIdAsync([FromBody] BaseRequiredIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var response = await helper.UserGetById(request);

        return OkResponse(response);
    }

    #endregion 11. User Get By Id

    #region 12. Delete User

    [HttpPost(ActionsConsts.User.UserDelete)]
    public async Task<IActionResult> DeleteUser([FromBody] BaseIdRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var response = await helper.DeleteUser(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);
    }

    #endregion 12. Delete User

    #region 13. User Product Request

    [Authorize, HttpPost(ActionsConsts.User.UesrProductRequest)]
    public async Task<IActionResult> UserProductRequestAsync([FromBody] UserProductRequest request, [FromServices] IMessages messages)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        //string userName = GetUserName(User);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var helperResponse = await helper.UserProductRequestAsync(request);
        if (helperResponse == null) return ErrorResponse();

        if (!helperResponse.IsSuccess) return ErrorResponse(helperResponse.ErrorMessage);

        var userData = await DbContext.User.FirstOrDefaultAsync(f => f.Id.Equals(request.UserId) && f.Active && !f.Deleted);
        // Send Email to admin
        new EmailHelpers(base.Localizer, messages).SendProductRequestMail("support@azara-app.com", userData.FirstName, request.Name, request.Image, request.Description);

        return OkResponse();
    }

    #endregion 13. User Product Request

    #region 14. User IsValid For SignUp

    [AllowAnonymous]
    [HttpPost(ActionsConsts.User.IsUserValid)]
    public async Task<IActionResult> IsUserValidAsync([FromBody] CheckUserValidRequest request)
    {
        if (request == null) request = new CheckUserValidRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);
        var response = await helper.IsValidUser(request);

        if (response == null) return ErrorResponse();

        return OkResponse();
    }

    #endregion 14. User IsValid For SignUp

    #region 15. Upload Image

    [Authorize, HttpPost(ActionsConsts.User.UploadProfilePicture)]
    public async Task<IActionResult> UploadProfilePictureAsync(
       [FromForm] FileWithNameUploadRequest request,
       [FromServices] IFileManagerLogic fileManager)
    {
        if (!ModelState.IsValid) return ErrorResponse(ModelState);

        var userId = GetUserId(User);

        var ProfileUrl = await fileManager.UploadId(request.File, BlobContainerConsts.Profile, request.OldFileName, userId);

        if (string.IsNullOrEmpty(ProfileUrl)) return ErrorResponse();

        request.UniqueId = GetUniqueId(User);
        request.UserId = GetUserId(User);

        var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.UserId));

        if (user == null)
        {
            return ErrorResponse("User not found");
        }

        user.Image = ProfileUrl;
        DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 15. Upload Image

    #region 16. User List without Pagination

    [HttpPost(ActionsConsts.User.UserList)]
    public async Task<IActionResult> UserGet([FromBody] UserListRequest request)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var response = await helper.UserList(request);
        if (response is null) return ErrorResponse();

        return OkResponse(response);

    }
    #endregion 16. User List without Pagination

    #region 17. User status Update

    [Authorize, HttpPost(ActionsConsts.User.UserStatusUpdate)]
    public async Task<IActionResult> UserStatusUpdateAsync(
        [FromBody] BaseStatusUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
    {
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new UserHelpers(DbContext, Crypto, Mapper);

        var response = await helper.UserStatusUpdate(request);

        CheckHelperResponse(response);

        await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.UserStatusUpdate,
            new
            {
                request.Id,
                request.Active
            });

        return OkResponse();
    }

    #endregion 17. User status Update
}