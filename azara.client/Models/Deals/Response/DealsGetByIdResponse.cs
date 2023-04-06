namespace azara.client.Models.Deals.Response;

public class DealsGetByIdResponse
{
    public string DealTitle { get; set; }

    public string DealBannerImage { get; set; }

    public string Description { get; set; }

    public string StoreId { get; set; }

    public int Position { get; set; }

    public string Url { get; set; }

    public List<StoreDetailsItem> StoreDetails { get; set; }
}

public class StoreDetailsItem
{
    public string StoreId { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public string Name { get; set; }

    public bool IsNearestLocation { get; set; }
}