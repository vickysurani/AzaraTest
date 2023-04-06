using azara.models.Mapping;
using azara.models.Requests.SubAdmin;
using azara.models.Responses.SubAdmins;

namespace azara.api.Helpers
{
    public class SubAdminHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        IMapper Mapper { get; set; }

        public SubAdminHelpers(
            AzaraContext DbContext,
            ICrypto Crypto,
            IMapper Mapper = null)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
            this.Mapper = Mapper;
        }

        #endregion Constructor

        #region 1. Sub Admin Add

        public async Task<BaseResponse> SubAdminAdd([FromBody] SubAdminAddRequest request)
        {
            var roleId = DbContext.MasterRoles.FirstOrDefault(x => x.Name.Equals(MasterRoleConst.Subadmin))?.Id;

            if (roleId.GetValueOrDefault() == Guid.Empty)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_role_not_found" });

            // Validation for duplicate Name
            if (DbContext.Admin.Any(x => x.UserName.ToLower().Equals(request.UserName.ToLower())))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_username_exist" });

            // Validation for duplicate Email
            if (DbContext.Admin.Any(x => x.EmailId.ToLower().Equals(request.EmailId.ToLower())))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_email_exist" });

            // Validation for duplicate Mobile
            if (DbContext.Admin.Any(x => x.Mobile.Equals(request.Mobile)))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_mobile_exist" });

            //var password = Crypto.EncryptPassword($"{request.UserName.ToLower()}@123");

            request.PermissionJson = JsonConvert.SerializeObject(request.MenuList);

            var password = Crypto.EncryptPassword(request.Password);

            DbContext.Add(new AdminEntity
            {
                UserName = request.UserName.ToLower(),
                Name = request.Name,
                EmailId = request.EmailId,
                Mobile = request.Mobile,
                Password = password,
                RoleId = roleId.GetValueOrDefault(),
                CreatedBy = Guid.Parse(request.ModifiedBy),
                CurrentRevisionNumber = 1,
                PermissionJson = request.PermissionJson,
                IsSubAdmin = true,
                //TeamId = request.TeamId
            });

            await DbContext.SaveChangesAsync();

            var response = await DbContext.Admin.Where(w => w.EmailId.Equals(request.EmailId) && !w.Deleted && w.Active).FirstOrDefaultAsync();

            var dbResponse = new
            {
                Id = response.Id,
                Password = $"{request.UserName.ToLower()}@123"
            };

            return await Task.FromResult(new BaseResponse
            {
                IsSuccess = true,
                Data = System.Text.Json.JsonSerializer.Serialize(dbResponse)
            });
        }

        #endregion 1. Sub Admin Add

        #region 2. Get SubAdmin List

        public async Task<PaginationResponse> SubAdminGetListAsync(SubAdminListRequest request)
        {
            var roleId = DbContext.MasterRoles.FirstOrDefault(x => x.Name.Equals(MasterRoleConst.Subadmin))?.Id;

            var adminList = (
                from a in DbContext.Admin
                where (string.IsNullOrWhiteSpace(request.Name.ToString())
                || a.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                && (string.IsNullOrEmpty(request.SearchParam) 
                || a.Name.Contains(request.SearchParam))

                && (string.IsNullOrWhiteSpace(request.UserName.ToString())
                || a.UserName.ToLower().Contains(request.UserName.ToString().ToLower()))

                && (string.IsNullOrWhiteSpace(request.EmailId.ToString())
                || a.EmailId.ToLower().Contains(request.EmailId.ToString().ToLower()))

                && (string.IsNullOrWhiteSpace(request.Mobile.ToString())
                || a.Mobile.Contains(request.Mobile.ToString()))

                && a.Deleted.Equals(request.IsDeleted)

                && a.Id.ToString() != request.AdminId

                select new SubAdminDetailResponse
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    EmailId = a.EmailId,
                    Mobile = a.Mobile,
                    UserName = a.UserName,
                    Active = a.Active,
                    Deleted = a.Deleted,
                    Modified = a.Modified
                });


            if (adminList == null)
                throw new ApiException("error_team_not_found");

            string[] sort = request.SortBy.ToLower().Split("_");

            switch (sort[0])
            {
                case "name":

                    if (sort.Length > 1 && sort[1] == "desc")
                        adminList = adminList.OrderByDescending(x => x.Name);
                    else
                        adminList = adminList.OrderBy(x => x.Name);
                    break;

                case "email":

                    if (sort.Length > 1 && sort[1] == "desc")
                        adminList = adminList.OrderByDescending(x => x.EmailId);
                    else
                        adminList = adminList.OrderBy(x => x.EmailId);
                    break;

                case "username":

                    if (sort.Length > 1 && sort[1] == "desc")
                        adminList = adminList.OrderByDescending(x => x.UserName);
                    else
                        adminList = adminList.OrderBy(x => x.UserName);
                    break;

                default:

                    adminList = adminList.OrderByDescending(x => x.Modified);
                    break;
            }


            var total = adminList.Count();
            var totalPages = UserHelpers.CalculateTotalPages(total, request.PageSize);
            var adminPaginationList = adminList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = adminPaginationList,
            };

            return new PaginationResponse
            {
                Total = response.Total,
                TotalPages = response.Total,
                OffSet = response.Offset,
                Details = response.Details
            };
        }
        #endregion 2. Get SubAdmin List

        #region 3. Sub Admin Update

        public async Task<BaseResponse> SubAdminUpdate([FromBody] SubAdminUpdateRequest request)
        {
            var admin = DbContext.Admin.FirstOrDefault(x => x.Id.Equals(request.Id));

            request.PermissionJson = JsonConvert.SerializeObject(request.MenuList);

            if (admin == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_sub_admin_not_found" });

            admin.UserName = request.UserName ?? admin.UserName;
            admin.Name = request.Name ?? admin.Name;
            
            admin.EmailId = request.EmailId ?? admin.EmailId;
            admin.Password = Crypto.EncryptPassword(request.Password);
            admin.Mobile = request.Mobile ?? admin.Mobile;
            admin.RoleId = Guid.Parse(request.RoleId);
            admin.ModifiedBy = Guid.Parse(request.ModifiedBy);
            admin.Modified = DateTime.UtcNow;
            admin.PermissionJson = JsonConvert.SerializeObject(request.MenuList);
            admin.CurrentRevisionNumber++;

            if (request.RoleId.Any())
            {
                // Remove token for logout from all device
                var tokenList = DbContext.AdminAccessToken.Where(x => x.AdminId.Equals(admin.Id) && x.Purpose.Equals("Login"));
                DbContext.AdminAccessToken.RemoveRange(tokenList);
            }

            DbContext.SaveChanges();

            return await Task.FromResult(new BaseResponse { IsSuccess = true });
        }

        #endregion 3. Sub Admin Update

        #region 4. SubAdmin Get By Id
        public async Task<BaseResponse> SubAdminById([FromBody] BaseGetByIdRequest request)
        {
            // get default menus
            var menuList = DbContext.Menu.ToList();

            var subAdmin = (
                from a in DbContext.Admin
                where
                a.Id == request.Id
                select new SubAdminGetByIdResponse
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    EmailId = a.EmailId,
                    Mobile = a.Mobile,
                    UserName = a.UserName,
                    Active = a.Active,
                    RoleId = a.RoleId.ToString(),
                    PermissionJson = a.PermissionJson,
                    Password = Crypto.DecryptPassword(a.Password)
                }).FirstOrDefault();

            if (subAdmin == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_sub_admin_not_found" });

            var subAdminMenuList = JsonConvert.DeserializeObject<ICollection<MenuEntity>>(subAdmin.PermissionJson).ToList();

            var defaultMenuList = new MenuMap().Map(subAdminMenuList.ToList(), menuList.ToList());

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            var total = subAdminMenuList.Count();
            var totalPages = UserHelpers.CalculateTotalPages(total, request.PageSize);
            var sunAdminPaginationList = subAdminMenuList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Id = subAdmin.Id,
                Name = subAdmin.Name,
                EmailId = subAdmin.EmailId,
                PermissionJson = subAdmin.PermissionJson,
                Mobile = subAdmin.Mobile,
                UserName = subAdmin.UserName,
                Active = subAdmin.Active,
                RoleId = subAdmin.RoleId,
                MenuList = defaultMenuList,
                Password = subAdmin.Password,
                DefaultPermissionCsv = "Add,Update,View,Delete,Status,Export"
            };

            var pageResponse = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                OffSet = request.PageNo,
                Details = sunAdminPaginationList
            };

            return await Task.FromResult(new BaseResponse
            {
                IsSuccess = true,
                Data = JsonConvert.SerializeObject(response)
            });

        }

        #endregion 4. SubAdmin Get By Id

        #region 5. SubAdmin Delete

        public async Task<BaseResponse> SubAdminDelete([FromBody] SubAdminDeleteRequest request)
        {
            var admin = DbContext.Admin.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (admin == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_sub_admin_not_found" });

            admin.Active = false;
            admin.Deleted = true;
            admin.CurrentRevisionNumber++;

            // Remove token for logout from all device
            var tokenList = DbContext.AdminAccessToken.Where(x => x.AdminId.Equals(admin.Id) && x.Purpose.Equals("Login"));
            DbContext.AdminAccessToken.RemoveRange(tokenList);

            DbContext.SaveChanges();

            return await Task.FromResult(new BaseResponse { IsSuccess = true }); ;
        }

        #endregion 5. SubAdmin Delete

        #region 6. SubAdmin Status Update

        public async Task<BaseResponse> SubAdminStatusUpdate([FromBody] SubAdminStatusUpdateRequest request)
        {
            var admin = DbContext.Admin.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (admin == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_sub_admin_not_found" });

            admin.Active = request.Active;
            admin.Deleted = false;
            admin.CurrentRevisionNumber++;

            // Remove token for logout from all device
            var tokenList = DbContext.AdminAccessToken.Where(x => x.AdminId.Equals(admin.Id) && x.Purpose.Equals("Login"));
            DbContext.AdminAccessToken.RemoveRange(tokenList);

            DbContext.SaveChanges();

            return await Task.FromResult(new BaseResponse { IsSuccess = true }); ;

        }

        #endregion 6. SubAdmin Status Update

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
