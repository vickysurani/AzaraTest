namespace azara.admin.Models.ContactRequest.Response;

public class ContactResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<ContactData> Details { get; set; }
}

public class ContactData
{
    public int SrNo { get; set; }

    public string Id { get; set; }

    public string Name { get; set; }

    public string EmailId { get; set; }

    public string Description { get; set; }
}