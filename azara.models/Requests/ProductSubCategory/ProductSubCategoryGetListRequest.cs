namespace azara.models.Requests.ProductSubCategory;

public class ProductSubCategoryGetListRequest : PaginationRequest
{
    public string Name { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public bool IsDisplayActive { get; set; }

    public string? SortBy { get; set; } = string.Empty;
}