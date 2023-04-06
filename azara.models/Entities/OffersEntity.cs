namespace azara.models.Entities;

public class OffersEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Image { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }
}