namespace azara.client.Models.Account.Request;

public class ResetPasswordRequest
{
    [Required]
    public string EmailId { get; set; }

    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(Massage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_invalid_password_format", ErrorMessageResourceType = typeof(Massage))]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "error_confirm_password_required")]
    [Compare("Password", ErrorMessageResourceName = "error_confirm_password_is_not_same_with_password", ErrorMessageResourceType = typeof(Massage))]
    public string ConfirmPassword { get; set; }

    //[Required(ErrorMessage = "error_otp_required")]
    //public string Otp { get; set; } = "otp";
}