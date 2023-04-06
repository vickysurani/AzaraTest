namespace azara.models.Requests.Event
{
    public class EventUsersIntertRequest : BaseIdRequest
    {
        public Guid? EventId { get; set; }

        public Guid? UserId { get; set; }
    }
}
