namespace azara.models.Requests.ContactUs;

public class ContactGetListRequest : PaginationRequest
{
    public string Name { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public string? SortBy { get; set; } = string.Empty;
}