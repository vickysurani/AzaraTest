namespace azara.admin.Models.Account.Response;

public class AdminDetailsResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; }

    public object MobileNumber { get; set; }

    public string ProfilePhoto { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}