namespace azara.models.Entities
{
    public class MyRewardEntity : BaseEntity
    {
        public string? Name { get; set; }

        public bool? AvailableRewards { get; set; }

        public bool? UsedRewards { get; set; }

        public bool? RemainingRewards { get; set; }
        public string? RemainingRewards1 { get; set; }
        public string? RemainingRewards11 { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }

        public Guid? RewardId { get; set; }

        [ForeignKey("RewardId")]
        public RewardsEntity RewardsEntity { get; set; }

        public Guid? CouponsId { get; set; }

        [ForeignKey("CouponsId")]
        public CouponsEntity CouponsEntity { get; set; }

        public Guid? PunchCardId { get; set; }

        [ForeignKey("PunchCardId")]
        public PunchCardEntity PunchCardEntity { get; set; }

        //public bool IsUsed { get; set; } = false;
        public bool? IsUsed { get; set; }
    }
}
