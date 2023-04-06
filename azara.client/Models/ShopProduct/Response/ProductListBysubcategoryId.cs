namespace azara.client.Models.ShopProduct.Response;

public class ProductListBysubcategoryId
{
    public List<ProductList> ProductList { get; set; }
}

public class ProductList
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double OriginalPrice { get; set; }

    public double DiscountedPrice { get; set; }

    public double OfferLabel { get; set; }

    public string StoreId { get; set; }

    public object ProductCategoryId { get; set; }

    public object ModifiedBy { get; set; }

    public string StoreName { get; set; }

    public object ProductCategoryName { get; set; }

    public object ProductSubCategoryId { get; set; }

    public string SkuId { get; set; }
}