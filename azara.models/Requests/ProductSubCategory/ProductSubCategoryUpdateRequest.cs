namespace azara.models.Requests.ProductSubCategory;

public class ProductSubCategoryUpdateRequest : BaseIdRequest
{
    public Guid? ProductCategoryId { get; set; }

    public string Name { get; set; }

    public string? Image { get; set; }

    public bool Status { get; set; }
}