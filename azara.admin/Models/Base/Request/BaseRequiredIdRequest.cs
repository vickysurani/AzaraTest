namespace azara.admin.Models.Base.Request;

public class BaseRequiredIdRequest
{
    [Required]
    public Guid Id { get; set; }

    public bool IsAdmin { get; set; }
}