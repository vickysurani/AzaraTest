namespace azara.api.Helpers;

public class ChatHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public ChatHelpers(
        AzaraContext DbContext,
        ICrypto Crypto)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
    }

    #endregion Constructor

    public static int CalculateTotalPages(
        int total,
        int? pageSize)
    {
        var pages = Convert.ToDecimal(total) / Convert.ToDecimal(pageSize);
        var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

        return response;
    }

    #region 1. User Insert Chat

    public async Task<BaseResponse> ChatInsert([FromBody] UserChatInsertRequest request)
    {
        await DbContext.AddRangeAsync(new ChatEntity
        {
            UserId = request.UserId,
            Message = request.Message,
            Created = DateTime.UtcNow,
            AdminId = request.AdminId
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. User Insert Chat

    #region 2. Chat Get By Id

    public async Task<ChatGetByIdResponse> ChatGetById(BaseRequiredIdRequest request)
    {
        var chatData = await DbContext.Chat.FirstOrDefaultAsync(x => x.UserId.Equals(request.Id));

        var userData = await DbContext.User.FirstOrDefaultAsync(f => f.Id.Equals(request.Id));

        if (chatData == null && userData == null)
            throw new ApiException("error_chat_not_found");

        var chatList = await DbContext.Chat
            .Where(u => u.UserId.Equals(request.Id) && !u.Deleted && u.Active)
            .OrderBy(o => o.Created).ToListAsync();

        if (chatList.Any(a => !a.IsAdminRead || !a.IsUserRead))
        {
            if (request.IsAdmin)
                chatList.ForEach(f => f.IsAdminRead = true);
            else
                chatList.ForEach(f => f.IsUserRead = true);

            DbContext.UpdateRange(chatList);
            await DbContext.SaveChangesAsync();
        }

        return new ChatGetByIdResponse
        {
            UserId = chatData?.UserId ?? new Guid(),
            AdminId = chatData?.AdminId ?? new Guid(),
            ChatDetails = chatList.Select(s => new ChatDetail
            {
                Message = s.Message,
                IsUserMessage = !s.AdminId.HasValue,
                Created = s.Created
            }).ToList(),
            IsNewConversation = chatData == null || chatList.Count == 0
        };
    }

    #endregion 2. Chat Get By Id

    #region 3. Chat List

    public async Task<PaginationResponse> GetChatListAsync(ChatListRequest request)
    {
        var chatList = await DbContext.Chat
                        .Where(w => !w.Deleted
                            && (string.IsNullOrEmpty(request.Name)
                            || request.Name.ToLower().Contains(w.UserEntity.FirstName)
                            || request.Name.ToLower().Contains(w.UserEntity.LastName))) //&& w.UserEntity.EmailVerified)
                        .GroupBy(g => g.UserId)
                        .Select(s => new ChatListResponse
                        {
                            UserId = s.Key.Value,
                            UserName = s.FirstOrDefault().UserEntity.FirstName + " " + s.FirstOrDefault().UserEntity.LastName,
                            UserImage = s.FirstOrDefault().UserEntity.Image,
                            UserLastMessage = s.OrderByDescending(o => o.Created).FirstOrDefault().Message,
                            UnReadMessageCount = s.Where(w => !w.AdminId.HasValue && !w.Deleted).Count(c => !c.IsAdminRead),
                            LastMessageTime = s.OrderByDescending(o => o.Created).FirstOrDefault().Created
                        })
                        .OrderByDescending(o => o.LastMessageTime)
                        .ToListAsync();

        var userList = await DbContext.User.Where(w => !chatList.Select(s => s.UserId).Contains(w.Id) && !w.Deleted
                        && (string.IsNullOrEmpty(request.Name)
                                            || request.Name.ToLower().Contains(w.FirstName)
                                            || request.Name.ToLower().Contains(w.LastName)))//&& w.EmailVerified)
                        .Select(s => new ChatListResponse
                        {
                            UserId = s.Id,
                            UserImage = s.Image,
                            UserName = s.FirstName + s.LastName
                        }).ToListAsync();

        if (chatList == null)
            throw new ApiException("error_chat_not_found");

        chatList = chatList.Concat(userList).ToList();

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    chatList = chatList.OrderByDescending(x => x.UserName).ToList();
                else
                    chatList = chatList.OrderBy(x => x.UserName).ToList();
                break;

            default:
                break;
        }

        var total = chatList.Count;
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = chatList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = eventPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 3. Chat List

    #region 4. Get Admin Message Count

    public async Task<AdminMessCountResponse> GetAdminMessCount(Guid userId)
    {
        int count = await DbContext.Chat.Where(w => w.UserId.Equals(userId) && !w.Deleted && w.Active && !w.IsUserRead).CountAsync();

        return new AdminMessCountResponse { UnReadMessCount = count };
    }

    #endregion 4. Get Admin Message Count

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}