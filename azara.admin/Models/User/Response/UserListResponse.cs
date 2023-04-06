namespace azara.admin.Models.User.Response;

public class UserListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public List<UserListData> Details { get; set; }
}

public class UserListData
{
    public int SrNo { get; set; }

    public string id { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string emailId { get; set; }

    public string mobileNumber { get; set; }

    public DateTime created { get; set; }

    public string referraCode { get; set; }

    public int Points { get; set; }

    public string? AssignPoints { get; set; }

    public int count { get; set; }

    public bool active { get; set; }
}