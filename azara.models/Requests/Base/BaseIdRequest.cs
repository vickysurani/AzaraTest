namespace azara.models.Requests.Base;

public class BaseIdRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
}