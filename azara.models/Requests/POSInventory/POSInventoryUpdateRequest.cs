namespace azara.models.Requests.POSInventory
{
    public class POSInventoryUpdateRequest
    {
        public int Id { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public DateTime? TimeCreated { get; set; }
    }
}
