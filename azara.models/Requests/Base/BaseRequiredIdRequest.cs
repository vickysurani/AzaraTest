namespace azara.models.Requests.Base;

public class BaseRequiredIdRequest
{
    [Required]
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public bool IsAdmin { get; set; }
}