namespace azara.client.Models.FAQs.Response;

public class FAQsListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int Offset { get; set; }

    public List<FAQsData> Details { get; set; }
}

public class FAQsData
{
    public string Id { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public int SrNo { get; set; }
}