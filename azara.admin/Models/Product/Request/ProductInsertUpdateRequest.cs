namespace azara.admin.Models.Product.Request;

public class ProductInsertUpdateRequest
{
    public string? Id { get; set; }

    public string? ProductId { get; set; }

    public List<string>? ImageList { get; set; }

    public string? Image { get; set; }

    [Required(ErrorMessageResourceName = "error_product_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(100, ErrorMessage = "Product name max length is 100", MinimumLength = 0)]
    public string Name { get; set; }

    [Required(ErrorMessageResourceName = "error_description_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(10000, ErrorMessage = "Product price max length is 10000", MinimumLength = 0)]
    public string Description { get; set; }

    [Required(ErrorMessageResourceName = "error_price_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [Range(0.00, 9999999.00, ErrorMessage = "Please enter a discount between 0.00 and 9999999.00")]
    public string OriginalPrice { get; set; }

    public string? StoreId { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public Guid? ProductSubCategoryId { get; set; }

    [Required(ErrorMessageResourceName = "error_offername_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string OfferId { get; set; }

    [Required(ErrorMessageResourceName = "error_reward_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string RewardsId { get; set; }

    public string offerName { get; set; }
    public string rewardName { get; set; }

    public Guid? ModifiedBy { get; set; }

    public string? StoreDropdownId { get; set; }

}