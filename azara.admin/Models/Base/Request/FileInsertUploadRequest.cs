namespace azara.admin.Models.Base.Request
{
    public class FileInsertUploadRequest
    {

        [System.Text.Json.Serialization.JsonPropertyName("file")]
        [Required(ErrorMessage = "bad_response_file_required")]
        public IBrowserFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? OldFileName { get; set; }

        public string FolderName { get; set; }
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

    public class FileListResponse
    {
        public List<FileResponse> Details { get; set; }

    }
}
