namespace azara.models.Responses.PrivacyPolicy
{
    public class PrivacyPolicyListResponse
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}
