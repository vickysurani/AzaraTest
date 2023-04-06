namespace azara.models.Requests.ContactUs
{
    public class ContactListRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public string? SortBy { get; set; } = string.Empty;
    }
}
