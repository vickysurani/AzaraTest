namespace azara.admin.Models.TeamManagement.Response
{
    public class TeamDropdownResponse
    {
        public List<TeamDropdownDetail> details { get; set; }

    }

    public class TeamDropdownDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }
}
