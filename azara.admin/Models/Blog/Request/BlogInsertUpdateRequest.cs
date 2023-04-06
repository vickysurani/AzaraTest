namespace azara.admin.Models.Blog.Request;

public class BlogInsertUpdateRequest : BaseIdRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_image_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Image { get; set; }

    [Required(ErrorMessageResourceName = "error_title_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessageResourceName = "error_published_date_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public DateTime PublishedDate { get; set; }

    [Required(ErrorMessageResourceName = "error_author_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string AuthorName { get; set; }

    public string? Descriptions { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescriptions { get; set; }

    public List<string>? ImageList { get; set; }

    public DateTime Created { get; set; }
}