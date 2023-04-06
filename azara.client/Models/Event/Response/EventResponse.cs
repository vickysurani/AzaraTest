namespace azara.client.Models.Event.Response;

public class EventResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<EventData> Details { get; set; }
}

public class EventData
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public bool Active { get; set; }

    public DateTime Eventstarttime { get; set; }

    public DateTime Eventendtime { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime EventTime { get; set; }

    public string EventLocation { get; set; }

    public string Description { get; set; }

    public bool IsLocationFound { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}