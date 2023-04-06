namespace azara.models.Requests.Store
{
    public class StoreListRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public string? LocationDetail { get; set; }

        public string? SortBy { get; set; } = string.Empty;
    }
}
