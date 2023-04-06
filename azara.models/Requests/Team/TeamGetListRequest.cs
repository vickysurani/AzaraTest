namespace azara.models.Requests.Team
{
    public class TeamGetListRequest : PaginationRequest
    {
        public string? Name { get; set; } = string.Empty;

        public string? SortBy { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }
    }
}
