namespace azara.client.Models.Contest;

public class ContestResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<ContestData> Details { get; set; }
}

public class ContestData
{
    public string Id { get; set; }

    public string ContestName { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string ContestType { get; set; }

    public string Location { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public bool IsLocationFound { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}