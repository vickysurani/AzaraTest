namespace azara.admin.Models.Coupons.Response
{

    public class AdjustmentFileUploadResponseData
    {
        public List<AdjustmentFileUploadResponse> Details { get; set; }
    }
    public class AdjustmentFileUploadResponse
    {
        public string CouponTitle { get; set; }
        public string CouponCode { get; set; }
        public string CouponsPoint { get; set; }
        public string Amount { get; set; }
    }
}
