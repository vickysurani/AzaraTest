namespace azara.client.Models.Common.Response
{
    public class FileResponse
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

    public class FileListResponse
    {
        public List<FileResponse> Details { get; set; }

    }
}
