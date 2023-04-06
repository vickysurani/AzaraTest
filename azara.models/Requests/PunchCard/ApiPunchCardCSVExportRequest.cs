namespace azara.models.Requests.PunchCard
{
    public class ApiPunchCardCSVExportRequest
    {
        public string PunchCardName { get; set; }

        public string PunchCardPromos { get; set; }
    }
    public class PunchCardDataList
    {
        public List<ApiPunchCardCSVExportRequest> Details { get; set; }
    }
}
