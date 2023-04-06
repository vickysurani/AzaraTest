namespace azara.models.Responses.ProductSubCategory
{
    public class ProductSubCategoryListResponse
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string ProductCategoryName { get; set; }

        public string? Image { get; set; }

        public bool Status { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
