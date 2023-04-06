namespace azara.models.Entities
{
    public class MasterRolesEntity : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string DisplayName { get; set; }
    }
}
