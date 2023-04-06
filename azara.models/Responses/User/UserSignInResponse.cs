namespace azara.models.Responses.User;

public class UserSignInResponse : BaseResponse
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; } = false;

    public string MobileNumber { get; set; }

    public string Image { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }

    public string UniqueId { get; set; }
}