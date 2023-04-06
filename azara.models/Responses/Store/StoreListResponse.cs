namespace azara.models.Responses.Store;

public class StoreListResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Location { get; set; }

    public string Address { get; set; }

    public string EmailId { get; set; }

    public string ContactNumber { get; set; }

    public string Description { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public bool IsLocationFound { get; set; } = false;

    public DateTime Modified { get; set; }

    public DateTime Created { get; set; }

    public bool Active { get; set; }

    public string Code { get; set; }
}