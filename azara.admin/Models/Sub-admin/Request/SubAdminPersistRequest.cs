
using azara.admin.Models.TeamManagement.Response;

namespace azara.admin.Models.Sub_admin.Request
{
    public class SubAdminPersistRequest
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string? PermissionJson { get; set; }

        [Required]
        public string RoleId { get; set; } = "C43BCA69-8E07-4E5E-85F5-0DB10B003CBF";

        public List<Detail> MenuList { get; set; }
    }
}
