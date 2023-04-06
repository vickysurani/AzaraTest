namespace azara.admin.Models.Base.Request;

public class BaseStatusUpdateRequest
{
    public Guid Id { get; set; }

    public bool Active { get; set; }
}