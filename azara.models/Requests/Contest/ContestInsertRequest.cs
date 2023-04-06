namespace azara.models.Requests.Contest;

public class ContestInsertRequest : BaseIdRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    [StringLength(100, ErrorMessage = "Contest name max length is 100", MinimumLength = 0)]
    public string ContestName { get; set; }

    [Required(ErrorMessage = "error_image_required")]
    public string Image { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "error_type_required")]
    public string ContestType { get; set; }

    public string Location { get; set; }

    [Required]
    public bool Active { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? StoreDropdownId { get; set; }

}