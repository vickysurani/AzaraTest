namespace azara.models.Entities;

public class CouponsEntity : BaseEntity
{
    [Required]
    public string CouponTitle { get; set; }

    [Required]
    public string CouponImage { get; set; }

    public DateTime ExpiryDate { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string? CouponCode { get; set; }

    public int CouponsPoint { get; set; }
}