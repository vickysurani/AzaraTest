namespace azara.client.Models.Common.Request
{
    public class FileInsertUploadRequest : BaseValidateRequest
    {
        [System.Text.Json.Serialization.JsonPropertyName("file")]
        [Required(ErrorMessage = "bad_response_file_required")]
        public List<IBrowserFile> File { get; set; }

        [Required]
        public List<string> FileName { get; set; }

        public List<string>? OldFileName { get; set; }

        public string FolderName { get; set; }
    }

}
