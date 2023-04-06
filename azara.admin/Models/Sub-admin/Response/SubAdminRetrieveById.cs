
namespace azara.admin.Models.Sub_admin.Response
{
    public class SubAdminRetrieveById
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }
        public string Password { get; set; }

        public string? PermissionJson { get; set; }

        public string UserName { get; set; }

        public string Mobile { get; set; }

        public bool Active { get; set; }

        public string RoleId { get; set; }

        public List<azara.admin.Models.TeamManagement.Response.Detail> MenuList { get; set; }
    }

    public class Revision
    {
        public int SrNo { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("details")]
        public List<Detail> Details { get; set; }

    }

    public class MenuLists
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fontIconName")]
        public string FontIconName { get; set; }

        [JsonProperty("permissionCsv")]
        public string PermissionCsv { get; set; }

        [JsonProperty("defaultPermissionCsv")]
        public string DefaultPermissionCsv { get; set; }

        [JsonProperty("parentId")]
        public object ParentId { get; set; }

        [JsonProperty("isChild")]
        public bool IsChild { get; set; }

        [JsonProperty("pageUrl")]
        public string PageUrl { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }

        [JsonProperty("menuList")]
        public List<MenuLists> menuList { get; set; }
    }


    public class Detail
    {
        public int SrNo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
