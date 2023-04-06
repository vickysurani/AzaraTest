namespace azara.models.Requests.Store;

public class StoreGetListRequest : PaginationRequest
{
    public string? Name { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public string? LocationDetail { get; set; }

    public string? SortBy { get; set; }
}