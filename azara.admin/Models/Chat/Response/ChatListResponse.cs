namespace azara.admin.Models.Chat.Response;

public class ChatListResponse
{
    public int Total { get; set; }

    public int TotalPages { get; set; }

    public int OffSet { get; set; }

    public List<Chatdata> Details { get; set; }
}

public class Chatdata
{
    public int SrNo { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public string UserImage { get; set; }

    public string UserLastMessage { get; set; }

    public int UnReadMessageCount { get; set; }

    public DateTime LastMessageTime { get; set; }
}