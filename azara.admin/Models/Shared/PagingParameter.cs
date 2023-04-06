namespace azara.admin.Models.Shared;

public class PagingParameter
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int TotalPage { get; set; }

    public int PageNumber { get; set; }

    public int FirstItemNumber { get; set; }

    public int LastItemNumber { get; set; }

    public int CurrentPageNumber { get; set; }

    public bool ShowPageNumber { get; set; } = false;

    public bool ShowFooter { get; set; } = false;
}