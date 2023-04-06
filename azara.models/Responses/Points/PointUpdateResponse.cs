namespace azara.models.Responses.Points;

public class PointUpdateResponse
{
    public string Id { get; set; }

    public int AvailablePoints { get; set; } = 10;

    public int UsedPoints { get; set; }

    public string PointName { get; set; }

    public Guid? UserId { get; set; }
}