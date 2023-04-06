using azara.models.Mapping;

namespace azara.api.Helpers;

public class AdminHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public AdminHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
    }

    #endregion Constructor

    #region Admin Seed Insert

    public async Task<ApiSeedInsertResponse> SeedInsert(ApiSeedInsertRequest request)
    {
        // Check and Insert Master Admin
        var adminData = DbContext.Admin.Where(x => x.EmailId.Equals(request.EmailId));
        if (!adminData.Any())
        {
            DbContext.Admin.Add(new AdminEntity
            {
                Name = request.Name,
                UserName = request.UserName,
                EmailId = request.EmailId,
                //EmailVerified = request.EmailVerified,
                Password = Crypto.EncryptPassword(request.Password),
                Mobile = request.Mobile,
                //MobileVerified = request.MobileVerified,
                //LastLoginAt = request.LastLoginAt
            });

            return await Task.FromResult(new ApiSeedInsertResponse { IsNewAdmin = true });
        }

        return await Task.FromResult(new ApiSeedInsertResponse { IsNewAdmin = false });
    }

    #endregion Admin Seed Insert

    #region 1. Admin Login

    public async Task<AdminLoginResponse> Login(AdminLoginRequest request)
    {
        // Encrt password
        var password = Crypto.EncryptPassword(request.Password);

        // Check Username
        if (!DbContext.Admin.Where(x => x.EmailId.Equals(request.EmailId.ToLower()) && !x.Deleted).Any())
            throw new ApiException("error_account_not_found");

        var admin = await DbContext.Admin.FirstOrDefaultAsync(x =>
                        x.EmailId.Equals(request.EmailId.ToLower()) &&
                        x.Password.Equals(password) &&
                        !x.Deleted);

        if (admin == null)
            throw new ApiException("error_password_invalid");

        if (!admin.Active)
            throw new ApiException("error_user_inactive");

        var idList = new[] { admin.CreatedBy, admin.ModifiedBy };

        var userCreators = DbContext.Admin.Where(x => idList.Contains(x.Id))
            .Select(x => new { x.Id, x.Name, x.UserName });

        // Select Names of admin approvals
        var createdBy = userCreators.FirstOrDefault(x => x.Id.Equals(admin.CreatedBy))?.Name;
        var modifiedBy = userCreators.FirstOrDefault(x => x.Id.Equals(admin.ModifiedBy))?.Name;

        //remove old token
        var tokenList = DbContext.AdminAccessToken.Where(x => x.AdminId.Equals(admin.Id) && x.Purpose.Equals("Login"));
        DbContext.AdminAccessToken.RemoveRange(tokenList);
        DbContext.SaveChanges();

        var uniqueId = Guid.NewGuid();
        // Generate New Token
        await DbContext.AddRangeAsync(new AdminAccessTokenEntity
        {
            AdminId = admin.Id,
            Purpose = "Login",
            UniqueId = uniqueId,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
        });
        DbContext.SaveChanges();

        // Response Generation
        var response = new AdminLoginResponse
        {
            Id = admin.Id,
            Name = admin.Name,
            Username = admin.UserName,
            EmailId = admin.EmailId,
            //EmailVerified = admin.EmailVerified,
            Mobile = admin.Mobile,
            //MobileVerified = admin.MobileVerified,
            ProfilePhoto = admin.ProfilePhoto,
            //LastLoginAt = admin.LastLoginAt,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = admin.Id.ToString(),
            ModifiedBy = modifiedBy,
            UniqueId = uniqueId.ToString(),
        };
        return response;
    }

    #endregion 1. Admin Login

    #region 2. Forgot Password

    public async Task<AdminForgotPasswordResponse> ForgotPassword(AdminForgotPasswordRequest request)
    {
        // Remove Old Token
        var admin = DbContext.Admin.FirstOrDefault(x => x.EmailId.Equals(request.EmailId));

        if (admin == null)
            throw new ApiException("error_email_not_found");

        // Code for generate OTP
        var generator = new Random();
        var otp = generator.Next(0, 1000000).ToString("D6");

        admin.Otp = otp;
        admin.OtpExpireTime = DateTime.UtcNow.AddMinutes(3);

        DbContext.Admin.Attach(admin);
        DbContext.Entry(admin).Property(x => x.Otp).IsModified = true;
        DbContext.Entry(admin).Property(x => x.OtpExpireTime).IsModified = true;
        DbContext.SaveChanges();

        var response = new AdminForgotPasswordResponse
        {
            UserName = admin.UserName,
            Email = admin.EmailId,
            Token = Crypto.Encrypt(otp).Replace("/", ""),
            Created = admin.Created,
            Modified = admin.Modified,
            AdminId = GenericHelpers.Encipher(admin.Id.ToString(), 3)
        };
        return response;
    }

    #endregion 2. Forgot Password

    #region 3. Reset Password

    public async Task<BaseResponse> ResetPassword(AdminResetPasswordRequest request)
    {
        // Check user exists or not
        var admin = DbContext.Admin.FirstOrDefault(x => x.Id.Equals(Guid.Parse(request.AdminId)));

        if (admin == null)
            throw new ApiException("error_account_not_found");

        ////var dateDifference = DateTime.UtcNow.AddMinutes(5);
        //if (admin.OtpExpireTime <= DateTime.UtcNow)
        //    throw new ApiException("error_otp_expired");

        admin.Password = Crypto.EncryptPassword(request.Password);
        admin.Modified = DateTime.UtcNow;

        DbContext.Admin.Attach(admin);
        DbContext.Entry(admin).Property(x => x.Password).IsModified = true;
        DbContext.Entry(admin).Property(x => x.Modified).IsModified = true;
        DbContext.SaveChanges();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 3. Reset Password

    #region 4. Change Password

    public async Task<BaseResponse> ChangePassword(AdminChangePasswordRequest request)
    {
        // Validate Current Password
        request.CurrentPassword = Crypto.EncryptPassword(request.CurrentPassword);
        var admin = DbContext.Admin.FirstOrDefault(x =>
                        x.Id.Equals(request.AdminId) &&
                        x.Password.Equals(request.CurrentPassword));

        if (admin == null)
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_current_password_invalid" });

        admin.Password = Crypto.EncryptPassword(request.Password);
        admin.Modified = DateTime.UtcNow;
        admin.ModifiedBy = request.AdminId;

        DbContext.Admin.Attach(admin);
        DbContext.Entry(admin).Property(x => x.Password).IsModified = true;
        DbContext.Entry(admin).Property(x => x.Modified).IsModified = true;
        DbContext.Entry(admin).Property(x => x.ModifiedBy).IsModified = true;

        // Remove Old Token
        var tokenList = DbContext.AdminAccessToken.Where(x =>
                            x.AdminId.Equals(request.AdminId) &&
                            x.Purpose.Equals("Login"));

        if (!tokenList.Any())
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_token_expired" });

        DbContext.AdminAccessToken.RemoveRange(tokenList);

        DbContext.SaveChanges();

        return await Task.FromResult(new BaseResponse { IsSuccess = true });
    }

    #endregion 4. Change Password

    #region 5. Delete Token - Logout

    public async Task<BaseResponse> DeleteToken(AdminDeleteTokenRequest request)
    {
        var admin = await DbContext.Admin.FirstOrDefaultAsync(x =>
                            x.Id.Equals(new(request.AdminId)) &&
                            !x.Deleted);

        // Remove Old Token
        var tokenList = DbContext.AdminAccessToken.Where(x =>
                        x.AdminId.ToString().Equals(request.AdminId) &&
                        x.UniqueId.ToString().Equals(request.UniqueId) &&
                        x.Purpose.Equals("Login"));

        if (!tokenList.Any())
            throw new ApiException("error_token_expired");

        DbContext.AdminAccessToken.RemoveRange(tokenList);
        DbContext.SaveChanges();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Delete Token - Logout

    #region 6. Admin IsValid For Reset Password

    public async Task<BaseResponse> IsValidAdmin(CheckAdminResetRequest request)
    {
        Guid adminId = new(GenericHelpers.Decipher(request.AdminId.ToString(), 3));
        string otp = Crypto.Decrypt(request.Token);
        // Check user exists or not
        var admin = await DbContext.Admin.FirstOrDefaultAsync(x => x.Id.Equals(adminId) && x.Otp.Equals(otp));

        if (admin == null)
            throw new ApiException("error_account_not_found");

        if (admin.OtpExpireTime <= DateTime.UtcNow)
            throw new ApiException("error_otp_expired");

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Admin IsValid For Reset Password

    #region 7. Get Admin Profile

    public async Task<AdminResponse> GetAdminProfile(Guid adminId)
    {
        var data = await DbContext.Admin.FirstOrDefaultAsync(f => f.Id.Equals(adminId) && f.Active && !f.Deleted);

        if (data == null)
            throw new ApiException("Invalid Admin");
        else
        {
            var adminResponseData = new AdminResponse();
            Mapper.Map(data, adminResponseData);
            return adminResponseData;
        }
    }

    #endregion 7. Get Admin Profile

    #region 8. Update Profile

    public async Task<AdminUpdateProfileResponse> EditProfile(AdminUpdateProfileRequest request)
    {
        // Get Admin
        var admin = DbContext.Admin.FirstOrDefault(x => x.Id.Equals(new(request.Id)));

        if (admin == null)
            throw new ApiException("error_account_not_found");

        admin.ProfilePhoto = request.ProfilePhoto;
        admin.UserName = request.UserName;
        admin.EmailId = request.EmailId;
        admin.ModifiedBy = Guid.Parse(request.Id);
        admin.Modified = DateTime.UtcNow;

        DbContext.Admin.Attach(admin);
        DbContext.Entry(admin).Property(x => x.ProfilePhoto).IsModified = true;
        DbContext.Entry(admin).Property(x => x.UserName).IsModified = true;
        DbContext.Entry(admin).Property(x => x.EmailId).IsModified = true;
        DbContext.Entry(admin).Property(x => x.ModifiedBy).IsModified = true;
        DbContext.Entry(admin).Property(x => x.Modified).IsModified = true;
        DbContext.SaveChanges();

        var response = new AdminUpdateProfileResponse
        {
            //Id= admin.Id,
            ProfilePhoto = admin.ProfilePhoto,
            UserName = admin.UserName,
            EmailId = admin.EmailId
            //UniqueId = request.UniqueId,
        };
        return response;
    }

    #endregion 8. Update Profile

    #region 9. Get all Menus

    public async Task<BaseResponse> GetDefaultPermission()
    {
        var menuList = DbContext.Menu.ToList();

        var response = new MenuMap().Map(menuList.Where(x => x.ParentId == null).ToList(), menuList.ToList());

        return await Task.FromResult(new BaseResponse
        {
            IsSuccess = true,
            Data = JsonConvert.SerializeObject(response)
        });
    }

    #endregion 9. Get all Menus

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}