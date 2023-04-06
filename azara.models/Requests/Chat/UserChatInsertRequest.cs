namespace azara.models.Requests.Chat;

public class UserChatInsertRequest : BaseIdRequest
{
    public Guid UserId { get; set; }

    public string Message { get; set; }

    public Guid? AdminId { get; set; }
}