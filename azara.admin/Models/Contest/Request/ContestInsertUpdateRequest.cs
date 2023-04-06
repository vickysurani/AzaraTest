namespace azara.admin.Models.Contest.Request;

public class ContestInsertUpdateRequest : BaseIdRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_contest_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(100, ErrorMessage = "Contest name max length is 100", MinimumLength = 0)]
    public string ContestName { get; set; }

    //[Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Image { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessageResourceName = "error_contest_type_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string ContestType { get; set; }

    [Required(ErrorMessageResourceName = "error_contest_location_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Location { get; set; }

    [Required]
    public bool Active { get; set; }

    public List<string>? ImageList { get; set; }

    public string? StoreDropdownId { get; set; }


}