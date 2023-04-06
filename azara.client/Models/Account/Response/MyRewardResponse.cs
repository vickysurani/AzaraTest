namespace azara.client.Models.Account.Response;

public class MyRewardResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<MyresponseData> Details { get; set; }
}

public class MyresponseData
{
    public string? Id { get; set; }

    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string? PointId { get; set; }
    public string? UserId { get; set; }
    public string? CouponsId { get; set; }
    public string? StatusId { get; set; }
    public DateTime? CouponExpiryDate { get; set; }
    public string? CouponImage { get; set; }
    public string? CouponCode { get; set; }
    public string? Barcode { get; set; }
    public string? IsReward { get; set; }

    public DateTime? Created { get; set; }
    public bool Status { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? RewardsExpiryDate { get; set; }
    public Guid? RewardId { get; set; }
}

public class MyRewardData
{
    public string? Id { get; set; }
  
    public string Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CouponsId { get; set; }

    public Guid? RewardId { get; set; }

    public string? Barcode { get; set; }

    public string? StatusId { get; set; }

    public string? IsReward { get; set; }
}