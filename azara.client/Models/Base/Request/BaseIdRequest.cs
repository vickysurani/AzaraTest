namespace azara.client.Models.Base.Request;

public class BaseIdRequest
{
    public Guid? Id { get; set; }

    public bool IsAdmin { get; set; }
}