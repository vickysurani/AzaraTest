namespace azara.models.Entities;

public class AdminAccessTokenEntity : BaseEntity
{
    public Guid AdminId { get; set; }

    public string Purpose { get; set; }

    public Guid UniqueId { get; set; } = Guid.NewGuid();
}