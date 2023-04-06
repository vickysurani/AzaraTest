namespace azara.client.Models.Account.Request;

public class LoginRequest : PasswordRequest
{
    [Required(ErrorMessage = "Please enter email address")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "enter_valid_email", ErrorMessageResourceType = typeof(Massage))]
    public string EmailId { get; set; }
}