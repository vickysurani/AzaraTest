namespace azara.models.Requests.SubAdmin
{
    public class SubAdminStatusUpdateRequest : BaseIdRequest
    {
        public bool Active { get; set; }

        [JsonIgnore]
        public string? ModifiedBy { get; set; }
    }
}
