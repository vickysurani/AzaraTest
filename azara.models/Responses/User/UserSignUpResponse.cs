namespace azara.models.Responses.User;

public class UserSignUpResponse : BaseAuditResponse
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmailId { get; set; }

    public string MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public int? ZipCode { get; set; }

    public string Image { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }



}