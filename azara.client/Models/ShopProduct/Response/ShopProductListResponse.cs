namespace azara.client.Models.ShopProduct.Response;

public class ShopProductListResponse
{
    public List<ShopProductListData> Details { get; set; }
}

public class ShopProductListData
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string Name { get; set; }

    public string Descripition { get; set; }

    public double OriginalPrice { get; set; }

    public double DiscountedPrice { get; set; }

    public string OfferName { get; set; }

    public string StoreName { get; set; }

    public string ProductCategoryName { get; set; }

    public string ModifiedBy { get; set; }

    public string StoreId { get; set; }
}