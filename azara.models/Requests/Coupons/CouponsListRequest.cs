namespace azara.models.Requests.Coupons
{
    public class CouponsListRequest
    {
        public string CouponTitle { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;

        public bool IsDisplayActive { get; set; }

        public string? SortBy { get; set; } = string.Empty;

        public Guid? UserId { get; set; }
    }
}
