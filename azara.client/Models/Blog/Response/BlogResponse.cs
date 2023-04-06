namespace azara.client.Models.Blog.Response;

public class BlogListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<BlogDataRetrive> Details { get; set; }
}

public class BlogDataRetrive
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string Title { get; set; }

    public DateTime PublishedDate { get; set; }

    public string AuthorName { get; set; }

    public string Descriptions { get; set; }

    public DateTime Created { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescriptions { get; set; }
}