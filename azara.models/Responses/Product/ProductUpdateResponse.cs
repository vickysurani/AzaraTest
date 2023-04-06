namespace azara.models.Responses.Product;

public class ProductUpdateResponse
{
    public string Image { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }

    public Guid? StoreId { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public Guid? OfferId { get; set; }

    public Guid? RewardsId { get; set; }

    public Guid? ModifiedBy { get; set; }

    public string? StoreDropdownId { get; set; }
}