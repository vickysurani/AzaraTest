namespace azara.admin.Models.TeamManagement.Response
{
    public class TeamGetByIdResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string permissionJson { get; set; }
        public int currentRevision { get; set; }
        public string modifiedBy { get; set; }
        public List<MenuList> menuList { get; set; }
    }

    public class MenuList
    {
        public string id { get; set; }
        public string name { get; set; }
        public object fontIconName { get; set; }
        public string defaultPermissionCsv { get; set; }
        public string permissionCsv { get; set; }
        public object parentId { get; set; }
        public bool isChild { get; set; }
        public object pageUrl { get; set; }
        public int priority { get; set; }
        public bool isSelected { get; set; }
        public object menuList { get; set; }
    }

}
