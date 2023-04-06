namespace azara.client.Models.Account.Request;

public class SignUpRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_first_name_required", ErrorMessageResourceType = typeof(Massage))]
    //[RegularExpression(@"^[a-zA-Z]{3,7}$", ErrorMessageResourceName = "error_invalid_firstname", ErrorMessageResourceType = typeof(Massage))]
    public string FirstName { get; set; }

    [Required(ErrorMessageResourceName = "error_last_name_required", ErrorMessageResourceType = typeof(Massage))]
    //[RegularExpression(@"^[a-zA-Z]{3,7}$", ErrorMessageResourceName = "error_invalid_lastname", ErrorMessageResourceType = typeof(Massage))]
    public string LastName { get; set; }

    [Required(ErrorMessageResourceName = "error_email_required", ErrorMessageResourceType = typeof(Massage))]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "error_invalid_email", ErrorMessageResourceType = typeof(Massage))]
    public string EmailId { get; set; }

    [Required]
    [RegularExpression(@"[0-9]\d{9}$", ErrorMessageResourceName = "error_invalid_Mobile", ErrorMessageResourceType = typeof(Massage))]
    public string? MobileNumber { get; set; }

    [Required(ErrorMessageResourceName = "error_Image_required", ErrorMessageResourceType = typeof(Massage))]
    public string? Image { get; set; }

    [Required(ErrorMessageResourceName = "error_password_required", ErrorMessageResourceType = typeof(Massage))]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_invalid_password_format", ErrorMessageResourceType = typeof(Massage))]
    public string Password { get; set; }

    [Required(ErrorMessageResourceName = "error_confirm_password_required", ErrorMessageResourceType = typeof(Massage))]
    [Compare("Password", ErrorMessageResourceName = "error_confirm_password_is_not_same_with_password", ErrorMessageResourceType = typeof(Massage))]
    public string ConfirmPassword { get; set; }

    public DateTime Created { get; set; }

    [Required(ErrorMessage = "error_password_required")]
    public bool IsConditionAccepted { get; set; }

    public string? ReferralCode { get; set; }

    [Required(ErrorMessageResourceName = "error_birthdate_required", ErrorMessageResourceType = typeof(Massage))]
    [RegularExpression(@"^[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}$", ErrorMessageResourceName = "error_data_format", ErrorMessageResourceType = typeof(Massage))]
    public DateTime? BirthDate { get; set; }

    //[Required(ErrorMessageResourceName = "error_country_required", ErrorMessageResourceType = typeof(Massage))]
    //public string? Country { get; set; }

    [Required(ErrorMessageResourceName = "error_address_required", ErrorMessageResourceType = typeof(Massage))]
    public string? Address { get; set; }

    [Required(ErrorMessageResourceName = "error_city_required", ErrorMessageResourceType = typeof(Massage))]
    public string? City { get; set; }

    [Required(ErrorMessageResourceName = "error_state_required", ErrorMessageResourceType = typeof(Massage))]
    public string? State { get; set; }

    [Required(ErrorMessageResourceName = "error_zipcode_required", ErrorMessageResourceType = typeof(Massage))]
    public int? ZipCode { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageUpload { get; set; }

    public int Sequence { get; set; }
    public string? FrontImage { get; set; } = null;

    public string? SelfieImage { get; set; } = null;

    public string? ImagePath { get; set; }

    public List<string>? ImageList { get; set; }

    public string? FrontImagePath { get; set; }
    public string? SelfieImagePath { get; set; }

    public bool? IsFirstLogin { get; set; }
}