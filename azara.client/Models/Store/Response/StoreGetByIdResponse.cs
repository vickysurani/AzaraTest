namespace azara.client.Models.Store.Response;

public class StoreGetByIdResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Location { get; set; }

    public string Address { get; set; }

    public string EmailId { get; set; }

    public string ContactNumber { get; set; }

    public string Description { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public List<ProductListResponse> ProductDetail { get; set; }
    public List<StoreProductDetails> StoreProductDetails { get; set; }
}

public class ProductListResponse
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal DiscountedPrice { get; set; }

    public string OfferLabel { get; set; }

    public string StoreId { get; set; }

    public string StoreName { get; set; }
}

public class StoreProductDetails
{
    public string? ListId { get; set; }

    public string ProductName { get; set; }

    public decimal? Quantity { get; set; }

    public string Image { get; set; }

    public decimal? Price1 { get; set; }

    public decimal? Price2 { get; set; }

    public decimal? Price3 { get; set; }

    public decimal? Price4 { get; set; }

    public decimal? Price5 { get; set; }

    public string Description { get; set; }
}