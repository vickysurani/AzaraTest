namespace azara.admin.Models.Points.Request;

public class PointsInsertUpdateRequest
{
    public string? PointId { get; set; }

    public int AvailablePoints { get; set; } = 10;

    public int UsedPoints { get; set; }

    public string PointName { get; set; }

    public Guid? UserId { get; set; }
}