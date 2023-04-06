namespace azara.admin.Models.ProductCategory.Response;

public class ProductCategoryListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<ProductCategoryListData> Details { get; set; }
}

public class ProductCategoryListData
{
    public int SrNo { get; set; }

    public string name { get; set; }

    public string image { get; set; }

    public bool active { get; set; }
     
    public bool deleted { get; set; }

    public DateTime created { get; set; }

    public DateTime modified { get; set; }

    public Guid id { get; set; }
}