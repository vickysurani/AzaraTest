namespace azara.models.Requests.Deals;

public class DealsGetListRequest : PaginationRequest
{
    public string Title { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public bool IsDisplayActive { get; set; }

    public string? SortBy { get; set; } = string.Empty;

    public string? LocationDetail { get; set; }
}