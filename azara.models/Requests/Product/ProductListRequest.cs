namespace azara.models.Requests.Product
{
    public class ProductListRequest
    {
        public string? Name { get; set; } = string.Empty;

        public string? ProductCategoryName { get; set; } = string.Empty;

        public string? StoreName { get; set; } = string.Empty;

        public string? RewardsName { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public string? SortBy { get; set; } = string.Empty;
    }
}
