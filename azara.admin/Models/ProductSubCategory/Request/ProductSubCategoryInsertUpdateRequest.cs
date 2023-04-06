namespace azara.admin.Models.ProductSubCategory.Request;

public class ProductSubCategoryInsertUpdateRequest
{
    public string? Id { get; set; }

    public Guid? ProductCategoryId { get; set; }

    [Required(ErrorMessageResourceName = "error_product_sub_category_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Name { get; set; }

    //public bool Status { get; set; }

    public string Image { get; set; }

    public List<string>? ImageList { get; set; }

}