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
        public async Task <AboutUsUpdateResponse> AboutUsList([FromBody] AboutUsGetRequest request)
        {
            var Aboutus = (from B in DbContext.AboutUs
                           where
                           !B.Deleted

                           select new AboutUsUpdateResponse
                           {
                               id = B.Id,
                               Description = B.Description,
                               Image = B.Image,
                           }).FirstOrDefault();

            if (Aboutus == null)
                throw new ApiException("error_AboutUs_not_found");

            return Aboutus;
        }

        #endregion

        #region 5. About Us Get By Id
        public async Task<AboutUsGetByIDResponse> AboutusGetById([FromBody] BaseIdRequest request)
        {
            var aboutus = await DbContext.AboutUs.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (aboutus == null)
                throw new ApiException("error_aboutus_not_found");

            var response = new AboutUsGetByIDResponse
            {
                Image = aboutus.Image ?? aboutus.Image, 
                Description = aboutus.Description,
            };

            return response;
        }

        #endregion

        #region 4. AboutUs Delete
        public async Task<BaseResponse> AboutUsDelete([FromBody] BaseIdRequest request)
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
