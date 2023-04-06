namespace azara.client.Models.Rewards.Request
{
    public class UseMyRewardRequest
    {
        public string UserId { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }

        public Guid StatusId { get; set; }
    }
}
