namespace azara.models.Requests.Contest
{
    public class ContestUserInsertRequest : BaseIdRequest
    {
        public Guid? ContestId { get; set; }

        public Guid? UserId { get; set; }
    }
}
