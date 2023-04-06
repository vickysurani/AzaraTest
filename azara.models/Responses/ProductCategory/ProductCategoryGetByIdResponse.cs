namespace azara.models.Responses.ProductCategory;

public class ProductCategoryGetByIdResponse
{
    public string Name { get; set; }

    public bool Status { get; set; }

    public string Image { get; set; }

    public List<ProductSubCategoryDetail> ProductSubCategoryDetail { get; set; }
}

public class ProductSubCategoryDetail
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}