namespace azara.client.Models.Banner
{
    public class BannerImageListResponse
    {
        public List<BannerImageListData> details { get; set; }
    }

    public class BannerImageListData
    {
        public string? id { get; set; }
        public string bannerImage1 { get; set; }
        public string bannerImage2 { get; set; }
        public string bannerImage3 { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public object modifiedBy { get; set; }
        public bool active { get; set; }
    }
}
