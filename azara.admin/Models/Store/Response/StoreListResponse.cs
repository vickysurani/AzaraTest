namespace azara.admin.Models.Store.Response;

public class StoreListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<StoreListData> Details { get; set; }
}

public class StoreListData
{
    public int SrNo { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string image { get; set; }

    public string location { get; set; }

    public string address { get; set; }

    public string emailId { get; set; }

    public string contactNumber { get; set; }

    public string description { get; set; }

    public string latitude { get; set; }

    public string longitude { get; set; }

    public bool isLocationFound { get; set; }

    public DateTime modified { get; set; }

    public DateTime created { get; set; }

    public bool active { get; set; }

    public string Code { get; set; }
}