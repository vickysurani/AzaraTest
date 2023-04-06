namespace azara.admin.Models.Account.Request;

public class AdminEditRequestResponse
{
    public string Id { get; set; }

    public string ProfilePhoto { get; set; }

    [Required(ErrorMessageResourceName = "error_username_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "enter_valid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EmailId { get; set; }

    public string UniqueId { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    public object? Created { get; set; }

    public object? Modified { get; set; }

    public List<string>? ImageList { get; set; }

}