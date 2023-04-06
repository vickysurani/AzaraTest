namespace azara.models.Entities;

public class ProductEntity : BaseEntity
{
    [Required]
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Required, MaxLength(100)]
    [Display(Name = "Name")]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal OriginalPrice { get; set; }

    [Required]
    public decimal DiscountedPrice { get; set; }

    public Guid? ProductCategoryId { get; set; }

    [ForeignKey("ProductCategoryId")]
    public ProductCategoryEntity ProductCategoryEntity { get; set; }

    public Guid? ProductSubCategoryId { get; set; }

    [ForeignKey("ProductSubCategoryId")]
    public ProductSubCategoryEntity ProductSubCategoryEntity { get; set; }

    public Guid? OfferId { get; set; }

    [ForeignKey("OfferId")]
    public OffersEntity OffersEntity { get; set; }

    public Guid? RewardsId { get; set; }

    [ForeignKey("RewardsId")]
    public RewardsEntity RewardsEntity { get; set; }

    [Required]
    public Guid? ModifiedBy { get; set; }

    public string? StoreDropdownId { get; set; }
}