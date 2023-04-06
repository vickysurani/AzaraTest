namespace azara.models.Requests.Chat;

public class AdminChatInsertRequest : BaseIdRequest
{
    public Guid AdminId { get; set; }

    public Guid UserId { get; set; }

    public string Message { get; set; }
}