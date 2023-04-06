namespace azara.admin.Models.Points.Response;

public class PointsListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<PointsData> Details { get; set; }
}

public class PointsData
{
    public string UserId { get; set; }

    public string UserName { get; set; }

    public int AvailablePoints { get; set; }

    public int UsedPoints { get; set; }

    public int Point { get; set; }

    public string LastPointName { get; set; }

    public DateTime RedeemDate { get; set; }

    public int SrNo { get; set; }
}