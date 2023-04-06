using azara.models.Requests.SubAdmin;

namespace azara.models.Responses.SubAdmins
{
    public class SubAdminGetByIdResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

        public string? PermissionJson { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Mobile { get; set; }

        public bool Active { get; set; }

        public string RoleId { get; set; }

        public ICollection<ApiSubAdminUpdatePermissionListRequest> MenuList { get; set; }
    }
}
