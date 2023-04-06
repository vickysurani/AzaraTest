namespace azara.client.Models.Base.Request;

public class ListRequest
{
    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string ContestName { get; set; } = string.Empty;

    public bool IsDisplayActive { get; set; } = true;

    public string LocationDetail { get; set; }

    public bool IsAdminList { get; set; } = false;

    public string? SortBy { get; set; } = string.Empty;

    public string? ProductCategoryName { get; set; } = string.Empty;

    public string UserId { get; set; }
}

public class ListIdRequest
{
    public int PageNo { get; set; }

    public int PageSize { get; set; }
}

public class PaginationRequest
{
    public int PageNo { get; set; }

    public int PageSize { get; set; }
}