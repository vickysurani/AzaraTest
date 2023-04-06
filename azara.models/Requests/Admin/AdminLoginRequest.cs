namespace azara.models.Requests.Admin;

public class AdminLoginRequest : PasswordRequest
{
    [JsonProperty("emailId")]
    [Required(ErrorMessage = "error_email_required")]
    public string EmailId { get; set; }

    [JsonIgnore]
    public string UniqueId { get; set; }
}