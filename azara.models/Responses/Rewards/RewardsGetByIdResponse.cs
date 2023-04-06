namespace azara.models.Responses.Rewards;

public class RewardsGetByIdResponse
{
    public Guid? Id { get; set; }
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public Guid? PointId { get; set; }
    public Guid? StatusId { get; set; }


    public Guid? UserId { get; set; }

    public Guid? CouponsId { get; set; }

    public Guid? RewardId { get; set; }

    public DateTime? CouponExpiryDate { get; set; }

    public string? CouponImage { get; set; }

    public string? CouponCode { get; set; }

    public string? Barcode { get; set; }

    public DateTime Created { get; set; }

    public bool Status { get; set; }

    public DateTime? RewardsExpiryDate { get; set; }

    public int RewardsPoint { get; set; }

    public bool? IsUsed { get; set; } = false;
    public bool? IsReward { get; set; }

}