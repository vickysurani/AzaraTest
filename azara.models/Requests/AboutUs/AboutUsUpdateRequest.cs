namespace azara.models.Requests.AboutUs
{
    public class AboutUsUpdateRequest : BaseIdRequest
    {
        public string? Description { get; set; }

        public string? Image { get; set; }

        public List<string>? ImageList { get; set; }
    }
}
