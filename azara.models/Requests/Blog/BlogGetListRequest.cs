namespace azara.models.Requests.Blog;

public class BlogGetListRequest : PaginationRequest
{
    public string Title { get; set; } = string.Empty;

    public string? SortBy { get; set; } = string.Empty;

    public bool IsAdminList { get; set; } = false;
}