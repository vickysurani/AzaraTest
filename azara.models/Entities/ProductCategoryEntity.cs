namespace azara.models.Entities;

public class ProductCategoryEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public string? Image { get; set; }
}