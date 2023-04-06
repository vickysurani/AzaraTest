namespace azara.admin.Models.Rewards.Response;

public class PunchcardListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<PunchcardListData> Details { get; set; }
}

public class PunchcardListData
{
    public Guid id { get; set; }

    public string PunchCardName { get; set; }
    public int SrNo { get; set; }

    public string? Image { get; set; }

    public string Description { get; set; }

    public string PunchCardPromos { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool Active { get; set; }
    public bool Status { get; set; }

    public DateTime Modified { get; set; }

    public DateTime Created { get; set; }

    public bool IsUsed { get; set; }
}