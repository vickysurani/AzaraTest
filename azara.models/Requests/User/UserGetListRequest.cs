namespace azara.models.Requests.User;

public class UserGetListRequest : PaginationRequest
{
    public string Name { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;

    public string? SortBy { get; set; } = string.Empty;
}