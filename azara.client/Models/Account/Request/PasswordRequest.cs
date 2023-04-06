namespace azara.client.Models.Account.Request;

public class PasswordRequest
{
    [Required(ErrorMessage = "Please enter password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessageResourceName = "error_password_invalid", ErrorMessageResourceType = typeof(Massage))]
    public string Password { get; set; }
}