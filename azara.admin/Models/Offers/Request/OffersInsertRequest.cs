namespace azara.admin.Models.Offers.Request;

public class OffersInsertRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_offer_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Name { get; set; }

    [Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Image { get; set; }

    [Required(ErrorMessageResourceName = "error_description_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Description { get; set; }

    [Required(ErrorMessageResourceName = "error_amount_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [Range(0.00, 100.00)]
    public string Amount { get; set; }

    public bool Active { get; set; }

    public List<string>? ImageList { get; set; }

}