namespace azara.admin.Models.Store.Request;

public class StoreInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_store_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Name { get; set; }

    public List<string>? ImageList { get; set; }

    public string? Image { get; set; }

    public string? Location { get; set; }

    [Required(ErrorMessageResourceName = "error_address_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Address { get; set; }

    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EmailId { get; set; }

    [Required(ErrorMessageResourceName = "error_mobile_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"[0-9]\d{9}$", ErrorMessageResourceName = "error_invalid_Mobile", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string ContactNumber { get; set; }

    public string Description { get; set; }


}