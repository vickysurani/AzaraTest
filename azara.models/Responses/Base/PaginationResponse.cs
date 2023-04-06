namespace azara.models.Responses.Base;

public class PaginationResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public dynamic Details { get; set; }
}