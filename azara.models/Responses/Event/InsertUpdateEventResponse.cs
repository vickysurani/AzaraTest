namespace azara.models.Responses.Event;

public class InsertUpdateEventResponse
{
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Image { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EventStartTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EventEndTime { get; set; }

    [Required]
    public string EventLocation { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool Active { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }
}