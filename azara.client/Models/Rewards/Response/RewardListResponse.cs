namespace azara.client.Models.Rewards.Response;

public class RewardListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<RewardData> Details { get; set; }
}

public class RewardData
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string Barcode { get; set; }

    public string PointId { get; set; }
    public string RequiredPoints { get; set; }

    public string UserId { get; set; }

    public DateTime Modified { get; set; }

    public bool Status { get; set; }
    public bool IsUsed { get; set; }

    public DateTime Created { get; set; }

    public DateTime? RewardsExpiryDate { get; set; }

    public int RewardsPoint { get; set; }
    public int rewardsPoint { get; set; }

}