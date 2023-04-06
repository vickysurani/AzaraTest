namespace azara.models.Responses.Product;

public class StoreProductCategoryResponse
{
    public List<StoreDropDownResponse> StoreList { get; set; }

    public List<ApiBaseDropdownResponse> ProductCategoryList { get; set; }

    public List<ApiBaseDropdownResponse> OfferLabelList { get; set; }
}

public class StoreDropDownResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Lat { get; set; }

    public string Long { get; set; }

    public bool IsLocationFound { get; set; }
}