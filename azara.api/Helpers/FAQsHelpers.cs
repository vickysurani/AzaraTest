namespace azara.api.Helpers;

public class FAQsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public FAQsHelpers(
        AzaraContext DbContext,
        ICrypto Crypto)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
    }

    #endregion Constructor

    #region Calculate Total Pages

    public static int CalculateTotalPages(
        int total,
        int? pageSize)
    {
        var pages = Convert.ToDecimal(total) / Convert.ToDecimal(pageSize);
        var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

        return response;
    }

    #endregion Calculate Total Pages

    #region 1. Insert FAQs

    public async Task<BaseResponse> FAQsInsert([FromBody] FAQsInsertUpdateRequest request)
    {
        if (DbContext.FAQs.Any(x => x.Question.ToLower().Equals(request.Question.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_question_exist" });

        await DbContext.AddRangeAsync(new FAQsEntity
        {
            Question = request.Question,
            Answer = request.Answer
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert FAQs

    #region 2. Update FAQs

    public async Task<FAQsUpdateResponse> FAQsUpdate([FromBody] FAQsInsertUpdateRequest request)
    {
        var faqs = DbContext.FAQs.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (faqs == null)
            throw new ApiException("error_FAQs_not_found");

        faqs.Question = request.Question ?? faqs.Question;
        faqs.Answer = request.Answer ?? faqs.Answer;

        var response = new FAQsUpdateResponse
        {
            Question = faqs.Question,
            Answer = faqs.Answer
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update FAQs

    #region 3. Get FAQs List

    public async Task<PaginationResponse> FAQsGetList([FromBody] FAQsGetListRequest request)
    {
        var faqsList = await (
            from F in DbContext.FAQs

            where (string.IsNullOrWhiteSpace(request.Question.ToString().ToLower())
                || F.Question.ToLower().Contains(request.Question.ToString().ToLower()))

                && F.Deleted.Equals(request.IsDeleted)

            select new
            {
                Id = F.Id,
                Question = F.Question,
                Answer = F.Answer
            }).ToListAsync();

        if (faqsList == null)
            throw new ApiException("error_FAQs_not_found");

        var total = faqsList.Count();
        var totalPages = FAQsHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = faqsList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 3. Get FAQs List

    #region 4. Get FAQs List

    public async Task<List<FAQsListResponse>> FAQsList([FromBody] FAQsListRequest request)
    {
        var faqsList = await (
            from F in DbContext.FAQs

            where (string.IsNullOrWhiteSpace(request.Question.ToString().ToLower())
                || F.Question.ToLower().Contains(request.Question.ToString().ToLower()))

                && F.Deleted.Equals(request.IsDeleted)

            select new FAQsListResponse
            {
                Id = F.Id,
                Question = F.Question,
                Answer = F.Answer
            }).ToListAsync();

        if (faqsList == null)
            throw new ApiException("error_FAQs_not_found");

        return faqsList;
    }

    #endregion 4. Get FAQs List

    #region 5. FAQs Get By Id

    public async Task<FAQsGetByIdResponse> FAQsGetById([FromBody] BaseRequiredIdRequest request)
    {
        var faqs = await DbContext.FAQs.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (faqs == null)
            throw new ApiException("error_FAQs_not_found");

        var response = new FAQsGetByIdResponse
        {
            Question = faqs.Question,
            Answer = faqs.Answer
        };

        return response;
    }

    #endregion 5. FAQs Get By Id

    #region 6. Delete FAQs

    public async Task<BaseResponse> FAQsDelete([FromBody] BaseIdRequest request)
    {
        var faqs = DbContext.FAQs.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (faqs == null)
            throw new ApiException("error_FAQs_not_found");

        if (faqs.Deleted == false)
        {
            faqs.Deleted = true;
        }

        faqs.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete FAQs

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}