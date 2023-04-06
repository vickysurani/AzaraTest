namespace azara.models.Responses.Chat;

public class ChatListResponse
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string UserImage { get; set; }

    public string UserLastMessage { get; set; }

    public int UnReadMessageCount { get; set; }

    public DateTime LastMessageTime { get; set; }
}