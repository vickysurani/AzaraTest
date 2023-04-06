namespace azara.client.Models.Chat.Response;

public class ChatGetByIdResponse
{
    public string UserId { get; set; }

    public string AdminId { get; set; }

    public List<ChatDetail> chatDetails { get; set; }
}

public class ChatDetail
{
    public string Message { get; set; }

    public bool IsUserMessage { get; set; }

    public DateTime Created { get; set; }

    public int UnseenCount { get; set; }


}

public class ChatReceive
{
    public string Message { get; set; }
    public Guid Id { get; set; }
    public int ReceiveUnseenCount { get; set; }





}