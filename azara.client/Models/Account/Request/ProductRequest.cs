namespace azara.client.Models.Account.Request;

public class ProductRequest
{
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string? UserId { get; set; }
}