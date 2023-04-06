namespace azara.models.Requests.Points;

public class PointsInsertUpdateRequest : BaseIdRequest
{
    public int AvailablePoints { get; set; } = 10;

    public int UsedPoints { get; set; }

    public string PointName { get; set; }

    public Guid? UserId { get; set; }
}