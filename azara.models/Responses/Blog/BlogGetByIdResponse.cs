namespace azara.models.Responses.Blog;

public class BlogGetByIdResponse
{
    public string Image { get; set; }

    public string Title { get; set; }

    public DateTime PublishedDate { get; set; }

    public string AuthorName { get; set; }

    public string Descriptions { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescriptions { get; set; }

    public DateTime Created { get; set; }
}