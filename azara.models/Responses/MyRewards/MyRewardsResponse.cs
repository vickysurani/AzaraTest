namespace azara.models.Responses.MyRewards;

public class MyRewardsResponse
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public string Image { get; set; }

    public Guid Barcode { get; set; }
}