namespace azara.models.Requests.SubAdmin
{
    public class SubAdminAddRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "error_invalid_email")]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "error_invalid_password_format")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "error_invalid_Mobile")]
        public string Mobile { get; set; }

        public string? ModifiedBy { get; set; }

        public string? PermissionJson { get; set; }

        public ICollection<ApiSubAdminPermissionListRequest> MenuList { get; set; }
    }

    public class ApiSubAdminPermissionListRequest
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? FontIconName { get; set; }

        public string? PermissionCsv { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsChild { get; set; }

        public string? PageUrl { get; set; }

        public int Priority { get; set; }

        public bool IsSelected { get; set; } = true;

        public ICollection<ApiPermissionListRequest>? MenuList { get; set; }
    }
}
