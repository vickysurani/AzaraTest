namespace azara.admin.Models.User.Response
{
    public class UserPosListResponse
    {
        public int Total { get; set; }

        public int Totalpages { get; set; }

        public int Offset { get; set; }

        public List<UserRootData> Details { get; set; }

    }

    public class UserRootData
    {
        public int SrNo { get; set; }

        public string? ListId { get; set; }

        public DateTime? TimeCreated { get; set; }

        public DateTime? TimeModified { get; set; }

        public decimal? AccountBalance { get; set; }

        public decimal? AccountLimit { get; set; }

        public decimal? AmountPastDue { get; set; }

        public string? CompanyName { get; set; }

        public decimal? CustomerDiscPercent { get; set; }

        public string? CustomerDiscType { get; set; }

        public string? CustomerType { get; set; }

        public string? CustomerID { get; set; }

        public string? DefaultShipAddress { get; set; }

        public string? EMail { get; set; }

        public string? FirstName { get; set; }

        public string? FullName { get; set; }

        public string? description { get; set; }

        public bool? IsAcceptingChecks { get; set; }

        public bool? IsNoShipToBilling { get; set; }

        public bool? IsOkToEMail { get; set; }

        public bool? IsRewardsMember { get; set; }

        public bool? IsUsingChargeAccount { get; set; }

        public bool? IsUsingWithQB { get; set; }

        public string? LastName { get; set; }

        public DateTime? LastSale { get; set; }

        public string? Notes { get; set; }

        public string? Phone { get; set; }

        public string? Phone2 { get; set; }

        public string? Phone3 { get; set; }

        public string? Phone4 { get; set; }

        public string? PriceLevelNumber { get; set; }

        public string? Salutation { get; set; }

        public string? StoreExchangeStatus { get; set; }

        public string? TaxCategory { get; set; }

        public string? WebNumber { get; set; }

        public string? BillAddressCity { get; set; }

        public string? BillAddressCountry { get; set; }

        public string? BillAddressPostalCode { get; set; }

        public string? BillAddressState { get; set; }

        public string? BillAddressStreet { get; set; }

        public string? BillAddressStreet2 { get; set; }

        public string? CustomFieldOther { get; set; }

        public string? CustomFieldSalesLocation { get; set; }

        public string? CustomFieldSerial { get; set; }

        public string? CustomFieldSerial1 { get; set; }

        public string? CustomFieldSerial2 { get; set; }
    }
}
