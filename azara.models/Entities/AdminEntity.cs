namespace azara.models.Entities;

public class AdminEntity : BaseEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required, MaxLength(250)]
    public string Password { get; set; }

    [Required, MaxLength(250)]
    public string EmailId { get; set; }

    //public bool EmailVerified { get; set; } = false;

    [Required, MaxLength(20)]
    public string Mobile { get; set; }

    //public bool MobileVerified { get; set; }

    public string? ProfilePhoto { get; set; }

    [MaxLength(10)]
    public string? Otp { get; set; }

    public DateTime? OtpExpireTime { get; set; }

    //public DateTime? LastLoginAt { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid ModifiedBy { get; set; }

    public Guid? RoleId { get; set; }

    [ForeignKey("RoleId")]
    public MasterRolesEntity MasterRolesEntity { get; set; }

    //public Guid? TeamId { get; set; }

    //[ForeignKey("TeamId")]
    //public TeamEntity TeamEntity { get; set; }

    public int CurrentRevisionNumber { get; set; }

    public string? PermissionJson { get; set; }

    public bool IsSubAdmin { get; set; } = false;
}