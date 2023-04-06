namespace azara.models.Entities.POSSales
{
    public class LastSalesDate
    {
        [Key, Required]
        public int Id { get; set; }
        public DateTime? TimeCreated { get; set; }
    }
}
