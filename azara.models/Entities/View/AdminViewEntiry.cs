namespace azara.models.Entities.View;

public class AdminViewEntiry
{
    public string Master { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string EmailId { get; set; }

    public bool EmailVerified { get; set; }

    public string Mobile { get; set; }

    public bool MobileVerified { get; set; }

    public string? ProfilePhoto { get; set; }

    public string? Otp { get; set; }

    public string? OtpExpireTime { get; set; }

    public string? LastLoginAt { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid ModifiedBy { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public string Created { get; set; }

    public string Modified { get; set; }
}