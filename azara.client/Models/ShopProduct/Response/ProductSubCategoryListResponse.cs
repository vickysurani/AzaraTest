namespace azara.client.Models.ShopProduct.Response;

public class ProductSubCategoryListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<ProductSubCategory> Details { get; set; }
}

public class ProductSubCategory
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public DateTime Modified { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }
}