using azara.models.Requests.AboutUs;
using azara.models.Responses.AboutUs;

namespace azara.api.Helpers
{
    public class AboutUsHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public AboutUsHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. AboutUs Insert

        public async Task<BaseResponse> AboutUsInsert([FromBody] AboutUsInsertRequest request)
        {
            await DbContext.AddRangeAsync(new AboutUsEntity
            {
                Description = request.Description,
                Image = request.Image
            });

            await DbContext.SaveChangesAsync();

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 1. AboutUs Insert               

        #region 2. AboutUs Update
        public async Task<AboutUsUpdateResponse> AboutUsUpdate([FromBody] AboutUsUpdateRequest request)
        {
            var aboutus = DbContext.AboutUs.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString())) ?? throw new ApiException("error_AboutUs_not_found");

            aboutus.Description = request.Description ?? aboutus.Description;
            aboutus.Image = request.Image ?? aboutus.Image;

            var response = new AboutUsUpdateResponse
            {
                Description = aboutus.Description,
                Image = aboutus.Image,
            };

            DbContext.SaveChanges();

            return response;
        }
        #endregion

        #region 3. AboutUs Get List
        public async Task<List<AboutUsUpdateResponse>> AboutUsList([FromBody] AboutUsInsertRequest request)
        {
            var Aboutus = (from B in DbContext.AboutUs
                           where
                           !B.Deleted

                           select new AboutUsUpdateResponse
                           {
                               Description = B.Description,
                               Image = B.Image
                           }).ToList();

            if (Aboutus == null)
                throw new ApiException("error_AboutUs_not_found");

            return Aboutus;
        }

        #endregion

        #region 4. AboutUs Delete
        public async Task<BaseResponse> AboutUsDelete([FromBody] AboutUsDeleteRequest request)
        {
            var aboutus = DbContext.AboutUs.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString())) ?? throw new ApiException("error_AboutUs_not_found");

            if (aboutus.Deleted == false)
            {
                aboutus.Deleted = true;
            }

            aboutus.Active = false;

            DbContext.SaveChanges();

            return new BaseResponse { IsSuccess = true };
        }
        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
