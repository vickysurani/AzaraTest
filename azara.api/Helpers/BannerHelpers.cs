using azara.models.Requests.Banner;
using azara.models.Responses.Banner;

namespace azara.api.Helpers
{
    public class BannerHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public BannerHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. Persist Banner

        public async Task<BaseResponse> BannerInsert([FromBody] BannerInsertUpdateRequest request)
        {
            await DbContext.AddRangeAsync(new BannerEntity
            {
                Id = Guid.Parse(request.Id),
                BannerImage1 = request.BannerImage1,
                BannerImage2 = request.BannerImage2,
                BannerImage3 = request.BannerImage3,
                Active = request.Status,
                ModifiedBy = request.ModifiedBy,
                Created = DateTime.UtcNow,
            });

            await DbContext.SaveChangesAsync();

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 1. Persist Banner

        #region 2. Banner Get By Id

        public async Task<BannerGetByIdResponse> BannerGetById([FromBody] BaseRequiredIdRequest request)
        {
            var banner = await DbContext.Banner.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (banner == null)
                throw new ApiException("error_banner_not_found");

            var response = new BannerGetByIdResponse
            {
                Id = banner.Id.ToString(),
                BannerImage1 = banner.BannerImage1,
                BannerImage2 = banner.BannerImage2,
                BannerImage3 = banner.BannerImage3,
                Status = banner.Active,
                Created = DateTime.UtcNow,
                ModifiedBy = banner.ModifiedBy,
            };

            return response;
        }

        #endregion 2. Banner Get By Id

        #region 3. Banner Get List

        public async Task<List<BannerGetListResponse>> BannerGetList([FromBody] BannerGetListRequest request)
        {
            var bannerList = (from B in DbContext.Banner
                              where
                              !B.Deleted

                              select new BannerGetListResponse
                              {
                                  Id = B.Id,
                                  BannerImage1 = B.BannerImage1,
                                  BannerImage2 = B.BannerImage2,
                                  BannerImage3 = B.BannerImage3,
                                  Created = B.Created,
                                  Modified = B.Modified,
                                  ModifiedBy = B.ModifiedBy,
                                  Active = B.Active,
                              }).OrderByDescending(x => x.Created).ToList();

            if (bannerList == null)
                throw new ApiException("error_banner_not_found");

            if (bannerList != null && bannerList.Any() && request.IsDisplayActive)
                bannerList = bannerList.Where(b => b.Active.Equals(true)).ToList();

            return bannerList;
        }

        #endregion 3. Banner Get List

        #region 4. Bannner Image Update 

        public async Task<BannerGetListResponse> BannerUpdateImage([FromBody] BannerInsertRequest request)
        {
            var BannerImage = DbContext.Banner.FirstOrDefault(x => x.Id.ToString().Equals(request.Id));


            if (BannerImage == null)
                throw new ApiException("error_Banner_not_found");

            BannerImage.BannerImage1 = request.BannerImage1;
            BannerImage.BannerImage2 = request.BannerImage2;
            BannerImage.BannerImage3 = request.BannerImage3;
            BannerImage.Modified = DateTime.UtcNow;

            var response = new BannerGetListResponse
            {
                BannerImage1 = BannerImage.BannerImage1,
                BannerImage2 = BannerImage.BannerImage2,
                BannerImage3 = BannerImage.BannerImage3,
                Modified = DateTime.UtcNow,

            };
            DbContext.SaveChanges();

            return response;
        }

        #endregion 4.  Bannner Image Update 

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose


    }
}
