namespace azara.models.Entities;

public class NotificationEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsRead { get; set; }

    public DateTime ReadableTime { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; }

    public string? Icon { get; set; }    
}