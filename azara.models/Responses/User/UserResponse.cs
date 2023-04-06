namespace azara.models.Responses.User;

public class UserResponse : BaseAuditResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; }

    public string MobileNumber { get; set; }

    public string Image { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid ModifiedBy { get; set; }

    public int Points { get; set; }

    public string? City { get; set; }
    public string? State { get; set; }
    public int? ZipCode { get; set; }

    public string? Address { get; set; }

    public bool? IsFirstLogin { get; set; }

}