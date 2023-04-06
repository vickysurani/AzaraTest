namespace azara.models.Requests.ProductCategory;

public class ProductCategoryInsertUpdateRequest
{
    public string? ProductCategoryId { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    public bool Status { get; set; }

    public string? Image { get; set; }
}