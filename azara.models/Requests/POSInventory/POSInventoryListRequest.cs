namespace azara.models.Requests.POSInventory
{
    public class POSInventoryListRequest
    {
        public string? Desc1 { get; set; }

        public string? LocationDetail { get; set; }

        public string? SortBy { get; set; } = string.Empty;
    }
}
