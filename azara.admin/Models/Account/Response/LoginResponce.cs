namespace azara.admin.Models.Account.Response;

public class LoginResponse : CallAPI
{
    public string Id { get; set; }

    public string EmailId { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }

    public string? UniqueId { get; set; }

    public bool IsSuccess { get; set; }

    public string ErrorMessage { get; set; }
}