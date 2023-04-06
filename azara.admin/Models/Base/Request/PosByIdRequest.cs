namespace azara.admin.Models.Base.Request
{
    public class PosByIdRequest
    {
        public string Id { get; set; }
    }

    public class PosInventoryImageUpdateRequest
    {

        public string image { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

    public class PosByIntIdRequest
    {
        public int Id { get; set; }
    }


}
