namespace azara.admin.Models.Base.Request;

public class BasePasswordRequest
{
    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_invalid_password_format", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }
}