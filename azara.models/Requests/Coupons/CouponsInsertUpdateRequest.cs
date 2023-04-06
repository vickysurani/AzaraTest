namespace azara.models.Requests.Coupons;

public class CouponsInsertUpdateRequest
{
    public string? Id { get; set; }

    public string CouponTitle { get; set; }

    public string? CouponImage { get; set; }

    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

    public string Description { get; set; }

    public string Amount { get; set; }

    public string? CouponCode { get; set; }

    public bool Active { get; set; }

    // public int CouponsPoint { get; set; }

}