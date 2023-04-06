namespace azara.admin.Models.Coupons.Request;

public class CouponsInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_coupon_title_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [StringLength(100, ErrorMessage = "Coupon title max length is 100", MinimumLength = 0)]
    public string CouponTitle { get; set; }

    [Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string? CouponImage { get; set; }

    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

    public string Description { get; set; }

    public string Amount { get; set; }

    public string? CouponCode { get; set; }

    public bool Active { get; set; }
    public List<string>? ImageList { get; set; }

    //public int CouponsPoint { get; set; }


}