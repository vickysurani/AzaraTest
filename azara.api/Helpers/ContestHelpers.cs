using azara.models.Requests.GoogleMap;
using Microsoft.JSInterop;

namespace azara.api.Helpers;

public class ContestHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public ContestHelpers(
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

    #region 1. Insert Contest

    public async Task<BaseResponse> ContestInsert([FromBody] ContestInsertRequest request)
    {
        if (DbContext.Contests.Any(x => x.ContestName.ToLower().Equals(request.ContestName.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_contest_name_exist" });

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            var geoLocationData = await new StoreHelpers(DbContext, Crypto).GeoLocation(new GeoLocationRequest
            {
                Address = request.Location
            });
            if (geoLocationData != null)
            {
                request.Location = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
            else
            {
                throw new ApiException("Please enter valid location");
            }
        }
       

        await DbContext.AddRangeAsync(new ContestsEntity
        {
            ContestName = request.ContestName,
            Image = request.Image,
            Description = request.Description,
            ContestType = request.ContestType,
            Location = request.Location,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            StoreDropdownId = request.StoreDropdownId,
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Contest

    #region 2. Update Contest

    public async Task<ContestUpdateResponse> ContestUpdate([FromBody] ContestUpdateRequest request)
    {
        var contest = DbContext.Contests.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (contest == null)
            throw new ApiException("error_contest_not_found");

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            //var httpResult = await new HttpClient().GetAsync($"https://search.map.santolibre.net/api/v1/search/{request.Address}");
            //var result = await httpResult.Content.ReadAsStringAsync();
            //GeoLocationDetail geoLocationData = JsonConvert.DeserializeObject<GeoLocationDetail>(result);
            //if (geoLocationData != null && geoLocationData.locations != null && geoLocationData.locations.Count > 0)
            //{
            //    request.Location = geoLocationData.locations.FirstOrDefault().name;
            //    request.Latitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lat.ToString();
            //    request.Longitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lng.ToString();
            //}

            var geoLocationData = await new StoreHelpers(DbContext, Crypto).GeoLocation(new GeoLocationRequest
            {
                Address = request.Location
            });
            if (geoLocationData != null)
            {
                request.Location = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
            else
            {
                throw new ApiException("Please enter valid location");
            }
        }

        contest.ContestName = request.ContestName ?? contest.ContestName;
        contest.Image = request.Image ?? contest.Image;
        contest.Description = request.Description ?? contest.Description;
        contest.ContestType = request.ContestType ?? contest.ContestType;
        contest.Location = request.Location ?? contest.Location;
        contest.Active = request.Active;
        contest.Latitude = request.Latitude ?? contest.Latitude;
        contest.Longitude = request.Longitude ?? contest.Longitude;
        contest.Modified = DateTime.UtcNow;
        contest.StoreDropdownId = request.StoreDropdownId;
        var response = new ContestUpdateResponse
        {
            ContestName = contest.ContestName,
            Image = contest.Image,
            Description = contest.Description,
            ContestType = contest.ContestType,
            Location = contest.Location,
            Active = contest.Active,
            StoreDropdownId = contest.StoreDropdownId
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Contest

    #region 3. Contest Get By Id

    public async Task<ContestGetByIdResponse> ContestGetById([FromBody] BaseRequiredIdRequest request)
    {
        var contest = await DbContext.Contests.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (contest == null)
            throw new ApiException("error_contest_not_found");

        var response = new ContestGetByIdResponse
        {
            ContestName = contest.ContestName,
            Image = contest.Image,
            Description = contest.Description,
            ContestType = contest.ContestType,
            Location = contest.Location,
            Active = contest.Active,
            Latitude = contest.Latitude,
            Longitude = contest.Longitude,
            StoreDropdownId = contest.StoreDropdownId,

        };

        return response;
    }

    #endregion 3. Contest Get By Id

    #region 4. Get Contest List

    public async Task<PaginationResponse> ContestGetListAsync([FromBody] ContestGetListRequest request)
    {
        var contestList = await (
            from C in DbContext.Contests

            where (string.IsNullOrWhiteSpace(request.ContestName.ToString().ToLower())
                || C.ContestName.ToLower().Contains(request.ContestName.ToLower()))

                && C.Deleted.Equals(request.IsDeleted)

            select new ContestListResponse
            {
                Id = C.Id,
                ContestName = C.ContestName,
                Image = C.Image,
                Description = C.Description,
                ContestType = C.ContestType,
                Location = C.Location,
                Active = C.Active,
                Latitude = C.Latitude,
                Longitude = C.Longitude,
                Modified = C.Modified,
                StoreDropdownId = C.StoreDropdownId,
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in contestList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (contestList == null)
            throw new ApiException("error_contest_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    contestList = contestList.OrderByDescending(x => x.ContestName).ToList();
                else
                    contestList = contestList.OrderBy(x => x.ContestName).ToList();
                break;

            default:

                contestList = contestList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (contestList != null && contestList.Any() && request.IsDisplayActive)
            contestList = contestList.Where(pc => pc.Active.Equals(true)).ToList();

        var total = contestList.Count();
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var contestPaginationList = contestList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = contestPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 4. Get Contest List

    #region 5. Contest List Without Pagination

    public async Task<List<ContestListResponse>> ContestList([FromBody] ContestListRequest request)
    {
        var contestList = await (
            from C in DbContext.Contests

            where (string.IsNullOrWhiteSpace(request.ContestName.ToString().ToLower())
                || C.ContestName.ToLower().Contains(request.ContestName.ToLower()))

                && C.Deleted.Equals(request.IsDeleted)

            select new ContestListResponse
            {
                Id = C.Id,
                ContestName = C.ContestName,
                Image = C.Image,
                Description = C.Description,
                ContestType = C.ContestType,
                Location = C.Location,
                Active = C.Active,
                Latitude = C.Latitude,
                Longitude = C.Longitude,
                Modified = C.Modified,
                StoreDropdownId = C.StoreDropdownId,
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in contestList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (contestList == null)
            throw new ApiException("error_contest_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    contestList = contestList.OrderByDescending(x => x.ContestName).ToList();
                else
                    contestList = contestList.OrderBy(x => x.ContestName).ToList();
                break;

            default:

                contestList = contestList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (contestList != null && contestList.Any() && request.IsDisplayActive)
            contestList = contestList.Where(pc => pc.Active.Equals(true)).ToList();

        return contestList;
    }

    #endregion 5. Contest List Without Pagination

    #region 6. Delete Contest

    public async Task<BaseResponse> ContestDelete([FromBody] BaseIdRequest request)
    {
        var contest = DbContext.Contests.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (contest == null)
            throw new ApiException("error_contest_not_found");

        if (contest.Active == true && contest.Deleted == false)
        {
            throw new ApiException("error_contest_active");
        }
        else
        {
            contest.Deleted = true;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Contest

    #region 7. Contest Status Update

    public async Task<BaseResponse> ContestStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var contestData = await DbContext.Contests.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (contestData == null)
            throw new ApiException("error_contest_not_found");

        contestData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Contest Status Update

    #region 8. Contest Insert As PerUserId
    public async Task<BaseResponse> UserContestInsert([FromBody] ContestUserInsertRequest request)
    {
        if (DbContext.UserContests.Any(x => x.ContestId.Equals(request.ContestId) && x.UserId.Equals(request.UserId)))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "event_alredy_assigned" });



        await DbContext.AddRangeAsync(new UserContestEntity
        {
            ContestId = request.ContestId,
            UserId = request.UserId ?? new Guid(),
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }
    #endregion

    #region 9. Contest Details Get By UserId

    public async Task<List<InsertUpdateContestResponse>> ContestDetailsGetByUserId([FromBody] BaseIdRequest request)
    {
        var userContestList = await (from U in DbContext.UserContests
                                     join E in DbContext.Contests on U.ContestId equals E.Id
                                     where (U.UserId.Equals(request.Id))

                                     select new InsertUpdateContestResponse
                                     {
                                         Id = E.Id,
                                         ContestName = E.ContestName,
                                         Image = E.Image,
                                         Description = E.Description,
                                         Active = E.Active,
                                         ContestType = E.ContestType,
                                         Latitude = E.Latitude,
                                         Longitude = E.Longitude

                                     }).ToListAsync();

        return userContestList;

    }

    #endregion

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}