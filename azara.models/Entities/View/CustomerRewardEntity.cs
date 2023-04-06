namespace azara.models.Entities.View;

public class CustomerRewardEntity
{
    public string ListID { get; set; }

    public DateTime TimeCreated { get; set; }

    public DateTime TimeModified { get; set; }

    public decimal? AccountBalance { get; set; }

    public string? AccountLimit { get; set; }

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

    public bool IsAcceptingChecks { get; set; }

    public bool IsNoShipToBilling { get; set; }

    public bool IsOkToEMail { get; set; }

    public bool IsRewardsMember { get; set; }

    public bool IsUsingChargeAccount { get; set; }

    public bool IsUsingWithQB { get; set; }

    public string? LastName { get; set; }

    public string? LastSale { get; set; }

    public string? Notes { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public string? Phone3 { get; set; }

    public string? Phone4 { get; set; }

    public int? PriceLevelNumber { get; set; }

    public string? Salutation { get; set; }

    public string? StoreExchangeStatus { get; set; }

    public string? TaxCategory { get; set; }

    public int? WebNumber { get; set; }

    public string? BillAddressCity { get; set; }

    public string? BillAddressCountry { get; set; }

    public int? BillAddressPostalCode { get; set; }

    public string? BillAddressState { get; set; }

    public string? BillAddressStreet { get; set; }

    public string? BillAddressStreet2 { get; set; }

    public int? RewardSeqNo { get; set; }

    public decimal RewardAmount { get; set; }

    public decimal? RewardPercent { get; set; }

    public DateTime? RewardEarnedDate { get; set; }

    public DateTime? RewardMatureDate { get; set; }

    public DateTime? RewardExpirationDate { get; set; }

    public string? RewardStatus { get; set; }

    public string? FQPrimaryKey { get; set; }

    public string? CustomFieldOther { get; set; }
}