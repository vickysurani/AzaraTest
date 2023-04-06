namespace azara.client.Models.Account.Request;

public class ChangePasswordRequest
{
    [Required(ErrorMessageResourceName = "error_confirm_password_required", ErrorMessageResourceType = typeof(Massage))]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(Massage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_invalid_password_format", ErrorMessageResourceType = typeof(Massage))]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}