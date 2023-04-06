namespace azara.models.Requests.Product;

public class ProductInsertUpdateRequest : BaseIdRequest
{
    public string? Id { get; set; }

    public string? ProductId { get; set; }

    public List<string>? ImageList { get; set; }

    public string? Image { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    [StringLength(100, ErrorMessage = "Product name max length is 100", MinimumLength = 0)]
    public string Name { get; set; }

    [Required(ErrorMessage = "error_description_required")]
    [StringLength(10000, ErrorMessage = "Product price max length is 10000", MinimumLength = 0)]
    public string Description { get; set; }

    [Required(ErrorMessage = "error_price_required")]
    [Range(0.00, 9999999.00, ErrorMessage = "Please enter a discount between 0.00 and 9999999.00")]
    public string OriginalPrice { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public Guid? ProductSubCategoryId { get; set; }

    public Guid? OfferId { get; set; }

    public Guid? RewardsId { get; set; }

    public Guid? ModifiedBy { get; set; }

    public string? StoreDropdownId { get; set; }
}