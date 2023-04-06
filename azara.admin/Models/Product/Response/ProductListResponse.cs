namespace azara.admin.Models.Product.Response;

public class ProductListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<ProductListData> Details { get; set; }
}

public class ProductListData
{
    public string id { get; set; }
    public int SrNo { get; set; }
    public string image { get; set; }
    public string name { get; set; }
    public string descripition { get; set; }
    public double originalPrice { get; set; }
    public double discountedPrice { get; set; }
    public string storeName { get; set; }
    public string productCategoryName { get; set; }
    public string offerName { get; set; }
    public DateTime modified { get; set; }
    public string modifiedBy { get; set; }
    public string storeId { get; set; }
}