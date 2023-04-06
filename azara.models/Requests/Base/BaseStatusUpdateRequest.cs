namespace azara.models.Requests.Base;

public class BaseStatusUpdateRequest
{
    public Guid Id { get; set; }

    public bool Active { get; set; }
}