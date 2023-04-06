namespace azara.models.Requests.Product;

public class GetListRequest : PaginationRequest
{
    public string? Name { get; set; } = string.Empty;

    public string? ProductCategoryName { get; set; } = string.Empty;

    public string? StoreName { get; set; } = string.Empty;

    public string? RewardsName { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public bool IsDisplayActive { get; set; }

    public string? SortBy { get; set; } = string.Empty;
}