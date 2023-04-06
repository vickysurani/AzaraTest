namespace azara.models.Requests.User;

public class UserOtpVerifyRequest
{
    public string EmailId { get; set; }

    [Required(ErrorMessageResourceName = "error_otp_required")]
    public string Otp { get; set; }
}