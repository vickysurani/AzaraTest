namespace azara.models.Responses.Admin;

public class AdminResponse : BaseAuditResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; }

    public string MobileNumber { get; set; }

    public string ProfilePhoto { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid ModifiedBy { get; set; }
}