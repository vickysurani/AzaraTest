namespace azara.models.Entities.Base;

public class BaseIdEntity
{
    [Key, Required]
    public Guid Id { get; set; }

    public BaseIdEntity() => Id = Guid.NewGuid();
}