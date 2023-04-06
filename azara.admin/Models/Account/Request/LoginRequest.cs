namespace azara.admin.Models.Account.Request;

public class LoginRequest : BasePasswordRequest
{
    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "enter_valid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EmailId { get; set; }
}