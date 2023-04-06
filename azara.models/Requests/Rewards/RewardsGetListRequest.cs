namespace azara.models.Requests.Rewards;

public class RewardsGetListRequest : PaginationRequest
{
    public string Name { get; set; } = string.Empty;

    public bool? IsDeleted { get; set; } = false;

    public bool IsDisplayActive { get; set; }

    public string? SortBy { get; set; } = string.Empty;

    public Guid? UserId { get; set; }
}