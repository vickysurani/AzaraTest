namespace azara.client.Models.Store.Response
{
    public class StoreGetResponse
    {
        public List<StoreListResponseDataGet> Details { get; set; }

    }

    public class StoreListResponseDataGet
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        public bool IsLocationFound { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
