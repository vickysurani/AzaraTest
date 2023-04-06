namespace azara.admin.Models.Base.Request;

public class BaseImageUploadRequest
{
    public Guid AdminId { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    //[Required(ErrorMessageResourceName = "bad_response_file_required", ErrorMessageResourceType = typeof(Message))]
    public IBrowserFile File { get; set; }

    public string? FileName { get; set; }
}