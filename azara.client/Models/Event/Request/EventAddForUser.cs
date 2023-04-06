namespace azara.client.Models.Event.Request
{
    public class EventAddForUser
    {
        public Guid? EventId { get; set; }

        public Guid? UserId { get; set; }
    }
}
