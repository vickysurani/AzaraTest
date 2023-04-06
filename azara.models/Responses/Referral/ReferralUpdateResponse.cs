namespace azara.models.Responses.Referral;

public class ReferralUpdateResponse
{
    public string ReferralCode { get; set; }

    public Guid UserId { get; set; }

    public int Count { get; set; }

    public Guid? ReferredTo { get; set; }
}