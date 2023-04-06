namespace azara.admin.Models.Event.Response;

public class EventListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<EventListData> Details { get; set; }
}

public class EventListData
{
    public string Id { get; set; }

    public int SrNo { get; set; }

    public string image { get; set; }

    public string name { get; set; }

    public DateTime eventdate { get; set; }

    public DateTime eventstarttime { get; set; }

    public DateTime eventendtime { get; set; }

    public string eventLocation { get; set; }

    public bool active { get; set; }
}