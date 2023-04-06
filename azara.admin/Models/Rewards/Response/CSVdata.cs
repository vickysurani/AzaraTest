namespace azara.admin.Models.Rewards.Response
{
    public class CSVdata
    {
        public List<AdjustmentFileUploadResponse> Details { get; set; }
    }
    public class AdjustmentFileUploadResponse
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string RewardsPoint { get; set; }
    }
}
