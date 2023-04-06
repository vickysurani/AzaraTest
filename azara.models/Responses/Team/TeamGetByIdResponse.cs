using azara.models.Responses.Menu;

namespace azara.models.Responses.Team
{
    public class TeamGetByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? PermissionJson { get; set; }

        public int CurrentRevision { get; set; }

        public string? ModifiedBy { get; set; }

        public ICollection<MenuResponse> MenuList { get; set; }
    }
}
