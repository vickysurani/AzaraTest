namespace azara.models.Responses.User;

public class UserGetbyIdResponse
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Address { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public int ZipCode { get; set; }

    public string? Image { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public DateTime Created { get; set; }

    public List<string> ReferedTo { get; set; }

    public string ReferedCode { get; set; }

    public int? Points { get; set; }
}