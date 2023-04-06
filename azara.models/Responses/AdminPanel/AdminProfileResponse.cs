namespace azara.models.Responses.AdminPanel;

public class AdminProfileResponse : BaseIdResponse
{
    public string Name { get; set; }

    public string EmailId { get; set; }

    public string Mobile { get; set; }

    public string? ProfilePhoto { get; set; }
}