namespace azara.client.Models.ProductCategory.Response;

public class ProductCategoryResponseData
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public string StoreId { get; set; }
}

public class ProductCategoryResponse
{
    public List<ProductCategoryResponseData> Details { get; set; }
}