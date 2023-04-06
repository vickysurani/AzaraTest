namespace azara.models.Requests.POSInventory
{
    public class POSInventotyGetListRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string SearchParam { get; set; }

        public string? OrderBy { get; set; }
    }
}
