namespace azara.models.Entities;

public class PointsEntity : BaseEntity
{
    public int AvailablePoints { get; set; }

    public int UsedPoints { get; set; }

    public string PointName { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; }
}