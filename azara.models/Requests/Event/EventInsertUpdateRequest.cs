namespace azara.models.Requests.Event;

public class EventInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_name_required")]
    [StringLength(100, ErrorMessage = "Event name max length is 100", MinimumLength = 0)]
    public string Name { get; set; }

    [Required(ErrorMessage = "error_image_required"), MaxLength(100)]
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Required(ErrorMessage = "error_eventdate_required")]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "error_eventstarttime_required")]
    [DataType(DataType.DateTime)]
    public DateTime EventStartTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "error_eventendtime_required")]
    [DataType(DataType.DateTime)]
    public DateTime EventEndTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "error_location_required")]
    public string EventLocation { get; set; }

    [Required(ErrorMessage = "error_description_required")]
    public string Description { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    //[Required(ErrorMessage = "error_active_required")]
    //public bool Active { get; set; } = true;
}