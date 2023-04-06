namespace azara.models.Requests.Points
{
    public class PointsAddedForRewardsAndCouponsRequest
    {
        public Guid? UserId { get; set; }

        public int Points { get; set; }

        public string Status { get; set; }
        public string StatusCode { get; set; }

        public Guid StatusId { get; set; }
    }
}
