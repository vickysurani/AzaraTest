namespace azara.models.Requests.Admin;

public class ApiSeedInsertRequest
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; } = false;

    public string Password { get; set; }

    public string Mobile { get; set; }

    public bool MobileVerified { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid ModifiedBy { get; set; }
}