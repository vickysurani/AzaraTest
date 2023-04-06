namespace azara.models.Entities;

public class ReferralEntity : BaseEntity
{
    public string ReferralCode { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; }

    public int Count { get; set; } = 0;

    public Guid ReferredTo { get; set; }
}