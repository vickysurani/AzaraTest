namespace azara.models.Requests.Genric;

public class AttachmentUploadRequest : FileUploadRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [Required(ErrorMessage = "bad_response_file_upload_type_required")]
    public string Type { get; set; }
}