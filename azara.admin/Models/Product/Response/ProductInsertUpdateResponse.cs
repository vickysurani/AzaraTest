namespace azara.admin.Models.Product.Response;

public class ProductInsertUpdateResponse : BaseIdRequest
{
    public string? ProductId { get; set; }

    public string? Image { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal OriginalPrice { get; set; }

    public string OfferLabel { get; set; }

    public Guid? StoreId { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public Guid? ModifiedBy { get; set; }
}