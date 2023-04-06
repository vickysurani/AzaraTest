namespace azara.api.Helpers;

public class ContactUsHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public ContactUsHelpers(
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

    #region 1. Insert Contact

    public async Task<BaseResponse> ContactUs([FromBody] ContactUsAddRequest request)
    {
        await DbContext.AddRangeAsync(new ContactUsEntity
        {
            Name = request.Name,
            EmailId = request.EmailId,
            Description = request.Description
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Contact

    #region 2. Contact Get By Id

    public async Task<ContactUsGetByIdResponse> ContactUsGetById([FromBody] BaseRequiredIdRequest request)
    {
        var contact = await DbContext.ContactUs.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (contact == null)
            throw new ApiException("error_contact_not_found");

        var response = new ContactUsGetByIdResponse
        {
            Name = contact.Name,
            EmailId = contact.EmailId,
            Description = contact.Description
        };

        return response;
    }

    #endregion 2. Contact Get By Id

    #region 3. Get Contact List

    public async Task<PaginationResponse> ContactGetList([FromBody] ContactGetListRequest request)
    {
        var contactList = (from C in DbContext.ContactUs

                           where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                               || C.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                               && C.Deleted.Equals(request.IsDeleted)

                           select new
                           {
                               Id = C.Id,
                               Name = C.Name,
                               EmailId = C.EmailId,
                               Description = C.Description,
                               Modified = C.Modified
                           }).OrderByDescending(x => x.Modified);

        if (contactList == null)
            throw new ApiException("error_contact_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    contactList = contactList.OrderByDescending(x => x.Name);
                else
                    contactList = contactList.OrderBy(x => x.Name);
                break;

            default:

                contactList = contactList.OrderByDescending(x => x.Modified);
                break;
        }

        var total = contactList.Count();
        var totalPages = CalculateTotalPages(total, request.PageSize);
        var contactPaginationList = contactList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = contactPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 3. Get Contact List

    #region 4. Contact List Without Pagination

    public async Task<List<ContactUsListResponse>> ContactUsList([FromBody] ContactListRequest request)
    {
        var contactList = (from C in DbContext.ContactUs

                           where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                               || C.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                               && C.Deleted.Equals(request.IsDeleted)

                           select new ContactUsListResponse
                           {
                               Id = C.Id,
                               Name = C.Name,
                               EmailId = C.EmailId,
                               Description = C.Description,
                               Modified = C.Modified
                           }).OrderByDescending(x => x.Modified).ToList();

        if (contactList == null)
            throw new ApiException("error_contact_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    contactList = contactList.OrderByDescending(x => x.Name).ToList();
                else
                    contactList = contactList.OrderBy(x => x.Name).ToList();
                break;

            default:

                contactList = contactList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return contactList;
    }

    #endregion 4. Contact List Without Pagination

    #region 5. Delete Contact

    public async Task<BaseResponse> ContactDelete([FromBody] BaseIdRequest request)
    {
        var contact = DbContext.ContactUs.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (contact == null)
            throw new ApiException("error_contact_not_found");

        if (contact.Deleted == false)
        {
            contact.Deleted = true;
        }

        contact.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Delete Contact

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}