using azara.models.Responses.POSDepartment;

namespace azara.admin.Models.Poscategory.Response
{
    public class PosCategoryListResponse
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int offSet { get; set; }
        public List<PosCategoryData> details { get; set; }
    }
}

public class PosCategoryData
{
    public int srno { get; set; }
    public List<string>? ImageList { get; set; }
    public int id { get; set; }
    public string listId { get; set; }
    public DateTime? timeCreated { get; set; }
    public DateTime timeModified { get; set; }
    public int defaultMarginPercent { get; set; }
    public int defaultMarkupPercent { get; set; }
    public string departmentCode { get; set; }
    public string taxCode { get; set; }
    public string departmentName { get; set; }
    public string storeExchangeStatus { get; set; }
    public string image { get; set; }
    public object departmentList { get; set; }
    public bool? IsActive { get; set; }

    public List<DepartmentGetByIdResponse>? DepartmentGetByIdResponse { get; set; }
}

public class POSInventortData
{
    public int Id { get; set; }

    public string? Desc1 { get; set; }

    public string? DepartmentCode { get; set; }

    public decimal? Cost { get; set; }

    public string? Keyword { get; set; }

    public decimal? QuantityOnHand { get; set; }

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

    public decimal? Price1 { get; set; }
    public decimal? Price2 { get; set; }
    public decimal? Price3 { get; set; }
    public decimal? Price4 { get; set; }
    public decimal? Price5 { get; set; }
    public bool? IsActive { get; set; }

}
