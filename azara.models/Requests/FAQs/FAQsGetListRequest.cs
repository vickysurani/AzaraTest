namespace azara.models.Requests.FAQs;

public class FAQsGetListRequest : PaginationRequest
{
    public string Question { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;
}