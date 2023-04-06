namespace azara.models.Requests.SubAdmin
{
    public class SubAdminListRequest : PaginationRequest
    {
        public string? Name { get; set; } = string.Empty;

        public string? EmailId { get; set; } = string.Empty;

        public string? Mobile { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;

        public string? RoleId { get; set; } = string.Empty;

        public string? SortBy { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        [JsonIgnore]
        public string? AdminId { get; set; }

        public string? SearchParam { get; set; }

    }
}
