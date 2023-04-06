namespace azara.admin.Models.Base.Request
{
    public class BaseFileUploadRequest
    {
        public IBrowserFile File { get; set; }

        public string? FileName { get; set; }
    }
}
