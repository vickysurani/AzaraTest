namespace azara.models.Requests.Store;

public class StoreInsertRequest : BaseIdRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    public List<string>? ImageList { get; set; }

    public string? Image { get; set; }

    public string? Location { get; set; }

    [Required(ErrorMessage = "error_address_required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "error_email_required")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
    public string EmailId { get; set; }

    [Required]
    [RegularExpression(@"[0-9]\d{9}$", ErrorMessage = "error_invalid_Mobile")]
    public string ContactNumber { get; set; }

    public string Description { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? Created { get; set; }

}