namespace azara.api.Helpers;

public class ReferralHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public ReferralHelpers(
        AzaraContext DbContext,
        ICrypto Crypto)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
    }

    #endregion Constructor

    #region 1. Insert Referral

    public async Task<BaseResponse> ReferralInsert([FromBody] ReferralUpdateRequest request)
    {
        // Code for generate Referral
        var generator = new Random();
        var code = "REF" + generator.Next(0, 1000000).ToString("D6");

        await DbContext.AddRangeAsync(new ReferralEntity
        {
            ReferralCode = code,
            UserId = request.UserId == new Guid() ? null : request.UserId,
            Count = request.Count,
            ReferredTo = request.ReferredTo ?? Guid.Empty
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Referral

    #region 2. Update Referral

    public async Task<ReferralUpdateResponse> ReferralUpdate([FromBody] ReferralUpdateRequest request)
    {
        var referral = DbContext.Referral.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (referral == null)
            throw new ApiException("error_referral_not_found");

        referral.ReferralCode = request.ReferralCode;
        referral.UserId = request.UserId;
        referral.Count = request.Count;
        referral.ReferredTo = request.ReferredTo ?? referral.ReferredTo;

        var response = new ReferralUpdateResponse
        {
            ReferralCode = referral.ReferralCode,
            UserId = referral.UserId ?? new Guid(),
            Count = referral.Count,
            ReferredTo = referral.ReferredTo
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Referral

    #region 3. Referral Get By Id

    public async Task<ReferralGetByIdResponse> ReferralGetById([FromBody] BaseRequiredIdRequest request)
    {
        var referral = await DbContext.Referral.FirstOrDefaultAsync(x => x.UserId.Equals(request.Id));

        if (referral == null)
            throw new ApiException("error_referral_not_found");

        var response = new ReferralGetByIdResponse
        {
            ReferralCode = referral.ReferralCode,
            UserId = referral.UserId ?? new Guid(),
            Count = referral.Count,
            ReferredTo = referral.ReferredTo
        };

        return response;
    }

    #endregion 3. Referral Get By Id

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}