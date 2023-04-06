namespace azara.admin.Models.Rewards.Request;

public class PunchCardInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_punch_card_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]

    public string PunchCardName { get; set; }


    public string? Image { get; set; }


    [Required(ErrorMessageResourceName = "error_punch_card_description_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Description { get; set; }


    [Required(ErrorMessageResourceName = "error_punch_card_promos_required", ErrorMessageResourceType = typeof(ErrorMessage))]

    public string PunchCardPromos { get; set; }


    public DateTime? ExpiryDate { get; set; }


    public bool Active { get; set; }

    public List<string>? ImageList { get; set; }

}