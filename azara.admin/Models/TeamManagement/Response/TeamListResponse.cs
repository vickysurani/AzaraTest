namespace azara.admin.Models.TeamManagement.Response
{
    public class TeamListResponse
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int offSet { get; set; }
        public List<TeamData> details { get; set; }
    }

    public class TeamData
    {
        public int SrNo { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string permissionJson { get; set; }
        public int currentRevision { get; set; }
        public DateTime modified { get; set; }
        public bool active { get; set; }
        public string modifiedBy { get; set; }
        public int totalUser { get; set; }
    }

}
