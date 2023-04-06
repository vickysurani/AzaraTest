namespace azara.client.Models.Common.Request;

public class BaseValidateRequest
{
    [System.Text.Json.Serialization.JsonIgnore]
    public Guid? UserId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string? UniqueId { get; set; }
}