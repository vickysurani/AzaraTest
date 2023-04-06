namespace azara.models.Entities
{
    public class UserContestEntity : BaseEntity
    {
        public Guid? ContestId { get; set; }

        public Guid? UserId { get; set; }
    }
}

