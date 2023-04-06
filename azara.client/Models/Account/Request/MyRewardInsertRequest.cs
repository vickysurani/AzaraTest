namespace azara.client.Models.Account.Request;

public class MyRewardInsertRequest
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public bool? AvailableRewards { get; set; }

    public bool? UsedRewards { get; set; }

    public bool? RemainingRewards { get; set; }

    public string? UserId { get; set; }

    public string? RewardId { get; set; }

    public string? CouponsId { get; set; }
}