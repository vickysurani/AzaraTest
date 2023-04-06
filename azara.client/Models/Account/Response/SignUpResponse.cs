namespace azara.client.Models.Account.Response;

public class SignUpResponse
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmailId { get; set; }

    public string MobileNumber { get; set; }

    public string Image { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}