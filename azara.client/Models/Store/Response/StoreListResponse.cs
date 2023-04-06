namespace azara.client.Models.Store.Response;

public class StoreListResponseData
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Location { get; set; }

    public string Address { get; set; }

    public bool IsLocationFound { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}

public class StoreListResponse
{

    public int Total { get; set; }
    public int TotalPages { get; set; }
    public int OffSet { get; set; }
    public List<StoreListResponseData> Details { get; set; }
}