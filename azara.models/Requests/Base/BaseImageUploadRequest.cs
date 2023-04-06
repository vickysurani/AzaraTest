namespace azara.models.Requests.Base;

public class BaseImageUploadRequest
{
    public Guid AdminId { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    public IFormFile File { get; set; }

    public string? FileName { get; set; }

    public string? OldFileName { get; set; }
}