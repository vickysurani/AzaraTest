namespace azara.models.Entities;

public class DealsEntity : BaseEntity
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string BannerImage { get; set; }

    [Required]
    public string URL { get; set; }

    [Required]
    public int Position { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string StoreId { get; set; }
}