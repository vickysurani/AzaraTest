namespace azara.models.Entities;

public class EventsEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required, MaxLength(100)]
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public DateTime EventStartTime { get; set; }

    [Required]
    public DateTime EventEndTime { get; set; }

    [Required]
    public string EventLocation { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Latitude { get; set; }

    [Required]
    public string Longitude { get; set; }
}