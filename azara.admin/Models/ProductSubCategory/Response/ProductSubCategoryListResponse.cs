namespace azara.admin.Models.ProductSubCategory.Response;

public class ProductSubCategoryListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<ProductSubCategoryListData> Details { get; set; }
}

public class ProductSubCategoryListData
{
    public int SrNo { get; set; }

    public Guid id { get; set; }

    public string name { get; set; }
    public string productCategoryName { get; set; }
    public string image { get; set; }
    public bool status { get; set; }
    public DateTime modified { get; set; }
    public DateTime created { get; set; }
}