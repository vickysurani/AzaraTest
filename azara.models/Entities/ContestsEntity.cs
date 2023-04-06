namespace azara.models.Entities;

public class ContestsEntity : BaseEntity
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
    public string Latitude { get; set; }

    [Required]
    public string Longitude { get; set; }

    [Required]
    public bool IsOnline { get; set; } = false;

    public string? StoreDropdownId { get; set; }

}