namespace azara.models.Requests.Product;

public class ProductUpdateRequest : BaseModifiedByRequest
{
    [Required(ErrorMessage = "error_image_required")]
    public string Image { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }

    public string OfferLabel { get; set; }

    public Guid? StoreId { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public Guid? ModifiedBy { get; set; }
}