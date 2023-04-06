namespace azara.client.Models.ShopProduct.Request
{
    public class PosByIdRequest
    {
        public string Id { get; set; }

        public string? LocationDetail { get; set; }
    }

    public class PosInventoryImageUpdateRequest
    {

        public string image { get; set; }
        public string Id { get; set; }
    }
}
