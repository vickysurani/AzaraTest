namespace azara.models.Responses.Banner
{
    public class BannerGetListResponse
    {
        public Guid Id { get; set; }

        public string? BannerImage1 { get; set; }

        public string? BannerImage2 { get; set; }

        public string? BannerImage3 { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public Guid? ModifiedBy { get; set; }

        public bool? Active { get; set; }
    }
}
