namespace azara.models.Requests.Base;

public class BaseListRequest : PaginationRequest
{
    public string Name { get; set; }

    public string SortBy { get; set; }
}