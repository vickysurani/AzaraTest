namespace azara.models.Requests.Coupons
{
    public class ApiCouponsCSVExportRequest
    {
        public string CouponTitle { get; set; }

        public string? CouponCode { get; set; }

        public int? CouponsPoint { get; set; }

        public decimal Amount { get; set; }
    }

    public class CouponsDataList
    {
        public List<ApiCouponsCSVExportRequest> Details { get; set; }
    }
}
