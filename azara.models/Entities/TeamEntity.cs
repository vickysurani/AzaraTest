namespace azara.models.Entities
{
    public class TeamEntity : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string? PermissionJson { get; set; }

        [Required]
        public int CurrentRevision { get; set; }

        [Required]
        public Guid? ModifiedBy { get; set; }
    }
}
