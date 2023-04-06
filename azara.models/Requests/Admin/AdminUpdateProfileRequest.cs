namespace azara.models.Requests.Admin;

public class AdminUpdateProfileRequest
{
    public string Id { get; set; }

    public string? ProfilePhoto { get; set; }

    [Required(ErrorMessageResourceName = "error_username_required")]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "error_email_required")]
    public string EmailId { get; set; }

    //public string UniqueId { get; set; }
}