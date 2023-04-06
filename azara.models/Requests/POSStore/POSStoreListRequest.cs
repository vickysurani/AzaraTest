namespace azara.models.Requests.POSStore
{
    public class POSStoreListRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string? SearchParam { get; set; }

        public string? OrderBy { get; set; }
    }
}
