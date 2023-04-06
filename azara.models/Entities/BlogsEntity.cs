namespace azara.models.Entities;

public class BlogsEntity : BaseEntity
{
    [Required]
    public string Image { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }

    [Required]
    public string AuthorName { get; set; }

    public string Descriptions { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescriptions { get; set; }
}