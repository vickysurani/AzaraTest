namespace azara.client.Models.Base.Request;

public class BaseImageUploadRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("file")]
    //[Required(ErrorMessageResourceName = "bad_response_file_required", ErrorMessageResourceType = typeof(Message))]
    public IBrowserFile File { get; set; }

    public string? FileName { get; set; }
}

public class FileNewInsertUploadRequest
{

    [System.Text.Json.Serialization.JsonPropertyName("file")]
    [Required(ErrorMessage = "bad_response_file_required")]
    public List<IBrowserFile> File { get; set; }

    [Required]
    public List<string> FileName { get; set; }

    public List<string>? OldFileName { get; set; }

    public string FolderName { get; set; }
}