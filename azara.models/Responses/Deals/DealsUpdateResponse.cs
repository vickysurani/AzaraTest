namespace azara.models.Responses.Deals;

public class DealsUpdateResponse
{
    public string Title { get; set; }

    public string BannerImage { get; set; }

    public string Description { get; set; }

    public string StoreId { get; set; }

    public int Position { get; set; }

    public string URL { get; set; }

    public List<StoreDetail> StoreDetails { get; set; }
}

public class StoreDetail
{
    public Guid StoreId { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public string Name { get; set; }

    public bool IsNearestLocation { get; set; }
}