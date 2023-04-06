namespace azara.models.Entities;

public class UserEntity : BaseEntity
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required, MaxLength(250)]
    public string EmailId { get; set; }

    public bool EmailVerified { get; set; } = false;

    [MaxLength(7)]
    public string? EmailOtp { get; set; }

    public DateTime? EmailOtpExpiry { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public int? ZipCode { get; set; }

    public string? Image { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

    [MaxLength(10)]
    public string? Otp { get; set; }

    public DateTime? OtpExpireTime { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    [Required]
    public Guid ModifiedBy { get; set; }

    public string? FrontImage { get; set; } = null;

    public string? SelfieImage { get; set; } = null;

    public int? Points { get; set; }

    public bool? IsFirstLogin { get; set; }
}