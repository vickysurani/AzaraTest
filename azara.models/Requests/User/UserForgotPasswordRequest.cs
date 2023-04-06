namespace azara.models.Requests.User;

public class UserForgotPasswordRequest
{
    [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email", ErrorMessageResourceName = "error_email_required")]
    public string EmailId { get; set; }
}