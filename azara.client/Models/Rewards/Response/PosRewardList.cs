namespace azara.client.Models.Rewards.Response
{
    public class PosRewardList
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int offSet { get; set; }
        public List<PosRewardData> details { get; set; }

    }

    public class PosRewardData
    {
        public string fullName { get; set; }
        public DateTime rewardExpirationDate { get; set; }
        public string rewardStatus { get; set; }
    }
}
