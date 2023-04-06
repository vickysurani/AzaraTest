namespace azara.models.Requests.MyRewards
{
    public class MyRewardsInsertUpdateRequest
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public bool? AvailableRewards { get; set; }

        public bool? UsedRewards { get; set; }

        public bool? RemainingRewards { get; set; }

        public Guid? UserId { get; set; }

        public Guid? RewardId { get; set; }

        public Guid? CouponsId { get; set; }

        public Guid? PunchCardId { get; set; }
    }
}
