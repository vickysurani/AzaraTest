namespace azara.admin.Models.Coupons.Response;

public class CouponsListResponse
{
    public int Total { get; set; }

    public int Totalpages { get; set; }

    public int Offset { get; set; }

    public List<CouponsData> Details { get; set; }
}

public class CouponsData
{
    public int SrNo { get; set; }

    public string id { get; set; }

    public string couponTitle { get; set; }

    public string couponImage { get; set; }
    public int couponspoint { get; set; }

    public DateTime expiryDate { get; set; }

    public string description { get; set; }

    public decimal amount { get; set; }

    public string couponCode { get; set; }

    public DateTime modified { get; set; }

    public bool active { get; set; }
}

