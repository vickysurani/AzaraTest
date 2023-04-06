namespace azara.models.Requests.PointManagement
{
    public class PointManagementGetListRequest : PaginationRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public string? SortBy { get; set; } = string.Empty;
    }
}
