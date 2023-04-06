namespace azara.admin.Models.ProductCategory.Request;

public class ProductCategoryInsertUpdateRequest
{
    public string? ProductCategoryId { get; set; }

    [Required(ErrorMessageResourceName = "error_product_category_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Name { get; set; }

    public string? Image { get; set; }

    public List<string>? ImageList { get; set; }

}