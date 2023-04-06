namespace azara.models.Requests.PunchCard
{
    public class PunchCardListRequest
    {
        public string PunchCardName { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }

        public string? SortBy { get; set; } = string.Empty;

        public Guid? UserId { get; set; }
    }
}