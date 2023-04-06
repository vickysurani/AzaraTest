namespace azara.admin.Models.Account.Request;

public class ResetPasswordRequest
{
    [Required(ErrorMessage = "Please enter the password", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_valid_password")]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = "error_confirm_password_is_not_same_with_password")]
    public string ConfirmPassword { get; set; }
}