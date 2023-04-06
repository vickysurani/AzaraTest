namespace azara.models.Requests.Team
{
    public class TeamInsertRequest : ApiModifiedByRequest
    {
        public string Name { get; set; }

        public string? PermissionJson { get; set; }

        public ICollection<ApiPermissionListRequest> MenuList { get; set; }
    }
}

public class TeamUpdateRequest : BaseModifiedByRequest
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string? PermissionJson { get; set; }

    public ICollection<ApiPermissionListRequest> MenuList { get; set; }
}

public class ApiPermissionListRequest
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? FontIconName { get; set; }

    public string? PermissionCsv { get; set; }

    public Guid? ParentId { get; set; }

    public bool IsChild { get; set; }

    public string? PageUrl { get; set; }

    public int Priority { get; set; }

    public bool IsSelected { get; set; } = true;

    public ICollection<ApiPermissionListRequest>? MenuList { get; set; }
}
