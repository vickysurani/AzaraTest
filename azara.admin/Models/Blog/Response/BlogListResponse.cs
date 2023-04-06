namespace azara.admin.Models.Blog.Response;

public class BlogListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int Offset { get; set; }

    public List<BlogListData> Details { get; set; }
}

public class BlogListData
{
    public int SrNo { get; set; }

    public string image { get; set; }

    public string title { get; set; }

    public DateTime publishedDate { get; set; }

    public string authorName { get; set; }

    public string descriptions { get; set; }

    public DateTime created { get; set; }

    public string id { get; set; }
}