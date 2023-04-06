namespace azara.models.Requests.PrivacyPolicy
{
    public class PrivacyPolicyInsertRequest : BaseIdRequest
    {
        public string? Id { get; set; }

        public string? Description { get; set; }
    }
}
