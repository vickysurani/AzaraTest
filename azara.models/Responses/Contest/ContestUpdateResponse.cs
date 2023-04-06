namespace azara.models.Responses.Contest;

public class ContestUpdateResponse
{
    [Required]
    public string ContestName { get; set; }

    [Required]
    public string Image { get; set; }

    public string Description { get; set; }

    [Required]
    public string ContestType { get; set; }

    public string Location { get; set; }

    [Required]
    public bool Active { get; set; }

    public string? StoreDropdownId { get; set; }

}