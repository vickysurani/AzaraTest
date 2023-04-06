namespace azara.models.Entities;

public class UserAccessTokenEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public string Purpose { get; set; }

    public Guid UniqueId { get; set; } = Guid.NewGuid();
}