namespace azara.client.Models.POSSubCategory.Response
{
    public class POSSubCategoryByIdResponse
    {
        public List<POSSubcategoryData> Details { get; set; }
    }

    public class POSSubcategoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }

        public bool? Active { get; set; }

        public string Attribute { get; set; }

        public string? DepartmentListId { get; set; }
    }
}
