namespace azara.models.Responses.Event;

public class EventListResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime EventStartTime { get; set; }

    public DateTime EventEndTime { get; set; }

    public string EventLocation { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public DateTime Modified { get; set; }

    public bool IsLocationFound { get; set; } = false;
}