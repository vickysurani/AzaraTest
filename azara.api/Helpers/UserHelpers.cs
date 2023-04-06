namespace azara.api.Helpers;

public class UserHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    public HttpStatusCode StatusCode { get; set; }


    //BlueCheckResponse blueCheckResponse = new BlueCheckResponse();

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public UserHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
    }

    #endregion Constructor

    #region Calculate Total Pages

    public static int CalculateTotalPages(
        int total,
        int? pageSize)
    {
        var pages = Convert.ToDecimal(total) / Convert.ToDecimal(pageSize);
        var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

        return response;
    }

    #endregion Calculate Total Pages

    #region 1. User Login

    public async Task<BaseResponse> Login(UserSignInRequest request)
    {
        try
        {
            // Encrt password
            var password = Crypto.EncryptPassword(request.Password);

            // Check Username
            if (!DbContext.User.Any(x => (x.EmailId.Equals(request.EmailId.ToLower()) || x.Password.Equals(password)) && !x.Deleted))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "Invalid email address & password" });

            if (!DbContext.User.Any(x => x.EmailId.Equals(request.EmailId.ToLower()) && !x.Deleted))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_account_not_found" });

            var user = await DbContext.User.FirstOrDefaultAsync(x =>
                            x.EmailId.Equals(request.EmailId.ToLower()) &&
                            x.Password.Equals(password) &&
                            !x.Deleted);

            if (user == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_password_invalid" });

            ////Change Login flag
            //if (user.IsFirstLogin == false)
            //{
            //    user.IsFirstLogin = true;
            //    DbContext.User.Attach(user);
            //    DbContext.Entry(user).Property(x => x.IsFirstLogin).IsModified = true;
            //    DbContext.SaveChanges();
            //}

            //if (!user.EmailVerified)
            //    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_email_not_verified" });

            if (!user.Active)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_user_inactive" });
            var idList = new[] { user.CreatedBy, user.ModifiedBy };

            var userCreators = DbContext.User.Where(x => idList.Contains(x.Id))
                .Select(x => new { x.Id, Name = $"{x.FirstName} ({x.LastName})" }).ToArray();

            // Select Names of admin approvals
            var createdBy = userCreators.FirstOrDefault(x => x.Id.Equals(user.CreatedBy))?.Name;
            var modifiedBy = userCreators.FirstOrDefault(x => x.Id.Equals(user.ModifiedBy))?.Name;

            user.Active = true;
            DbContext.SaveChanges();



            //remove old token
            var tokenList = await DbContext.UserAccessToken.Where(x => x.UserId.Equals(user.Id) && x.UniqueId.Equals(request.UniqueId) && x.Purpose.Equals("Login")).ToListAsync();
            if (tokenList != null && tokenList.Count > 0)
            {
                DbContext.UserAccessToken.RemoveRange(tokenList);
                await DbContext.SaveChangesAsync();
            }

            var uniqueId = Guid.NewGuid();
            // Generate New Token
            await DbContext.AddRangeAsync(new UserAccessTokenEntity
            {
                UserId = user.Id,
                Purpose = "Login",
                UniqueId = uniqueId,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            });
            DbContext.SaveChanges();

            var response = new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.EmailId,
                user.EmailVerified,
                user.MobileNumber,
                user.Image,
                user.Password,
                Created = DateTime.UtcNow,
                CreatedBy = createdBy,
                Modified = DateTime.UtcNow,
                ModifiedBy = modifiedBy,
                UniqueId = uniqueId.ToString()
            };
            return await Task.FromResult(new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new BaseResponse
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
                Data = JsonConvert.SerializeObject(ex)
            });
        }
    }

    #endregion 1. User Login

    #region 2. User Sign Up

    public async Task<UserSignUpResponse> SignUp(UserSignUpRequest request)
    {
        // Check user exists or not
        if (DbContext.User.Any(x => x.EmailId.Equals(request.EmailId)))
            throw new ApiException("error_email_exist");

        if (DbContext.POSCustomer.Any(x => x.Phone.Equals(request.MobileNumber) || x.Phone2.Equals(request.MobileNumber) || x.Phone3.Equals(request.MobileNumber) || x.Phone4.Equals(request.MobileNumber)))
            throw new ApiException("error_mobile_exist_in_pos_customer", StatusCodeConsts.POSError);

        if (!string.IsNullOrWhiteSpace(request.ReferralCode) && !DbContext.Referral.Any(x => x.ReferralCode.Equals(request.ReferralCode)))
            throw new ApiException("referral_code_does_not_exists");



        //var blueCheckResponse = await new BlueCheckHelpers(DbContext, Crypto, Mapper).VerifyWithBlueCheck(new BlueCheckVerifyModel
        //{
        //    files = new DocumentFiles
        //    {
        //        id_front = request.FrontImagePath,
        //        id_selfie = request.SelfieImagePath
        //    },
        //    userData = new BlueCheckPostRequest
        //    {
        //        first_name = request.FirstName,
        //        address = request.Address,
        //        country = request.Country,
        //        dob = request.BirthDate.HasValue ? request.BirthDate.Value.ToString("yyyy-MM-dd") : DateTime.UtcNow.ToString(),
        //        email = request.EmailId,
        //        last_name = request.LastName,
        //        ZipCode = request.ZipCode,
        //    }
        //});

        if (request.ReferralCode != null)
        {
            await DbContext.User.AddAsync(new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailId = request.EmailId,
                MobileNumber = request.MobileNumber,
                BirthDate = request.BirthDate,
                Address = request.Address,
                State = request.State,
                City = request.City,
                ZipCode = request.ZipCode,
                Image = request.Image,
                Password = Crypto.EncryptPassword(request.Password),
                ConfirmPassword = Crypto.EncryptPassword(request.ConfirmPassword),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                FrontImage = /*blueCheckResponse.StatusCode == HttpStatusCode.OK ?*/ request.FrontImage,
                SelfieImage = /*blueCheckResponse.StatusCode == HttpStatusCode.OK ? */ request.SelfieImage,
                //Points = 100,
                IsFirstLogin = false

            });
        }
        else
        {
            await DbContext.User.AddAsync(new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailId = request.EmailId,
                MobileNumber = request.MobileNumber,
                BirthDate = request.BirthDate,
                Address = request.Address,
                State = request.State,
                City = request.City,
                ZipCode = request.ZipCode,
                Image = request.Image,
                Password = Crypto.EncryptPassword(request.Password),
                ConfirmPassword = Crypto.EncryptPassword(request.ConfirmPassword),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                FrontImage = /*blueCheckResponse.StatusCode == HttpStatusCode.OK ?*/ request.FrontImage,
                SelfieImage = /*blueCheckResponse.StatusCode == HttpStatusCode.OK ?*/ request.SelfieImage,
                //Points = 100,
                IsFirstLogin = false

            });
        }

        await DbContext.SaveChangesAsync();

        Guid referalUserId = new();
        Guid ReferUserId = new();
        if (request.ReferralCode != null)
        {
            ReferUserId = (await DbContext.Referral.FirstOrDefaultAsync(f => f.ReferralCode.ToLower().Equals(request.ReferralCode.ToLower())))?.UserId ?? Guid.Empty;
        }
        

        if (!string.IsNullOrWhiteSpace(request.ReferralCode))
        {
            var refUser = DbContext.Referral.FirstOrDefault(x => x.ReferralCode.ToLower().Equals(request.ReferralCode.ToLower()));

            if (refUser != null)
            {
                refUser.Count++;
                referalUserId = ReferUserId;
            }
        }


        var UserId = (await DbContext.User.FirstOrDefaultAsync(f => f.EmailId.Equals(request.EmailId)))?.Id ?? Guid.Empty;

        //var user = DbContext.Referral.FirstOrDefault(x => x.ReferralCode.Equals(request.ReferralCode));

        //var userData = DbContext.User.FirstOrDefault(x => x.Id.Equals(user.ReferredTo));

        var refResponse = await new ReferralHelpers(DbContext, Crypto).ReferralInsert(new models.Requests.Referral.ReferralUpdateRequest
        {
            UserId = UserId,
            ReferredTo = referalUserId
        });

        if (request.ReferralCode != null)
        {
            await DbContext.Notification.AddAsync(new NotificationEntity
            {
                Title = "Points Added",
                Description = "50 point added to your profile for Referral",
                ReadableTime = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                UserId = referalUserId,
                Modified = DateTime.UtcNow,
                Icon = "images/point-icon.svg",
            });
        }



        if (referalUserId != new Guid())
        {
            await new PointsHelpers(DbContext, Crypto).PointsInsert(new models.Requests.Points.PointsInsertUpdateRequest
            {
                AvailablePoints = 50,
                PointName = "Referral",
                UserId = referalUserId
            });
        }

        var userdata = DbContext.User.FirstOrDefault(x => x.EmailId.Equals(request.EmailId));
            await DbContext.Notification.AddAsync(new NotificationEntity
            {
                Title = "Signup bonus",
                Description = "100 point added to your profile for signup ",
                ReadableTime = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                UserId = userdata.Id,
                Modified = DateTime.UtcNow,
                Icon = "images/point-icon.svg",
            });
        


        if (request.ReferralCode == null)
        {
            await new PointsHelpers(DbContext, Crypto).PointsInsert(new models.Requests.Points.PointsInsertUpdateRequest
            {
                AvailablePoints = 100,
                PointName = "Sign up",
                UserId = userdata.Id
            });
        }

        if (request.ReferralCode != null)
        {
            await new PointsHelpers(DbContext, Crypto).PointsInsert(new models.Requests.Points.PointsInsertUpdateRequest
            {
                AvailablePoints = 50,
                PointName = "Referral",
                UserId = userdata.Id
            });

            await new PointsHelpers(DbContext, Crypto).PointsInsert(new models.Requests.Points.PointsInsertUpdateRequest
            {
                AvailablePoints = 100,
                PointName = "Sign up",
                UserId = userdata.Id
            });

            await DbContext.Notification.AddAsync(new NotificationEntity
            {
                Title = "Signup bonus",
                Description = "50 point added to your profile for Referral bonus",
                ReadableTime = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                UserId = userdata.Id,
                Modified = DateTime.UtcNow,
                Icon = "images/point-icon.svg",
            });
        }


        var response = new UserSignUpResponse
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailId = request.EmailId,
            MobileNumber = request.MobileNumber,
            BirthDate = request.BirthDate,
            //Country = request.Country,
            Address = request.Address,
            City = request.City,
            State = request.State,
            ZipCode = request.ZipCode,
            Image = request.Image,
            Password = Crypto.EncryptPassword(request.Password),
            ConfirmPassword = Crypto.EncryptPassword(request.ConfirmPassword)
        };
        await DbContext.SaveChangesAsync();
        return response;
    }

    #endregion 2. User Sign Up

    #region 3. User Forgot Password

    public async Task<UserForgotPasswordResponse> ForgotPassword(UserForgotPasswordRequest request)
    {
        // Remove Old Token
        var user = DbContext.User.FirstOrDefault(x => x.EmailId.Equals(request.EmailId));

        if (user == null)
            throw new ApiException("error_email_not_found");

        // Code for generate OTP
        var generator = new Random();
        var otp = generator.Next(0, 1000000).ToString("D6");

        user.Otp = otp;
        user.OtpExpireTime = DateTime.UtcNow.AddMinutes(3);

        DbContext.User.Attach(user);
        DbContext.Entry(user).Property(x => x.Otp).IsModified = true;
        DbContext.Entry(user).Property(x => x.OtpExpireTime).IsModified = true;
        DbContext.SaveChanges();

        var response = new UserForgotPasswordResponse
        {
            UserName = user.FirstName,
            EmailId = user.EmailId
        };
        return response;
    }

    #endregion 3. User Forgot Password

    #region 4. User Otp Verify

    public async Task<BaseResponse> OtpVerify(UserOtpVerifyRequest request)
    {
        try
        {
            var user = DbContext.User.FirstOrDefault(x => x.EmailId.Equals(request.EmailId));

            if (user == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_account_not_found" });

            if (!string.IsNullOrEmpty(user.EmailOtp) && !user.EmailOtp.Equals(request.Otp))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_invalid_otp" });

            var dateDifference = (DateTime.UtcNow - user.EmailOtpExpiry).GetValueOrDefault().TotalMinutes;
            if (dateDifference >= 5)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_otp_expired" });

            user.Modified = DateTime.UtcNow;
            user.EmailVerified = true;
            DbContext.SaveChanges();

            return await Task.FromResult(new BaseResponse { IsSuccess = true });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new BaseResponse
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
                Data = JsonConvert.SerializeObject(ex)
            });
        }
    }

    #endregion 4. User Otp Verify

    #region 5. Reset Password

    public async Task<BaseResponse> ResetPassword(UserResetPasswordRequest request)
    {
        // Check user exists or not
        var user = DbContext.User.FirstOrDefault(x => x.EmailId.Equals(request.EmailId));

        if (user == null)
            throw new ApiException("error_account_not_found");

        //if (!string.IsNullOrEmpty(user.Otp) && !user.Otp.Equals(request.Otp))
        //    throw new ApiException("error_invalid_otp");

        //var dateDifference = DateTime.UtcNow.AddMinutes(5);
        if (user.OtpExpireTime <= DateTime.UtcNow)
            throw new ApiException("error_otp_expired");

        user.Password = Crypto.EncryptPassword(request.Password);
        user.Modified = DateTime.UtcNow;

        DbContext.User.Attach(user);
        DbContext.Entry(user).Property(x => x.Password).IsModified = true;
        DbContext.Entry(user).Property(x => x.Modified).IsModified = true;
        DbContext.SaveChanges();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Reset Password

    #region 6. Change Password

    public async Task<BaseResponse> ChangePassword(UserChangePasswordRequest request)
    {
        // Check user exists or not
        var isUserExists = DbContext.User.Any(x => x.Id.Equals(request.UserId));

        if (!isUserExists)
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_account_not_found" });

        // Validate Current Password
        request.CurrentPassword = Crypto.EncryptPassword(request.CurrentPassword);
        var user = DbContext.User.FirstOrDefault(x =>
                        x.Id.Equals(request.UserId) &&
                        x.Password.Equals(request.CurrentPassword));

        if (user == null)
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_current_password_invalid" });

        user.Password = Crypto.EncryptPassword(request.Password);
        user.Modified = DateTime.UtcNow;
        user.ModifiedBy = request.UserId;

        DbContext.User.Attach(user);
        DbContext.Entry(user).Property(x => x.Password).IsModified = true;
        DbContext.Entry(user).Property(x => x.Modified).IsModified = true;
        DbContext.Entry(user).Property(x => x.ModifiedBy).IsModified = true;

        // Remove Old Token
        var tokenList = DbContext.UserAccessToken.Where(x =>
                            x.UserId.Equals(request.UserId) &&
                            x.Purpose.Equals("Login"));

        if (!tokenList.Any())
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_token_expired" });

        DbContext.UserAccessToken.RemoveRange(tokenList);

        DbContext.SaveChanges();

        return await Task.FromResult(new BaseResponse { IsSuccess = true });
    }

    #endregion 6. Change Password

    #region 7. Update Profile

    public async Task<UserUpdateProfileResponse> EditProfile(UserUpdateProfileRequest request)
    {
        var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(Guid.Parse(request.Id)));

        // Get User
        if (!user.EmailId.Equals(request.EmailId) && DbContext.User.Any(x => x.EmailId.Equals(request.EmailId)))
            throw new ApiException("user_already_exists");

        user.Image = request.Image;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.EmailId = request.EmailId;
        user.MobileNumber = request.MobileNumber;
        user.BirthDate = request.BirthDate;
        user.ModifiedBy = Guid.Parse(request.Id);
        user.Modified = DateTime.UtcNow;
        user.Points = request.Points;
        user.State = request.State;
        user.City = request.City;
        user.Address = request.Address;
        user.ZipCode = request.ZipCode;
        user.IsFirstLogin = request.ISFirstLogin;


        DbContext.User.Attach(user);
        DbContext.Entry(user).Property(x => x.Image).IsModified = true;
        DbContext.Entry(user).Property(x => x.FirstName).IsModified = true;
        DbContext.Entry(user).Property(x => x.LastName).IsModified = true;
        DbContext.Entry(user).Property(x => x.EmailId).IsModified = true;
        DbContext.Entry(user).Property(x => x.MobileNumber).IsModified = true;
        DbContext.Entry(user).Property(x => x.BirthDate).IsModified = true;
        DbContext.Entry(user).Property(x => x.ModifiedBy).IsModified = true;
        DbContext.Entry(user).Property(x => x.Modified).IsModified = true;
        DbContext.Entry(user).Property(x => x.Points).IsModified = true;
        DbContext.Entry(user).Property(x => x.City).IsModified = true;
        DbContext.Entry(user).Property(x => x.State).IsModified = true;
        DbContext.Entry(user).Property(x => x.Address).IsModified = true;
        DbContext.Entry(user).Property(x => x.ZipCode).IsModified = true;
        DbContext.Entry(user).Property(x => x.IsFirstLogin).IsModified = true;

        DbContext.SaveChanges();

        var response = new UserUpdateProfileResponse
        {
            //Id= admin.Id,
            Image = user.Image,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailId = user.EmailId,
            MobileNumber = user.MobileNumber,
            BirthDate = user.BirthDate,
            Address = user.Address,
            City = user.City,
            State = user.State,
            ZipCode = user.ZipCode,
            Points = user.Points,
            IsFirstLogin = user.IsFirstLogin,
            //UniqueId = request.UniqueId,
        };
        return response;
    }

    #endregion 7. Update Profile

    #region 8. Delete Token - Logout

    public async Task<BaseResponse> DeleteToken(UserDeleteTokenRequest request)
    {
        var user = await DbContext.User.FirstOrDefaultAsync(x =>
                        x.Id.Equals(new(request.UserId)) &&
                        !x.Deleted);

        // Remove Old Token
        var tokenList = DbContext.UserAccessToken.Where(x =>
                            x.UserId.ToString().Equals(request.UserId) &&
                            x.UniqueId.ToString().Equals(request.UniqueId) &&
                            x.Purpose.Equals("Login"));

        if (!tokenList.Any())
            throw new ApiException("error_token_expired");

        DbContext.UserAccessToken.RemoveRange(tokenList);
        //user.Active = false;
        DbContext.SaveChanges();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 8. Delete Token - Logout

    #region 9. Get User Profile

    public async Task<UserResponse> GetUserProfile(Guid userId)
    {
        var userData = await DbContext.User.FirstOrDefaultAsync(f => f.Id.Equals(userId) && f.Active && !f.Deleted);

        if (userData == null)
            throw new ApiException("Invalid user");
        else
        {
            var userResponceData = new UserResponse();
            Mapper.Map(userData, userResponceData);
            return userResponceData;
        }
    }

    #endregion 9. Get User Profile

    #region 10. Get User List

    public async Task<PaginationResponse> UserGetListAsync([FromBody] UserGetListRequest request)
    {
        var userList = (
            from U in DbContext.User
            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || U.FirstName.ToLower().Contains(request.Name.ToString().ToLower())
                || U.LastName.ToLower().Contains(request.Name.ToString().ToLower()))

                && U.Deleted.Equals(request.IsDeleted)

            select new
            {
                Id = U.Id,
                FirstName = U.FirstName,
                LastName = U.LastName,
                EmailId = U.EmailId,
                MobileNumber = U.MobileNumber,
                ReferraCode = DbContext.Referral.Where(f => f.ReferredTo.Equals(U.Id) && !f.Deleted).Select(s => s.ReferralCode).FirstOrDefault(),
                Count = DbContext.Referral.Where(f => f.ReferredTo.Equals(U.Id) && !f.Deleted).Select(s => s.Count).FirstOrDefault(),
                Created = U.Created,
                Modified = U.Modified,
                Active = U.Active,
                Address = U.Address,
                City = U.City,
                State = U.State,
                ZipCode = U.ZipCode,
                AssignPoints = U.Points
            }).OrderByDescending(x => x.Modified).ToList();

        if (userList == null)
            throw new ApiException("error_user_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.FirstName).ToList();
                else
                    userList = userList.OrderBy(x => x.FirstName).ToList();
                break;

            case "count":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.Count).ToList();
                else
                    userList = userList.OrderBy(x => x.Count).ToList();
                break;

            case "signup_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.Created).ToList();
                else
                    userList = userList.OrderBy(x => x.Created).ToList();
                break;

            default:

                userList = userList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        var total = userList.Count();
        var totalPages = UserHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = userList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = eventPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 10. Get User List

    #region 11. User List Without Pagination

    public async Task<List<UserListsResponse>> UserList([FromBody] UserListRequest request)
    {
        var userList = (
            from U in DbContext.User
            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || U.FirstName.ToLower().Contains(request.Name.ToString().ToLower())
                || U.LastName.ToLower().Contains(request.Name.ToString().ToLower()))

                && U.Deleted.Equals(request.IsDeleted)

            select new UserListsResponse
            {
                Id = U.Id,
                FirstName = U.FirstName,
                LastName = U.LastName,
                EmailId = U.EmailId,
                MobileNumber = U.MobileNumber,
                ReferraCode = DbContext.Referral.Where(f => f.ReferredTo.Equals(U.Id) && !f.Deleted).Select(s => s.ReferralCode).FirstOrDefault(),
                Count = DbContext.Referral.Where(f => f.ReferredTo.Equals(U.Id) && !f.Deleted).Select(s => s.Count).FirstOrDefault(),
                Created = U.Created,
                Modified = U.Modified,
                Active = U.Active,
                Points = U.Points,
            }).OrderByDescending(x => x.Modified).ToList();

        if (userList == null)
            throw new ApiException("error_user_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.FirstName).ToList();
                else
                    userList = userList.OrderBy(x => x.FirstName).ToList();
                break;

            case "count":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.Count).ToList();
                else
                    userList = userList.OrderBy(x => x.Count).ToList();
                break;

            case "signup_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    userList = userList.OrderByDescending(x => x.Created).ToList();
                else
                    userList = userList.OrderBy(x => x.Created).ToList();
                break;

            default:

                userList = userList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return userList;
    }

    #endregion 10. Get User List

    #region 11. User Get By Id

    public async Task<UserGetbyIdResponse> UserGetById([FromBody] BaseRequiredIdRequest request)
    {
        var user = await DbContext.User.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (user == null)
            throw new ApiException("error_user_not_found");

        var referalUser = await DbContext.Referral.FirstOrDefaultAsync(f => f.ReferredTo.Equals(request.Id));

        var response = new UserGetbyIdResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailId = user.EmailId,
            MobileNumber = user.MobileNumber,
            BirthDate = user.BirthDate,
            Address = user.Address,
            Image = user.Image,
            Password = Crypto.DecryptPassword(user.Password),
            ConfirmPassword = Crypto.DecryptPassword(user.ConfirmPassword),
            Created = user.Created,
            Points = user.Points,
            State = user.State,
            City = user.City,
            ZipCode = (int)user.ZipCode,
            ReferedCode = referalUser?.ReferralCode,
            ReferedTo = referalUser != null ? await DbContext.Referral.Where(w => w.UserId.Equals(referalUser.ReferredTo)).Select(s => s.UserEntity.FirstName).ToListAsync() : null
        };

        return response;
    }

    #endregion 11. User Get By Id

    #region 12. Delete User

    public async Task<BaseResponse> DeleteUser([FromBody] BaseIdRequest request)
    {
        var user = DbContext.User.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (user == null)
            throw new ApiException("error_user_not_found");

        if (user.Active == true && user.Deleted == false)
        {
            throw new ApiException("error_user_active");
        }
        else
        {
            user.Deleted = true;
            user.Active = false;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 12. Delete User

    #region 12. User Product Request

    public async Task<BaseResponse> UserProductRequestAsync([FromBody] UserProductRequest request)
    {
        if (request == null)
            return new BaseResponse { IsSuccess = false };

        var productRequest = new ProductRequestEntity();
        Mapper.Map(request, productRequest);
        await DbContext.AddRangeAsync(productRequest);
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 12. User Product Request

    #region 13. User IsValid For SignUp

    public async Task<BaseResponse> IsValidUser(CheckUserValidRequest request)
    {
        // Check user exists or not
        var user = await DbContext.User.FirstOrDefaultAsync(x => x.EmailId.ToLower().Equals(request.EmaiId.ToLower()));

        if (user == null)
            throw new ApiException("error_account_not_found");

        user.EmailVerified = true;
        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 13. User IsValid For SignUp

    #region 14. User Status Update

    public async Task<BaseResponse> UserStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var user = await DbContext.User.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

        if (user == null)
            throw new ApiException("error_user_not_found");

        user.Active = request.Active;
        DbContext.Update(user);
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 14. User Status Update

    public async Task<UserSignInResponse> GetUserProfile(string UserId, string UniqueId)
    {
        var response = new UserSignInResponse
        {
            Id = UserId,
            UniqueId = UniqueId
        };

        return response;
    }

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}