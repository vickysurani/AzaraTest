namespace azara.admin.Models.Contest.Response;

public class ContestListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int Offset { get; set; }

    public List<ContestListData> Details { get; set; }
}

public class ContestListData
{
    public int SrNo { get; set; }

    public string id { get; set; }

    public string contestName { get; set; }

    public string image { get; set; }

    public string description { get; set; }

    public string contestType { get; set; }

    public string location { get; set; }

    public bool active { get; set; }
}