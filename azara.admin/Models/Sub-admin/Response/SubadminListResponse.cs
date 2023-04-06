namespace azara.admin.Models.Sub_admin.Response
{
    public class SubadminListResponse
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int offSet { get; set; }
        public List<SubadminData> details { get; set; }
    }

    public class SubadminData
    {
        public string id { get; set; }
        public int SrNo { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string userName { get; set; }
        public string mobile { get; set; }
        public bool active { get; set; }
        public bool deleted { get; set; }
        public string teamName { get; set; }
        public DateTime modified { get; set; }
    }

}
