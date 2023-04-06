namespace azara.admin.Models.Account.Request;

public class ForgotPasswordRequest
{
    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EmailId { get; set; }
}