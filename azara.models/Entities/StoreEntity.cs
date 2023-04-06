namespace azara.models.Entities;

public class StoreEntity : BaseEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; }

    public string? Image { get; set; }

    public string? Location { get; set; }

    [Required, MaxLength(500)]
    public string Address { get; set; }

    [Required, MaxLength(250)]
    public string? EmailId { get; set; }

    [Required, MaxLength(20)]
    public string? ContactNumber { get; set; }

    public string? Description { get; set; }

    [Required]
    public string? Latitude { get; set; }

    [Required]
    public string? Longitude { get; set; }

    [Required]
    public string? Code { get; set; }
}