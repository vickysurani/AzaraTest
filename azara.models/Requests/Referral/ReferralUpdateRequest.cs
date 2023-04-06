namespace azara.models.Requests.Referral;

public class ReferralUpdateRequest : BaseIdRequest
{
    public string ReferralCode { get; set; }

    public Guid UserId { get; set; }

    public int Count { get; set; }

    public Guid? ReferredTo { get; set; }
}