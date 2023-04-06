namespace azara.admin.Models.Offers.Response;

public class OffersListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<OffersData> Details { get; set; }
}

public class OffersData
{
    public string id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string description { get; set; }
    public double amount { get; set; }
    public DateTime modified { get; set; }
    public bool active { get; set; }
    public int SrNo { get; set; }
}