namespace azara.client.Models.Coupons.Response;

public class CouponsListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<CouponsData> Details { get; set; }
}

public class CouponsData
{
    public string Id { get; set; }

    public string CouponTitle { get; set; }

    public string CouponImage { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public string CouponCode { get; set; }

    public bool IsUsed { get; set; }
    public DateTime Modified { get; set; }

    public int CouponsPoint { get; set; }
}