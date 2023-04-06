namespace azara.models.Requests.Points
{
    public class PointsListRequest
    {
        public string PointName { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public string? SortBy { get; set; } = string.Empty;
    }
}
