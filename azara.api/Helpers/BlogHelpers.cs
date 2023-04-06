namespace azara.api.Helpers;

public class BlogHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public BlogHelpers(
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

    #region 1. Insert blog

    public async Task<BaseResponse> BlogInsert([FromBody] BlogInsertRequest request)
    {
        if (DbContext.Blogs.Any(x => x.Title.ToLower().Equals(request.Title.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_title_exist" });

        await DbContext.AddRangeAsync(new BlogsEntity
        {
            Image = request.Image,
            Title = request.Title,
            PublishedDate = request.PublishedDate,
            AuthorName = request.AuthorName,
            Descriptions = request.Descriptions,
            Created = DateTime.UtcNow,
            MetaTitle = request.Title,//request.MetaTitle,
            MetaDescriptions = request.Descriptions//request.MetaDescriptions
        }); 

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert blog

    #region 2. Update Blog

    public async Task<BlogUpdateResponse> BlogUpdate([FromBody] BlogUpdateRequest request)
    {
        var blog = DbContext.Blogs.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (blog == null)
            throw new ApiException("error_blog_not_found");

        if (!blog.Title.Equals(request.Title) && DbContext.Blogs.Any(x => x.Title.ToLower().Equals(request.Title.ToLower())))
            throw new ApiException("error_blog_name_exists");

        blog.Image = request.Image ?? blog.Image;
        blog.Title = request.Title ?? blog.Title;
        blog.PublishedDate = request.PublishedDate;
        blog.AuthorName = request.AuthorName ?? blog.AuthorName;
        blog.Descriptions = request.Descriptions ?? blog.Descriptions;
        blog.MetaTitle = request.Title ?? blog.Title;//request.MetaTitle ?? blog.MetaTitle;
        blog.MetaDescriptions = request.Descriptions ?? blog.Descriptions;//request.MetaDescriptions ?? blog.MetaDescriptions;
        blog.Created = request.Created;
        blog.Modified = DateTime.UtcNow;

        var response = new BlogUpdateResponse
        {
            Image = blog.Image,
            Title = blog.Title,
            PublishedDate = blog.PublishedDate,
            AuthorName = blog.AuthorName,
            Descriptions = blog.Descriptions,
            MetaTitle = blog.MetaTitle,
            MetaDescriptions = blog.MetaDescriptions,
            Created = blog.Created
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Blog

    #region 3. Blog Get By Id

    public async Task<BlogGetByIdResponse> BlogGetById([FromBody] BaseRequiredIdRequest request)
    {
        var blog = await DbContext.Blogs.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (blog == null)
            throw new ApiException("error_blog_not_found");

        var response = new BlogGetByIdResponse
        {
            Image = blog.Image,
            Title = blog.Title,
            PublishedDate = blog.PublishedDate,
            AuthorName = blog.AuthorName,
            Descriptions = blog.Descriptions,
            MetaTitle = blog.MetaTitle,
            MetaDescriptions = blog.MetaDescriptions,
            Created = DateTime.UtcNow
        };

        return response;
    }

    #endregion 3. Blog Get By Id

    #region 4. Get Blog List

    public async Task<PaginationResponse> BlogGetList([FromBody] BlogGetListRequest request)
    {
        var blogList = (from B in DbContext.Blogs
                        where

                        (string.IsNullOrWhiteSpace(request.Title.ToString().ToLower()) ||
                        B.Title.ToLower().Contains(request.Title.ToString().ToLower())) &&

                        !B.Deleted
                        select new BlogListResponse
                        {
                            Id = B.Id,
                            Image = B.Image,
                            Title = B.Title,
                            PublishedDate = B.PublishedDate,
                            AuthorName = B.AuthorName,
                            Descriptions = B.Descriptions,
                            MetaTitle = B.MetaTitle,
                            MetaDescriptions = B.MetaDescriptions,
                            Created = B.Created,
                            Modified = B.Modified
                        }).OrderByDescending(x => x.Created);

        if (blogList == null)
            throw new ApiException("error_blog_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.Title);
                else
                    blogList = blogList.OrderBy(x => x.Title);
                break;

            case "author_name":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.AuthorName);
                else
                    blogList = blogList.OrderBy(x => x.AuthorName);
                break;

            case "published_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.PublishedDate);
                else
                    blogList = blogList.OrderBy(x => x.PublishedDate);
                break;

            default:

                blogList = blogList.OrderByDescending(x => x.Modified);
                break;
        }

        var total = blogList.Count();
        var totalPages = BlogHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = blogList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Blog List

    #region 5. Blog List Without Pagination

    public async Task<List<BlogListResponse>> BlogList([FromBody] BlogListRequest request)
    {
        var blogList = (from B in DbContext.Blogs
                        where

                        (string.IsNullOrWhiteSpace(request.Title.ToString().ToLower()) ||
                        B.Title.ToLower().Contains(request.Title.ToString().ToLower())) &&

                        !B.Deleted
                        select new BlogListResponse
                        {
                            Id = B.Id,
                            Image = B.Image,
                            Title = B.Title,
                            PublishedDate = B.PublishedDate,
                            AuthorName = B.AuthorName,
                            Descriptions = B.Descriptions,
                            MetaTitle = B.MetaTitle,
                            MetaDescriptions = B.MetaDescriptions,
                            Created = B.Created,
                            Modified = B.Modified
                        }).OrderByDescending(x => x.Created).ToList();

        if (blogList == null)
            throw new ApiException("error_blog_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "title":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.Title).ToList();
                else
                    blogList = blogList.OrderBy(x => x.Title).ToList();
                break;

            case "author_name":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.AuthorName).ToList();
                else
                    blogList = blogList.OrderBy(x => x.AuthorName).ToList();
                break;

            case "published_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    blogList = blogList.OrderByDescending(x => x.PublishedDate).ToList();
                else
                    blogList = blogList.OrderBy(x => x.PublishedDate).ToList();
                break;

            default:

                blogList = blogList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return blogList;
    }

    #endregion 5. Blog List Without Pagination

    #region 6. Delete Blog

    public async Task<BaseResponse> BlogDelete([FromBody] BaseIdRequest request)
    {
        var blog = DbContext.Blogs.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (blog == null)
            throw new ApiException("error_blog_not_found");

        if (blog.Deleted == false)
        {
            blog.Deleted = true;
        }

        blog.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Blog

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}