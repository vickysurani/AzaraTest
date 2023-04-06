namespace azara.admin.Models.Advertisement.Response;

public class DealListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<AdvertisementListData> Details { get; set; }
}

public class AdvertisementListData
{
    public string Id { get; set; }

    public int SrNo { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string URL { get; set; }

    public int Position { get; set; }

    public bool Active { get; set; }

    public DateTime Modified { get; set; }

    public bool isLocationFound { get; set; }

    public object Latitude { get; set; }

    public object Longitude { get; set; }
}