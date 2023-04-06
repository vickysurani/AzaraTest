namespace azara.models.Requests.Event
{
    public class EventListRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? SortBy { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }

        public string? LocationDetail { get; set; }
    }
}
