namespace azara.models.Responses.Points;

public class PointsGetListResponse //: PaginationResponse
{
    public Guid? UserId { get; set; }

    public string? UserName { get; set; }

    public int AvailablePointCount { get; set; }

    public List<PointHistory> PointHistory { get; set; }
}

public class PointHistory
{
    public string PointName { get; set; }

    public DateTime PointRedeeemDate { get; set; }

    public int Point { get; set; }

    public bool IsSubTract { get; set; }
}
public class PointsListResponse
{
    public Guid? UserId { get; set; }

    public string? UserName { get; set; }

    public int AvailablePoints { get; set; }

    public int UsedPoints { get; set; }

    public int Point { get; set; }

    public string LastPointName { get; set; }

    public DateTime RedeemDate { get; set; }

    public DateTime Modified { get; set; }
}