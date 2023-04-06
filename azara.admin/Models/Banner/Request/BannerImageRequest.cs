namespace azara.admin.Models.Banner.Request
{
    public class BannerImageRequest
    {
        public string Id { get; set; }
        public string? bannerImage1 { get; set; }
        public string? bannerImage2 { get; set; }
        public string? bannerImage3 { get; set; }
        public bool status { get; set; }
        public List<string>? ImageList { get; set; }

    }
}
