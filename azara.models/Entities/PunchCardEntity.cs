namespace azara.models.Entities;

public class PunchCardEntity : BaseEntity
{
    public string PunchCardName { get; set; }


    public string? Image { get; set; }


    public string? PromoCode { get; set; }


    public string Description { get; set; }


    public string PunchCardPromos { get; set; }


    public DateTime? ExpiryDate { get; set; }
}