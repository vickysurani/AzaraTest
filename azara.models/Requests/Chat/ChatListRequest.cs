namespace azara.models.Requests.Chat;

public class ChatListRequest : PaginationRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public string? SortBy { get; set; } = string.Empty;
}