namespace azara.models.Responses.Coupons;

public class CouponsGetByIdResponse
{
    public string CouponTitle { get; set; }

    public string? CouponImage { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public string? CouponCode { get; set; }

    public bool Active { get; set; }

    public int CouponsPoint { get; set; }
}