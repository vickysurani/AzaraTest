namespace azara.models.Responses.Admin;

public class AdminUpdateProfileResponse : BaseAuditResponse
{
    public string AdminId { get; set; }

    public string? ProfilePhoto { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string EmailId { get; set; }
}