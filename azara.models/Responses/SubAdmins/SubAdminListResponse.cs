namespace azara.models.Responses.SubAdmins;

public class SubAdminListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int Offset { get; set; }

    public ICollection<SubAdminDetailResponse> Details { get; set; }
}

public class SubAdminDetailResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string EmailId { get; set; }

    public string UserName { get; set; }

    public string Mobile { get; set; }

    public bool Active { get; set; }

    public bool Deleted { get; set; }

    public DateTime? Modified { get; set; }
}

