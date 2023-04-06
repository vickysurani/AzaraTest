namespace azara.models.Responses.Contest;

public class ContestListResponse
{
    public Guid Id { get; set; }

    public string ContestName { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string ContestType { get; set; }

    public string Location { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public bool Active { get; set; }

    public DateTime Modified { get; set; }

    public bool IsLocationFound { get; set; } = false;

    public string? StoreDropdownId { get; set; }


}