namespace azara.models.Requests.Common;

public class FileUploadRequest : BaseValidateRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("file")]
    [Required(ErrorMessage = "bad_response_file_required")]
    public IFormFile File { get; set; }
}

public class FileWithNameUploadRequest : BaseValidateRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("file")]
    [Required(ErrorMessage = "bad_response_file_required")]
    public IFormFile File { get; set; }

    [Required]
    public string FileName { get; set; }

    public string? OldFileName { get; set; }

    public string FolderName { get; set; }
}


public class FileNewWithNameUploadRequest : BaseValidateRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("file")]
    [Required(ErrorMessage = "bad_response_file_required")]
    public List<IFormFile> File { get; set; }

    [Required]
    public List<string> FileName { get; set; }

    public List<string>? OldFileName { get; set; }

    public string FolderName { get; set; }
}
