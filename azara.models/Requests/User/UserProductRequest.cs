namespace azara.models.Requests.User;

public class UserProductRequest
{
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public Guid? UserId { get; set; }

    public Guid? StoreId { get; set; }
}