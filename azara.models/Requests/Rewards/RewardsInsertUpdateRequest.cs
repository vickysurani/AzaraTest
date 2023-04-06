namespace azara.models.Requests.Rewards;

public class RewardsInsertUpdateRequest
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string? Barcode { get; set; }

    public Guid? PointId { get; set; }

    public Guid? UserId { get; set; }

    public bool Status { get; set; }

    public DateTime? RewardsExpiryDate { get; set; }

    public int RewardsPoint { get; set; }
}