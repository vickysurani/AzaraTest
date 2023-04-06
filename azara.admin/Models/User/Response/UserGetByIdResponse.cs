namespace azara.admin.Models.User.Response;

public class UserGetByIdResponse
{
    public string firstName { get; set; }

    public string lastName { get; set; }

    public string emailId { get; set; }

    public string mobileNumber { get; set; }

    public string image { get; set; }

    public string password { get; set; }

    public string confirmPassword { get; set; }

    public DateTime created { get; set; }
}