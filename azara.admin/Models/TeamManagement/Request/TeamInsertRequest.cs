using azara.admin.Models.TeamManagement.Response;

namespace azara.admin.Models.TeamManagement.Request
{
    public class TeamInsertRequest
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public List<Detail> MenuList { get; set; }
    }


}
