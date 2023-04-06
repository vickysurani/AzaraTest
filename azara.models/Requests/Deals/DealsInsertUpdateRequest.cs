namespace azara.models.Requests.Deals;

public class DealsInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_title_required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "error_image_required"), MaxLength(100)]
    [Display(Name = "Image")]
    public string BannerImage { get; set; }

    [Required(ErrorMessage = "error_url_required")]
    public string URL { get; set; }

    [Required(ErrorMessage = "error_position_required")]
    [Range(0.00, 50.00, ErrorMessage = "Please enter a position between 0 and 50")]
    public string Position { get; set; }

    [Required(ErrorMessage = "error_description_required")]
    public string Description { get; set; }

    public string? StoreId { get; set; }
}