namespace azara.models.Entities
{
    public class ProductRequestEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }

        public Guid? StoreId { get; set; }

        [ForeignKey("StoreId")]
        public StoreEntity StoreEntity { get; set; }
    }
}
