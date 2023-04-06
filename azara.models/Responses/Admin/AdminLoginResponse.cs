namespace azara.models.Responses.Admin;

public class AdminLoginResponse : BaseAuditResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string EmailId { get; set; }

    //public bool EmailVerified { get; set; } = false;

    public string Mobile { get; set; }

    public string? ProfilePhoto { get; set; }

    public string UniqueId { get; set; }

    public string Token { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
}