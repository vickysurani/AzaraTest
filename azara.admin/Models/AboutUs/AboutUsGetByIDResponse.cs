namespace azara.admin.Models.AboutUs
{
    public class AboutUsGetByIDResponse : BaseIdRequest
    {
        public string? Id { get; set; }
        public string? Description { get; set; }

        public string? Image { get; set; }

        public List<string>? ImageList { get; set; }

    }
}
