namespace azara.models.Entities;

public class ChatEntity : BaseEntity
{
    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; }

    public Guid? AdminId { get; set; }

    [ForeignKey("AdminId")]
    public AdminEntity AdminEntity { get; set; }

    public string Message { get; set; }

    public bool IsAdminRead { get; set; }

    public bool IsUserRead { get; set; }
}