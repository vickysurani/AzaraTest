using azara.models.Mapping;
using azara.models.Requests.Team;
using azara.models.Responses.Team;

namespace azara.api.Helpers
{
    public class TeamHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public TeamHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
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

        #region 1. Team Insert

        public async Task<BaseResponse> TeamInsert([FromBody] TeamInsertRequest request)
        {
            if (DbContext.Team.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

            request.PermissionJson = JsonConvert.SerializeObject(request.MenuList);

            await DbContext.AddRangeAsync(new TeamEntity
            {
                Name = request.Name,
                PermissionJson = request.PermissionJson,
                ModifiedBy = Guid.Parse(request.ModifiedBy),
                CurrentRevision = 1
            });

            await DbContext.SaveChangesAsync();

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 1. Team Insert

        #region 2. Team Update

        public async Task<BaseResponse> TeamUpdate([FromBody] TeamUpdateRequest request)
        {
            try
            {
                var team = DbContext.Team.FirstOrDefault(x => x.Id.ToString().Equals(request.Id));

                if (team == null)
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_team_not_found" });

                // Validation for duplicate Name
                if (DbContext.Team.Any(x => x.Name.ToLower().Equals(request.Name.ToLower()) && !x.Id.ToString().Equals(request.Id)))
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

                request.PermissionJson = JsonConvert.SerializeObject(request.MenuList);

                team.Name = request.Name ?? team.Name;
                team.PermissionJson = request.PermissionJson ?? team.PermissionJson;
                team.ModifiedBy = Guid.Parse(request.ModifiedBy);
                team.Modified = DateTime.UtcNow;
                team.CurrentRevision++;

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

        #endregion 2. Team Update

        #region 3. Team Get By Id

        public async Task<BaseResponse> TeamGetById([FromBody] BaseGetByIdRequest request)
        {
            var menuList = DbContext.Menu.ToList();
            var team = (
                from T in DbContext.Team
                join A in DbContext.Admin on T.ModifiedBy equals A.Id
                where T.Id.Equals(request.Id)



                //where T.Id.ToString().Equals(request.Id)

                select new TeamGetByIdResponse
                {
                    Id = T.Id,
                    Name = T.Name,
                    CurrentRevision = T.CurrentRevision,
                    PermissionJson = T.PermissionJson,
                    ModifiedBy = A.UserName
                }).FirstOrDefault();

            if (team == null)
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_team_not_found" });

            var teamMenuList = JsonConvert.DeserializeObject<ICollection<MenuEntity>>(team.PermissionJson).ToList();

            //team.MenuList = JsonConvert.DeserializeObject<ICollection<MenuResponse>>(team.PermissionJson).ToList();

            var defaultList = new MenuMap().Map(teamMenuList, menuList);

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            var total = teamMenuList.Count();
            var totalPages = TeamHelpers.CalculateTotalPages(total, request.PageSize);
            var teamPaginationList = teamMenuList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Id = team.Id,
                Name = team.Name,
                ModifiedBy = team.ModifiedBy,
                CurrentRevision = team.CurrentRevision,
                PermissionJson = team.PermissionJson,
                DefaultPermissionCsv = team.PermissionJson,
                MenuList = defaultList
            };

            var pageResponse = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                OffSet = request.PageNo,
                Details = teamPaginationList
            };

            return await Task.FromResult(new BaseResponse
            {
                IsSuccess = true,
                Data = JsonConvert.SerializeObject(response)
            });
        }

        #endregion 3. Team Get By Id

        #region 4. Team Delete
        public async Task<BaseResponse> TeamDelete([FromBody] BaseIdRequest request)
        {
            var team = DbContext.Team.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (team == null)
                throw new ApiException("error_team_not_found");

            if (team.Active == true && team.Deleted == false)
            {
                throw new ApiException("error_team_active");
            }
            else
            {
                team.Deleted = true;
            }

            DbContext.SaveChanges();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 4. Team Delete

        #region 5. Team Status Update
        public async Task<BaseResponse> TeamStatusUpdate([FromBody] BaseStatusUpdateRequest request)
        {
            var teamData = await DbContext.Team.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (teamData == null)
                throw new ApiException("error_team_not_found");

            teamData.Active = request.Active;
            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 5. Team Status Update

        #region 6. Team Get List

        public async Task<PaginationResponse> TeamGetList([FromBody] TeamGetListRequest request)
        {
            var teamList = await (
                from T in DbContext.Team

                where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                    || T.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                    && T.Deleted.Equals(request.IsDeleted)

                select new
                {
                    Id = T.Id,
                    Name = T.Name,
                    PermissionJson = T.PermissionJson,
                    CurrentRevision = T.CurrentRevision,
                    Modified = T.Modified,
                    Active = T.Active,
                    ModifiedBy = T.ModifiedBy,
                    TotalUser = DbContext.Admin.Select(x => x.Id).Distinct().Count()
                }).OrderByDescending(x => x.Modified).ToListAsync();

            if (teamList == null)
                throw new ApiException("error_team_not_found");

            string[] sort = request.SortBy.ToLower().Split("_");

            switch (sort[0])
            {
                case "name":

                    if (sort.Length > 1 && sort[1] == "desc")
                        teamList = teamList.OrderByDescending(x => x.Name).ToList();
                    else
                        teamList = teamList.OrderBy(x => x.Name).ToList();
                    break;

                case "totaluser":

                    if (sort.Length > 1 && sort[1] == "desc")
                        teamList = teamList.OrderByDescending(x => x.TotalUser).ToList();
                    else
                        teamList = teamList.OrderBy(x => x.TotalUser).ToList();
                    break;

                default:

                    teamList = teamList.OrderByDescending(x => x.Modified).ToList();
                    break;
            }

            if (teamList != null && teamList.Any() && request.IsDisplayActive)
                teamList = teamList.Where(pc => pc.Active.Equals(true)).ToList();

            var total = teamList.Count();
            var totalPages = TeamHelpers.CalculateTotalPages(total, request.PageSize);
            var eventPaginationList = teamList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

        #endregion 6. Team Get List

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
