namespace azara.models.Requests.Base;

public class BaseModifiedByRequest : BaseIdRequest
{
    [System.Text.Json.Serialization.JsonIgnore]
    public string? ModifiedBy { get; set; }
}

public class ApiModifiedByRequest
{
    [JsonIgnore]
    public string? ModifiedBy { get; set; }
}