namespace azara.models.Responses.Chat;

public class ChatGetByIdResponse
{
    public Guid UserId { get; set; }

    public Guid AdminId { get; set; }

    public List<ChatDetail> ChatDetails { get; set; }

    public bool IsNewConversation { get; set; }
}

public class ChatDetail
{
    public string Message { get; set; }

    public bool IsUserMessage { get; set; }

    public DateTime Created { get; set; }
}