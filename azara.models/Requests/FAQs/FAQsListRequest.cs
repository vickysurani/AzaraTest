namespace azara.models.Requests.FAQs
{
    public class FAQsListRequest
    {
        public string Question { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;
    }
}
