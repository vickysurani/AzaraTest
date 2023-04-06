namespace azara.models.Requests.Common;

public class BaseValidateRequest
{
    [System.Text.Json.Serialization.JsonIgnore]
    public Guid? UserId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string? UniqueId { get; set; }
}