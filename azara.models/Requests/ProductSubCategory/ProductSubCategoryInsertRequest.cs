namespace azara.models.Requests.ProductSubCategory;

public class ProductSubCategoryInsertRequest
{
    public string? Id { get; set; }

    public Guid? ProductCategoryId { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    public string Name { get; set; }

    public string? Image { get; set; }

    public bool Status { get; set; }
}