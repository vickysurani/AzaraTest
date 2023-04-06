namespace azara.admin.Models.Points.Request
{
    public class PointAssignRequest
    {
        public Guid? UserId { get; set; }

        public string Points { get; set; }
    }
}
