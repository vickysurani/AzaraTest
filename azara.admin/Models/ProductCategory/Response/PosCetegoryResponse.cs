namespace azara.admin.Models.ProductCategory.Response
{
    public class PosCetegoryResponse
    {
        public int Total { get; set; }

        public int TotalPages { get; set; }

        public int OffSet { get; set; }

        public List<POSSubCategoryData> Details { get; set; }
    }

    public class POSSubCategoryData
    {

        public int Id { get; set; }
        public int SrNo { get; set; }

        public string Name { get; set; }

        public string? Image { get; set; }

        public bool? Active { get; set; }

        public string Attribute { get; set; }

        public string? DepartmentListId { get; set; }
        public string? DepartmentListName { get; set; }

    }
}
