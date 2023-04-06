public class POSInventoryResponse
{
    public List<ItemInventoryViewEntity> Details { get; set; }
}
public class ItemInventoryViewEntity
{
    //public decimal? Cost { get; set; }

    public string? DepartmentCode { get; set; }

    public string Desc1 { get; set; }

    public string Desc2 { get; set; }

    //public decimal? Price1 { get; set; }

    //public decimal? Price2 { get; set; }
}