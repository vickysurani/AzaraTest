namespace azara.models.Entities;

public class FacebookRewardSharingEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string RewardEarned { get; set; }

    public string Description { get; set; }
}