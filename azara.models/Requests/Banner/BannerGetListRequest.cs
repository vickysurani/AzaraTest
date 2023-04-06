namespace azara.models.Requests.Banner
{
    public class BannerGetListRequest
    {
        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }

        public string? SortBy { get; set; } = string.Empty;

    }
}
