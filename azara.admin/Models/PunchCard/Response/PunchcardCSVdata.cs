namespace azara.admin.Models.Rewards.Response
{
    public class PunchcardCSVdata
    {
        public List<PunchcardAdjustmentFileUploadResponse> Details { get; set; }
    }
    public class PunchcardAdjustmentFileUploadResponse
    {
        public string PunchCardName { get; set; }
        public string PunchCardPromos { get; set; }
    }
}
