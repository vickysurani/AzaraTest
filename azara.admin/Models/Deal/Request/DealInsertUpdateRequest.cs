namespace azara.admin.Models.Advertisement.Request;

public class DealInsertUpdateRequest
{
    public string Id { get; set; }

    [Required(ErrorMessageResourceName = "error_advertisement_title_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Title { get; set; }


    [Required(ErrorMessageResourceName = "error_advertisement_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string BannerImage { get; set; }


    [Required(ErrorMessageResourceName = "error_advertisement_description_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Description { get; set; }

    public string StoreId { get; set; }

    [Required(ErrorMessageResourceName = "error_advertisement_position_required", ErrorMessageResourceType = typeof(ErrorMessage))]

    public string Position { get; set; }


    [Required(ErrorMessageResourceName = "error_advertisement_url_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Url { get; set; }

    public List<string>? ImageList { get; set; }


    public List<StoreDetail> StoreDetails { get; set; }
}

public class StoreDetail
{
    public string StoreId { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Name { get; set; }
    public bool IsNearestLocation { get; set; }
}