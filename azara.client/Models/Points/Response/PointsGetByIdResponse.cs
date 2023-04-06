namespace azara.client.Models.Points.Response;

public class PointsGetByIdResponse
{
    public Guid? UserId { get; set; }

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