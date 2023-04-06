namespace azara.models.Responses.Banner
{
    public class BannerGetByIdResponse
    {
        public string? Id { get; set; }

        public string? BannerImage1 { get; set; }

        public string? BannerImage2 { get; set; }

        public string? BannerImage3 { get; set; }

        public bool Status { get; set; }

        public DateTime Created { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
