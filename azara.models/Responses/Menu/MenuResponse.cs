namespace azara.models.Responses.Menu
{
    public class MenuResponse
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string? FontIconName { get; set; }

        public string? DefaultPermissionCsv { get; set; }

        public string? PermissionCsv { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsChild { get; set; }

        public string? PageUrl { get; set; }

        public int Priority { get; set; }

        public bool IsSelected { get; set; }

        public ICollection<MenuResponse> MenuList { get; set; }
    }
}
