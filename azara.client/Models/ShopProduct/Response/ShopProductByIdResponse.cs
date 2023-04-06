namespace azara.client.Models.ShopProduct.Response;

public class ShopProductByIdResponse
{
    public string Image { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double OriginalPrice { get; set; }

    public double DiscountedPrice { get; set; }

    public string OfferName { get; set; }

    public string StoreId { get; set; }

    public string ProductCategoryId { get; set; }

    public string ModifiedBy { get; set; }

    public string StoreName { get; set; }

    public string ProductCategoryName { get; set; }
}