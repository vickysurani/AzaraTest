namespace azara.models.Requests.Contest
{
    public class ContestListRequest
    {
        public string ContestName { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }

        public string? SortBy { get; set; } = string.Empty;

        public string? LocationDetail { get; set; }
    }
}
