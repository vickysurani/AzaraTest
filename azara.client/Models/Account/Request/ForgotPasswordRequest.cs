namespace azara.client.Models.Account.Request;

public class ForgotPasswordRequest
{
    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(Massage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(Massage))]
    public string EmailId { get; set; }
}