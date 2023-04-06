namespace azara.client.Models.Deals.Response;

public class DealsListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<DealsData> Details { get; set; }
}

public class DealsData
{
    public string Id { get; set; }

    public string DealTitle { get; set; }

    public string DealBannerImage { get; set; }

    public int Position { get; set; }

    public string Active { get; set; }

    public string Url { get; set; }

    public string Description { get; set; }

    public string storeId { get; set; }

    public bool IsLocationFound { get; set; }
}