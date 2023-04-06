namespace azara.models.Requests.Admin;

public class AdminForgotPasswordRequest
{
    [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
    public string EmailId { get; set; }
}