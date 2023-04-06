namespace azara.admin.Models.Product.Request
{
    public class PosInventoryGetListRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string SearchParam { get; set; }

        public string? OrderBy { get; set; }
    }
}
