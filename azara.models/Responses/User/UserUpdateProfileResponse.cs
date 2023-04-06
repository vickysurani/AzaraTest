namespace azara.models.Responses.User;

public class UserUpdateProfileResponse : BaseAuditResponse
{
    [System.Text.Json.Serialization.JsonIgnore]
    public string? UserId { get; set; }

    public string? Image { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmailId { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public int? ZipCode { get; set; }

    [Required]
    public int? Points { get; set; }

    public bool? IsFirstLogin { get; set; }

}