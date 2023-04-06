namespace azara.admin.Models.AboutUs
{
    public class AboutUsData
    {
        public List<AboutUsDataResponse> Details { get; set; }    
    }
    public class AboutUsDataResponse
    {
        public string? Description { get; set; }

        public string? Image { get; set; }

        public Guid id { get; set; }


    }
}
