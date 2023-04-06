namespace azara.admin.Models.Points.Response;

public class PointGetByIdResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public string? UserId { get; set; }

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

    public int SrNo { get; set; }
}