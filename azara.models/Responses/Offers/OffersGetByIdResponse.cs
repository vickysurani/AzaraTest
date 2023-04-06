namespace azara.models.Responses.Offers;

public class OffersGetByIdResponse
{
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public bool Active { get; set; }
}