namespace azara.models.Requests.User;

public class UserResetPasswordRequest
{
    [Required]
    public string EmailId { get; set; }

    [Required(ErrorMessage = "error_password_required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "error_invalid_password_format")]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "error_confirm_password_required")]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}