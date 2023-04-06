namespace azara.models.Entities
{
    public class BannerEntity : BaseEntity
    {
        public string? BannerImage1 { get; set; }

        public string? BannerImage2 { get; set; }

        public string? BannerImage3 { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
