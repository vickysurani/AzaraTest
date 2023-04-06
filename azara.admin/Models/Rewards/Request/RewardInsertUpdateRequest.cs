namespace azara.admin.Models.Rewards.Request;

public class RewardInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_reward_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? Name { get; set; }


    [Required(ErrorMessageResourceName = "error_reward_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]

    public string Image { get; set; }

    [Required(ErrorMessageResourceName = "error_reward_description_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Description { get; set; }

    public Guid? PointId { get; set; }

    public string? Barcode { get; set; }

    public Guid? UserId { get; set; }

    public DateTime Created { get; set; }

    public bool Status { get; set; }

    public DateTime? RewardsExpiryDate { get; set; }

    public List<string>? ImageList { get; set; }

    public int RewardsPoint { get; set; }

}