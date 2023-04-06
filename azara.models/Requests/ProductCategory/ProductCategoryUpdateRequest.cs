namespace azara.models.Requests.ProductCategory;

public class ProductCategoryUpdateRequest : BaseIdRequest
{
    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    public string Status { get; set; }
}