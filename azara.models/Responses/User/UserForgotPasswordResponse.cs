namespace azara.models.Responses.User;

public class UserForgotPasswordResponse : BaseAuditResponse
{
    [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
    public string EmailId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public string UserId { get; set; }
}