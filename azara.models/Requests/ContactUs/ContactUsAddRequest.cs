namespace azara.models.Requests.ContactUs;

public class ContactUsAddRequest
{
    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "error_email_required")]
    public string EmailId { get; set; }

    public string Description { get; set; }
}