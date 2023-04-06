namespace azara.admin.Models.Account.Response
{
    public class MenuListResponse
    {
        public List<Menudata> details { get; set; }
    }

    public class Menudata
    {
        public string id { get; set; }
        public string name { get; set; }
        public string fontIconName { get; set; }
        public object defaultPermissionCsv { get; set; }
        public object permissionCsv { get; set; }
        public object parentId { get; set; }
        public bool isChild { get; set; }
        public string pageUrl { get; set; }
        public int priority { get; set; }
        public bool isSelected { get; set; }
        public object menuList { get; set; }
    }
}
