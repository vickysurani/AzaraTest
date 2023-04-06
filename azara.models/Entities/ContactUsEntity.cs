namespace azara.models.Entities;

public class ContactUsEntity : BaseEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, MaxLength(250)]
    public string EmailId { get; set; }

    public string Description { get; set; }
}