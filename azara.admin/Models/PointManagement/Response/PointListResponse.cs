namespace azara.admin.Models.PointManagement.Response
{
    public class PointListResponse
    {
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public int Offset { get; set; }
        public List<PointData> details { get; set; }
    }

    public class PointData
    {
        public int SrNo { get; set; }
        public string userId { get; set; }
        public string Id { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        public string point { get; set; }
        public DateTime modified { get; set; }
    }
}
