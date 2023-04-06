namespace azara.models.Responses.Deals;

public class DealsListResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string BannerImage { get; set; }

    public string URL { get; set; }

    public int Position { get; set; }

    public bool Active { get; set; }

    public DateTime Modified { get; set; }

    public bool IsLocationFound { get; set; } = false;

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}