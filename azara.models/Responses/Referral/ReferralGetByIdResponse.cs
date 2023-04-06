namespace azara.models.Responses.Referral;

public class ReferralGetByIdResponse
{
    public string ReferralCode { get; set; }

    public Guid UserId { get; set; }

    public int Count { get; set; }

    public Guid? ReferredTo { get; set; }
}