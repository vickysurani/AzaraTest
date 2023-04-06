namespace azara.models.Requests.Points
{
    public class PointsAssignedByAdminRequest
    {
        public Guid? UserId { get; set; }

        public int Points { get; set; }
    }
}
