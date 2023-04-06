namespace azara.models.Entities
{
    public class AboutUsEntity : BaseEntity
    {
        [Key, Required]
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
