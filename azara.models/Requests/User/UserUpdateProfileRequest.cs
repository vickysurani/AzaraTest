namespace azara.models.Requests.User;

public class UserUpdateProfileRequest
{
    public string Id { get; set; }

    public string? Image { get; set; }

    [Required(ErrorMessage = "error_first_name_required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "error_last_name_required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "error_email_required")]
    public string EmailId { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public int Points { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public int ZipCode { get; set; }

    [Required]
    public string Address { get; set; }

    public bool? ISFirstLogin { get; set; }
}