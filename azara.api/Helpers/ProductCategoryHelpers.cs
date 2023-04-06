namespace azara.api.Helpers;

public class ProductCategoryHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public ProductCategoryHelpers(
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

    #region 1. Insert Product Category

    public async Task<BaseResponse> ProductCategoryInsert([FromBody] ProductCategoryInsertUpdateRequest request)
    {
        if (DbContext.ProductCategories.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

        await DbContext.AddRangeAsync(new ProductCategoryEntity
        {
            Name = request.Name,
            //Active = request.Status,
            Image = request.Image
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Product Category

    #region 2. Update Category Product

    public async Task<ProductCategoryUpdateResponse> ProductCategoryUpdate([FromBody] ProductCategoryInsertUpdateRequest request)
    {
        var product = DbContext.ProductCategories.FirstOrDefault(x => x.Id.Equals(new(request.ProductCategoryId)));

        if (product == null)
            throw new ApiException("error_product_category_not_found");

        // Validation for duplicate Name
        if (!request.Name.ToLower().Equals(product.Name.ToLower()) && DbContext.ProductCategories.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            throw new ApiException("error_product_category_name_exist");

        product.Name = request.Name ?? product.Name;
        //product.Active = request.Status;
        product.Image = request.Image ?? product.Image;

        var response = new ProductCategoryUpdateResponse
        {
            Name = product.Name,
            //Status = product.Active,
            Image = product.Image
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Category Product

    #region 3. Product Category Get By Id

    public async Task<ProductCategoryGetByIdResponse> ProductCategoryGetById([FromBody] BaseIdRequest request)
    {
        var product = await DbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

        if (product == null)
            throw new ApiException("error_product_category_not_found");

        var productSubCategoryList = await DbContext.ProductSubCategory
            .Where(ps => ps.ProductCategoryId == request.Id && !ps.Deleted && ps.Active)
            .Select(s => new ProductSubCategoryDetail
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

        var response = new ProductCategoryGetByIdResponse
        {
            Name = product.Name,
            Status = product.Active,
            Image = product.Image,
            ProductSubCategoryDetail = productSubCategoryList
        };

        return response;
    }

    #endregion 3. Product Category Get By Id

    #region 4. Get Product Category List

    public async Task<PaginationResponse> ProductCategoryGetListAsync([FromBody] ProductCategoryGetListRequest request)
    {
        var productCategoryList = (from PC in DbContext.ProductCategories
                                   where (string.IsNullOrWhiteSpace(request.Name)
                                        || PC.Name.ToLower().Contains(request.Name.ToLower()))

                                        && PC.Deleted.Equals(request.IsDeleted)

                                   select new
                                   {
                                       PC.Id,
                                       PC.Name,
                                       PC.Image,
                                       PC.Modified,
                                       PC.Active,
                                       PC.Deleted
                                   }).OrderByDescending(x => x.Modified).ToList();

        if (productCategoryList == null)
            throw new ApiException("error_product_category_not_found");

        if (productCategoryList != null && productCategoryList.Any() && request.IsDisplayActive)
            productCategoryList = productCategoryList.Where(pc => pc.Active.Equals(true)).ToList();

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    productCategoryList = productCategoryList.OrderByDescending(x => x.Name).ToList();
                else
                    productCategoryList = productCategoryList.OrderBy(x => x.Name).ToList();
                break;

            default:

                productCategoryList = productCategoryList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        var total = productCategoryList.Count();
        var totalPages = ProductCategoryHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = productCategoryList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Product Category List

    #region 4. Get Product Category List

    public async Task<List<ProductCategoryListResponse>> ProductCategoryListAsync([FromBody] ProductCategoryListRequest request)
    {
        var productCategoryList = (from PC in DbContext.ProductCategories
                                   where (string.IsNullOrWhiteSpace(request.Name)
                                        || PC.Name.ToLower().Contains(request.Name.ToLower()))

                                        && PC.Deleted.Equals(request.IsDeleted)

                                   select new ProductCategoryListResponse
                                   {
                                       Id = PC.Id,
                                       Name = PC.Name,
                                       Image = PC.Image,
                                       Modified = PC.Modified,
                                       Created = PC.Created,
                                       Active = PC.Active,
                                       //Deleted = PC.Deleted
                                   }).OrderByDescending(x => x.Modified).ToList();

        if (productCategoryList == null)
            throw new ApiException("error_product_category_not_found");

        if (productCategoryList != null && productCategoryList.Any() && request.IsDisplayActive)
            productCategoryList = productCategoryList.Where(pc => pc.Active.Equals(true)).ToList();

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    productCategoryList = productCategoryList.OrderByDescending(x => x.Name).ToList();
                else
                    productCategoryList = productCategoryList.OrderBy(x => x.Name).ToList();
                break;

            default:

                productCategoryList = productCategoryList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return productCategoryList;
    }

    #endregion 4. Get Product Category List

    #region 5. Delete Product Category

    public async Task<BaseResponse> ProductCategoryDelete([FromBody] BaseIdRequest request)
    {
        var product = DbContext.ProductCategories.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (product == null)
            throw new ApiException("error_product_category_not_found");

        if (product.Active == true && product.Deleted == false)
        {
            throw new ApiException("error_product_category_active");
        }
        else
        {
            product.Deleted = true;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 5. Delete Product Category

    #region 6. Product Category Status Update

    public async Task<BaseResponse> ProductCategoryStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var productCategory = await DbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

        if (productCategory == null)
            throw new ApiException("error_product_category_not_found");

        productCategory.Active = request.Active;
        DbContext.Update(productCategory);
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Product Category Status Update

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}