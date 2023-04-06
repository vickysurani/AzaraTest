namespace azara.models.Requests.Admin;

public class AdminChangePasswordRequest
{
    [System.Text.Json.Serialization.JsonIgnore]
    public Guid AdminId { get; set; }

    [Required(ErrorMessage = "error_current_password_required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,30}$", ErrorMessageResourceName = "error_invalid_password_format")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "error_password_required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,30}$", ErrorMessageResourceName = "error_invalid_password_format")]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "error_confirm_password_required")]
    [Compare("Password", ErrorMessageResourceName = "error_confirm_password_is_not_same_with_password")]
    public string ConfirmPassword { get; set; }
}