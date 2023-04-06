namespace azara.models.Entities;

public class RewardsEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string? Barcode { get; set; }

    public Guid? PointId { get; set; }

    [ForeignKey("PointId")]
    public PointsEntity PointsEntity { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; }

    public DateTime? RewardsExpiryDate { get; set; }

    public int RewardsPoint { get; set; }
}