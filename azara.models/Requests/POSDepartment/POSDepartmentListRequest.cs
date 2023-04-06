namespace azara.models.Requests.POSDepartment
{
    public class POSDepartmentListRequest
    {
        public string? ListId { get; set; } = string.Empty;

        public string? DefaultMarginPercent { get; set; } = string.Empty;

        public string? DefaultMarkupPercent { get; set; } = string.Empty;

        public string? DepartmentCode { get; set; } = string.Empty;

        //public string? TaxCode { get; set; } = string.Empty;

        public string? DepartmentName { get; set; } = string.Empty;

        //public string? StoreExchangeStatus { get; set; } = string.Empty;
    }
}
