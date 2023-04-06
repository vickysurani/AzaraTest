using azara.models.Requests.PrivacyPolicy;
using azara.models.Responses.PrivacyPolicy;

namespace azara.api.Helpers
{
    public class PrivacyPolicyHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public PrivacyPolicyHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. Insert PrivacyPolicy

        public async Task<BaseResponse> PrivacyPolicyInsert([FromBody] PrivacyPolicyInsertRequest request)
        {
            await DbContext.AddRangeAsync(new PrivacyPolicyEntity
            {
                Description = request.Description,
                Created = DateTime.UtcNow,
            }); ;

            await DbContext.SaveChangesAsync();

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 1. Insert PrivacyPolicy

        #region 2. Update PrivacyPolicy

        public async Task<PrivacyPolicyUpdateResponse> PrivacyPolicyUpdate([FromBody] PrivacyPolicyInsertRequest request)
        {
            var pp = DbContext.PrivacyPolicy.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (pp == null)
                throw new ApiException("error_privacy_policy_not_found");

            pp.Description = request.Description ?? pp.Description;
            pp.Created = DateTime.UtcNow;

            var response = new PrivacyPolicyUpdateResponse
            {
                Descriptions = pp.Description,
                Created = pp.Created
            };

            DbContext.SaveChanges();

            return response;
        }

        #endregion 2. Update PrivacyPolicy

        #region 3. PrivacyPolicy List Without Pagination

        public async Task<List<PrivacyPolicyListResponse>> PrivacyPolicyGetList([FromBody] PrivacyPolicyGetListRequest request)
        {
            var PrivacyPolicyList = (from P in DbContext.PrivacyPolicy where

                            !P.Deleted
                            select new PrivacyPolicyListResponse
                            {
                                Id = P.Id,
                                Description = P.Description,
                                Created = P.Created,
                                Modified = P.Modified
                            }).OrderByDescending(x => x.Created).ToList();

            if (PrivacyPolicyList == null)
                throw new ApiException("error_privacy_policy_not_found");

            return PrivacyPolicyList;
        }

        #endregion 3. PrivacyPolicy List Without Pagination

        #region 4. Delete PrivacyPolicy

        public async Task<BaseResponse> PrivacyPolicyDelete([FromBody] BaseIdRequest request)
        {
            var pp = DbContext.PrivacyPolicy.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (pp == null)
                throw new ApiException("error_privacy_policy_not_found");

            if (pp.Deleted == false)
            {
                pp.Deleted = true;
            }

            pp.Active = false;

            DbContext.SaveChanges();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 4. Delete PrivacyPolicy

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
