namespace azara.admin.Models.TeamManagement.Request
{
    public class TeamDetailsGetByIdRequest
    {
        public string? Id { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
