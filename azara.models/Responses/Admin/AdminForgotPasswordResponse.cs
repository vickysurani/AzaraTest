namespace azara.models.Responses.Admin;

public class AdminForgotPasswordResponse : BaseAuditResponse
{
    [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public string AdminId { get; set; }
}