namespace azara.models.Requests.Points;

public class PointsGetListRequest : PaginationRequest
{
    public string UserName { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public string? SortBy { get; set; } = string.Empty;
}