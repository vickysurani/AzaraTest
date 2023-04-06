namespace azara.models.Entities
{
    public class UserEventsEntity : BaseEntity
    {
        public Guid? EventId { get; set; }

        public Guid? UserId { get; set; }
    }
}
