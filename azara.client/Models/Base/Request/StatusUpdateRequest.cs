namespace azara.client.Models.Base.Request;

public class StatusUpdateRequest
{
    public Guid Id { get; set; }

    public bool Active { get; set; }
}