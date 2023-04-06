using azara.models.Requests.PointManagement;
using azara.models.Responses.PointManagement;

namespace azara.api.Helpers
{
    public class PointManagementHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public PointManagementHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. Insert Points

        public async Task<BaseResponse> PointsInsert([FromBody] PointManagementInsertUpdateRequest request)
        {
            if (DbContext.PointManagement.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

            await DbContext.AddRangeAsync(new PointManagementEntity
            {
                Name = request.Name,
                Point = request.Point,
            });

            await DbContext.SaveChangesAsync();

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 1. Insert Points

        #region 2. Update Points

        public async Task<PointManagementUpdateResponse> PointsUpdate([FromBody] PointManagementInsertUpdateRequest request)
        {
            var point = DbContext.PointManagement.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (point == null)
                throw new ApiException("error_points_not_found");

            point.Name = request.Name;
            point.Point = request.Point;

            var response = new PointManagementUpdateResponse
            {
                Name = point.Name,
                Point = point.Point,
            };

            DbContext.SaveChanges();

            return response;
        }

        #endregion 2. Update Points

        #region 3. Points Get By Id
        public async Task<PointManagementUpdateResponse> PointsGetById([FromBody] BaseIdRequest request)
        {
            var pointData = await DbContext.PointManagement.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (pointData == null)
                throw new ApiException("error_points_not_found");

            var response = new PointManagementUpdateResponse
            {
                Name = pointData.Name,
                Point = pointData.Point,
            };

            return response;
        }

        #endregion 3. Points Get By Id

        #region 4. Get Points List With Pagination

        public async Task<PaginationResponse> PointsGetList([FromBody] PointManagementGetListRequest request)
        {
            var pointList = await (from P in DbContext.PointManagement

                                   where (string.IsNullOrWhiteSpace(request.Name.ToLower())
                                   || P.Name.ToLower().Contains(request.Name.ToLower()))

                                   && P.Deleted.Equals(request.IsDeleted)

                                   select new
                                   {
                                       Id = P.Id,
                                       Name = P.Name,
                                       Point = P.Point,
                                       Modified = P.Modified,
                                       Created = P.Created
                                   }).OrderByDescending(x => x.Modified).ToListAsync();

            if (pointList == null)
                throw new ApiException("error_points_not_found");

            var total = pointList.Count();
            var totalPages = PointsHelpers.CalculateTotalPages(total, request.PageSize);
            var eventPaginationList = pointList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

        #endregion 4. Get Points List With Pagination

        #region 5. Points List Without Pagination

        public async Task<List<PointManagementListResponse>> PointsList([FromBody] PointManagementListRequest request)
        {
            var pointList = (from P in DbContext.PointManagement

                             where (string.IsNullOrWhiteSpace(request.Name.ToLower())
                             || P.Name.ToLower().Contains(request.Name.ToLower()))

                             && P.Deleted.Equals(request.IsDeleted)

                             select new PointManagementListResponse
                             {
                                 Name = P.Name,
                                 Point = P.Point,
                                 Modified = P.Modified,
                                 Created = P.Created
                             }).OrderByDescending(x => x.Created).ToList();

            if (pointList == null)
                throw new ApiException("error_points_not_found");

            return pointList;
        }

        #endregion 5. Points List Without Pagination

        #region 6. Delete Points

        public async Task<BaseResponse> PointsDelete([FromBody] BaseIdRequest request)
        {
            var point = DbContext.PointManagement.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (point == null)
                throw new ApiException("error_points_not_found");

            if (point.Deleted == false)
            {
                point.Deleted = true;
            }

            point.Active = false;

            DbContext.SaveChanges();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 6. Delete Points

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
