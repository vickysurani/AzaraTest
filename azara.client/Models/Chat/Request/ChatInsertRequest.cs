namespace azara.client.Models.Chat.Request;

public class ChatInsertRequest
{
    public Guid UserId { get; set; }

    public string Message { get; set; }

    public Guid? AdminId { get; set; }
}