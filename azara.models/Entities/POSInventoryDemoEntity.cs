namespace azara.models.Entities
{
    public class POSInventoryDemoEntity
    {
        [Key, Required]
        public int Id { get; set; }

        public string? ListId { get; set; }

        public DateTime? TimeCreated { get; set; }

        public DateTime? TimeModified { get; set; }

        public string? ALU { get; set; }

        public string? Attribute { get; set; }

        public string? COGSAccount { get; set; }

        public decimal? Cost { get; set; }

        public string? DepartmentCode { get; set; }

        public string? DepartmentListID { get; set; }

        public string? Desc1 { get; set; }

        public string? Description { get; set; }

        public string? IncomeAccount { get; set; }

        public bool? IsBelowReorder { get; set; }

        public bool? IsEligibleForCommission { get; set; }

        public bool? IsPrintingTags { get; set; }

        public bool? IsUnorderable { get; set; }

        public int ItemNumber { get; set; }

        public string? ItemType { get; set; }

        public string? Keywords { get; set; }

        public DateTime? LastReceived { get; set; }

        public int MarginPercent { get; set; }

        public int MarkupPercent { get; set; }

        public string? MSRP { get; set; }

        public decimal? OnHandStore01 { get; set; }

        public decimal? OnHandStore02 { get; set; }
        public decimal? OnHandStore03 { get; set; }
        public decimal? OnHandStore04 { get; set; }
        public decimal? OnHandStore05 { get; set; }
        public decimal? OnHandStore06 { get; set; }
        public decimal? OnHandStore07 { get; set; }
        public decimal? OnHandStore08 { get; set; }
        public decimal? OnHandStore09 { get; set; }
        public decimal? OnHandStore10 { get; set; }
        public decimal? OnHandStore11 { get; set; }
        public decimal? OnHandStore12 { get; set; }
        public decimal? OnHandStore13 { get; set; }
        public decimal? OnHandStore14 { get; set; }
        public decimal? OnHandStore15 { get; set; }
        public decimal? OnHandStore16 { get; set; }
        public decimal? OnHandStore17 { get; set; }
        public decimal? OnHandStore18 { get; set; }
        public decimal? OnHandStore19 { get; set; }
        public decimal? OnHandStore20 { get; set; }

        public string? OrderByUnit { get; set; }

        public decimal? OrderCost { get; set; }

        public decimal? Price1 { get; set; }

        public decimal? Price2 { get; set; }

        public decimal? Price3 { get; set; }

        public decimal? Price4 { get; set; }

        public decimal? Price5 { get; set; }

        public decimal? QuantityOnCustomerOrder { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public decimal? QuantityOnOrder { get; set; }

        public decimal? QuantityOnPendingOrder { get; set; }

        public decimal? ReorderPoint { get; set; }

        public decimal? ReorderPointStore01 { get; set; }

        public string? SellByUnit { get; set; }

        public string? SerialFlag { get; set; }

        public string? Size { get; set; }

        public string? StoreExchangeStatus { get; set; }

        public string? UnitOfMeasure { get; set; }

        public string? UPC { get; set; }

        public string? VendorCode { get; set; }

        public string? VendorListID { get; set; }
        public string? WebDesc { get; set; }
        public decimal? WebPrice { get; set; }
        public string? Manufacturer { get; set; }
        public decimal Weight { get; set; }
        public string? WebSKU { get; set; }
        public string? WebCategories { get; set; }
        public string? UnitOfMeasure1ALU { get; set; }
        public decimal? UnitOfMeasure1MSRP { get; set; }
        public decimal? UnitOfMeasure1NumberOfBaseUnits { get; set; }
        public decimal? UnitOfMeasure1Price1 { get; set; }
        public decimal? UnitOfMeasure1Price2 { get; set; }
        public decimal? UnitOfMeasure1Price3 { get; set; }
        public decimal? UnitOfMeasure1Price4 { get; set; }
        public decimal? UnitOfMeasure1Price5 { get; set; }
        public string? UnitOfMeasure1UnitOfMeasure { get; set; }
        public string? UnitOfMeasure1UPC { get; set; }
        public string? UnitOfMeasure2ALU { get; set; }
        public decimal? UnitOfMeasure2MSRP { get; set; }
        public decimal? UnitOfMeasure2NumberOfBaseUnits { get; set; }
        public decimal? UnitOfMeasure2Price1 { get; set; }
        public decimal? UnitOfMeasure2Price2 { get; set; }
        public decimal? UnitOfMeasure2Price3 { get; set; }
        public decimal? UnitOfMeasure2Price4 { get; set; }
        public decimal? UnitOfMeasure2Price5 { get; set; }
        public string? UnitOfMeasure2UnitOfMeasure { get; set; }
        public string? UnitOfMeasure2UPC { get; set; }
        public string? UnitOfMeasure3ALU { get; set; }
        public decimal? UnitOfMeasure3MSRP { get; set; }
        public decimal? UnitOfMeasure3NumberOfBaseUnits { get; set; }
        public decimal? UnitOfMeasure3Price1 { get; set; }
        public decimal? UnitOfMeasure3Price2 { get; set; }
        public decimal? UnitOfMeasure3Price3 { get; set; }
        public decimal? UnitOfMeasure3Price4 { get; set; }
        public decimal? UnitOfMeasure3Price5 { get; set; }
        public string? UnitOfMeasure3UnitOfMeasure { get; set; }
        public string? UnitOfMeasure3UPC { get; set; }
        public string? VendorInfo2ALU { get; set; }
        public decimal? VendorInfo2OrderCost { get; set; }
        public string? VendorInfo2UPC { get; set; }
        public string? VendorInfo2VendorListID { get; set; }
        public string? VendorInfo3ALU { get; set; }
        public decimal? VendorInfo3OrderCost { get; set; }
        public string? VendorInfo3UPC { get; set; }
        public string? VendorInfo3VendorListID { get; set; }
        public string? VendorInfo4ALU { get; set; }
        public decimal? VendorInfo4OrderCost { get; set; }
        public string? VendorInfo4UPC { get; set; }
        public string? VendorInfo4VendorListID { get; set; }
        public string? VendorInfo5ALU { get; set; }
        public decimal? VendorInfo5OrderCost { get; set; }
        public string? VendorInfo5UPC { get; set; }
        public string? VendorInfo5VendorListID { get; set; }

        public string? Image { get; set; }
    }
}
