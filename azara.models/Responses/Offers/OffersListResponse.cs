namespace azara.models.Responses.Offers;

public class OffersListResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime Modified { get; set; }

    public bool Active { get; set; }
}