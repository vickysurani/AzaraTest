namespace azara.admin.Models.Base.Request;

public class ListRequest
{
    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string AdvertisementTitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public string ContestName { get; set; } = string.Empty;

    public string? SearchParam { get; set; }

    public string Question { get; set; } = string.Empty;

    public string? SortBy { get; set; } = string.Empty;

    public bool IsAdminList { get; set; } = false;

    public string LocationDetail { get; set; }

    public string PointName { get; set; } = string.Empty;

    public string CouponTitle { get; set; } = string.Empty;
}

public class ListIdRequest : BaseIdRequest
{
    public int PageNo { get; set; }

    public int PageSize { get; set; }
}