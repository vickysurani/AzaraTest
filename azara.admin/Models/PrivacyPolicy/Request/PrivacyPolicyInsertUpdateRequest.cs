namespace azara.admin.Models.PrivacyPolicy.Request
{
    public class PrivacyPolicyInsertUpdateRequest : BaseIdRequest
    {
        public string? Id { get; set; }
        public string? Description { get; set; }
    }
}
