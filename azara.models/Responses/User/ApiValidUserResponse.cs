namespace azara.models.Responses.User;

public class ApiValidUserResponse : BaseResponse
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string Otp { get; set; }
}