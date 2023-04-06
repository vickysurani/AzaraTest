namespace azara.models.Requests.Offers;

public class OffersInsertUpdateRequest
{
    public string? Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string Amount { get; set; }

    public bool Active { get; set; }
}