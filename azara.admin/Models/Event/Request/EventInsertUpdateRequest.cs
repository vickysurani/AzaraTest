namespace azara.admin.Models.Event.Request;

public class EventInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_event_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(100, ErrorMessage = "Event name max length is 100", MinimumLength = 0)]
    public string Name { get; set; }

    [Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [MaxLength(100)]
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Required(ErrorMessageResourceName = "error_eventdate_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessageResourceName = "error_eventstarttime_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.DateTime)]
    public DateTime EventStartTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessageResourceName = "error_eventendtime_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [DataType(DataType.DateTime)]
    public DateTime EventEndTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessageResourceName = "error_location_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string EventLocation { get; set; }

    public string? Description { get; set; }

    //[Required(ErrorMessage = "error_active_required")]
    //public bool Active { get; set; } = true;

    public List<string>? ImageList { get; set; }

}