namespace azara.models.Entities;

public class ProductSubCategoryEntity : BaseEntity
{
    public Guid? ProductCategoryId { get; set; }

    [ForeignKey("ProductCategoryId")]
    public ProductCategoryEntity ProductCategoryEntity { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public string? Image { get; set; }
}