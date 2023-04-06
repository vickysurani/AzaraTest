namespace azara.admin.Models.User.Request;

public class UserInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_first_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string FirstName { get; set; }

    [Required(ErrorMessageResourceName = "error_last_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string LastName { get; set; }

    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EmailId { get; set; }

    [Required(ErrorMessageResourceName = "error_mobile_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [RegularExpression(@"[0-9]\d{9}$", ErrorMessageResourceName = "error_invalid_Mobile", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? MobileNumber { get; set; }

    [Required(ErrorMessageResourceName = "error_birthdate_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessageResourceName = "error_address_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? Address { get; set; }

    [Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? Image { get; set; }

    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_invalid_password_format", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessageResourceName = "error_confirm_password_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [Compare("Password", ErrorMessageResourceName = "error_confirm_password_is_not_same_with_password", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string ConfirmPassword { get; set; }

    public DateTime Created { get; set; }

    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public bool IsConditionAccepted { get; set; }

    [Required(ErrorMessageResourceName = "error_city_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? City { get; set; }

    [Required(ErrorMessageResourceName = "error_state_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? State { get; set; }

    [Required(ErrorMessageResourceName = "error_zipcode_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public int? ZipCode { get; set; }

    public int? Points { get; set; }

    public string? AssignPoints { get; set; }

    public Guid? UserID { get; set; }
}