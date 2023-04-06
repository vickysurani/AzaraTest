namespace azara.models.Requests.SubAdmin
{
    public class SubAdminDeleteRequest : BaseIdRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string? ModifiedBy { get; set; }
    }
}
