namespace azara.models.Entities
{
    public class POSSubCategoryEntity
    {
        [Key, Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? Attribute { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public bool Active { get; set; }

        public string? DepartmentListId { get; set; }

        public string? DepartmentName { get; set; }

        public string? ListId { get; set; }

    }
}
