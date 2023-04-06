namespace azara.admin.Models.Rewards.Response;

public class RewardListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<RewardListData> Details { get; set; }
}

public class RewardListData
{
    public Guid id { get; set; }

    public string name { get; set; }

    public string image { get; set; }
    public int rewardspoint { get; set; }

    public string description { get; set; }

    public string barcode { get; set; }

    public string pointId { get; set; }

    public string userId { get; set; }

    public DateTime modified { get; set; }

    public bool status { get; set; }

    public DateTime created { get; set; }

    public object rewardsExpiryDate { get; set; }

    public int SrNo { get; set; }
}