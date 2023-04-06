namespace azara.models.Responses.POSDepartment
{
    public class POSDepartmentListResponse
    {
        public int Id { get; set; }

        public string? ListId { get; set; }

        public DateTime? TimeCreated { get; set; }

        public DateTime? TimeModified { get; set; }

        public int? DefaultMarginPercent { get; set; }

        public int? DefaultMarkupPercent { get; set; }

        public string? DepartmentCode { get; set; }

        public string? TaxCode { get; set; }

        public string? DepartmentName { get; set; }

        public string? StoreExchangeStatus { get; set; }

        public string? Image { get; set; }
    }
}
