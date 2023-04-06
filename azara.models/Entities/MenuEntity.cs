namespace azara.models.Entities
{
    public class MenuEntity : BaseEntity
    {
        public Guid? ParentId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string? FontIconName { get; set; }

        public string? PageUrl { get; set; }

        public string? PermissionCsv { get; set; }

        public int Priority { get; set; }

        public bool IsSelected { get; set; }
    }
}
