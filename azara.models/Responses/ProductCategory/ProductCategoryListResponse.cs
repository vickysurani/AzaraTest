namespace azara.models.Responses.ProductCategory
{
    public class ProductCategoryListResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Image { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}
