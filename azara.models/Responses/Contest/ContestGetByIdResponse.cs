namespace azara.models.Responses.Contest;

public class ContestGetByIdResponse
{
    public string ContestName { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string ContestType { get; set; }

    public string Location { get; set; }

    public bool Active { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? StoreDropdownId { get; set; }

}