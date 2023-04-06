namespace azara.client.Models.Notification
{
    public class NotificationGetList
    {
        public Guid? Id { get; set; }

        public List<NotificationListResponse> Details { get; set; }

    }
    public class NotificationListResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime ReadableTime { get; set; }

        public Guid? UserId { get; set; }

        public string? Icon { get; set; }
    }
}
