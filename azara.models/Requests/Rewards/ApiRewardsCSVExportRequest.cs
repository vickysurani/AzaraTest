namespace azara.models.Requests.Rewards
{
    public class ApiRewardsCSVExportRequest
    {
        public string Name { get; set; }

        public string? Barcode { get; set; }

        public int RewardsPoint { get; set; }
    }
    public class RewardsDataList
    {
        public List<ApiRewardsCSVExportRequest> Details { get; set; }

    }
}
