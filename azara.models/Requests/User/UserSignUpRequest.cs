namespace azara.models.Requests.User;

public class UserSignUpRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_first_name_required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "error_last_name_required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "error_email_required")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "error_invalid_email")]
    public string EmailId { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? Image { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public int? ZipCode { get; set; }

    [Required(ErrorMessage = "error_password_required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "error_invalid_password_format")]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "error_confirm_password_required")]
    [Compare("Password", ErrorMessage = "error_confirm_password_is_not_same_with_password")]
    public string ConfirmPassword { get; set; }

    public DateTime Created { get; set; }

    [Required(ErrorMessage = "error_password_required")]
    public bool IsConditionAccepted { get; set; }

    public string? ReferralCode { get; set; }

    public string? FrontImage { get; set; } = null;

    public string? SelfieImage { get; set; } = null;
    public string? FrontImagePath { get; set; }
    public string? SelfieImagePath { get; set; }

    public bool? IsFirstLogin { get; set; }
}