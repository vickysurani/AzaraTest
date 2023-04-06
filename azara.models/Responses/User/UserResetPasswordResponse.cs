namespace azara.models.Responses.User;

public class UserResetPasswordResponse
{
    [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required, MaxLength(10)]
    public string Otp { get; set; }
}