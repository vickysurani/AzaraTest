namespace azara.models.Requests.Base
{
    public class BaseListIdStatusUpdateRequest
    {
        public string Id { get; set; }

        public bool Active { get; set; }
    }

    public class BaseIntStatusUpdateRequest
    {
        public int Id { get; set; }

        public bool Active { get; set; }
    }
}
