namespace azara.admin.Models.TeamManagement.Response
{
    public class DefaultPermissions
    {
        public List<Detail> details { get; set; }

    }

    public class Detail
    {
        public string id { get; set; }
        public string? name { get; set; }
        public string? fontIconName { get; set; }
        public string? defaultPermissionCsv { get; set; } = "Add,Update,View,Delete,Status,Export";
        public string? permissionCsv { get; set; }
        public string? parentId { get; set; }
        public bool isChild { get; set; }
        public string? pageUrl { get; set; }
        public int priority { get; set; }
        public bool isSelected { get; set; }
        public List<Detail> menuList { get; set; }
    }
}
