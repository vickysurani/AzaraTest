namespace azara.api.Helpers;

public class ProductSubCategoryHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public ProductSubCategoryHelpers(
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

    #region 1. Insert Product Sub Category

    public async Task<BaseResponse> ProductSubCategoryInsert([FromBody] ProductSubCategoryInsertRequest request)
    {
        if (DbContext.ProductSubCategory.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

        await DbContext.AddRangeAsync(new ProductSubCategoryEntity
        {
            ProductCategoryId = request.ProductCategoryId,
            Name = request.Name,
            Image = request.Image,
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Product Sub Category

    #region 2. Update Product Sub Category

    public async Task<ProductSubCategoryUpdateResponse> ProductSubCategoryUpdate([FromBody] ProductSubCategoryUpdateRequest request)
    {
        var productSubCategory = DbContext.ProductSubCategory.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (productSubCategory == null)
            throw new ApiException("error_product_sub_category_not_found");

        // Validation for duplicate Name
        if (!productSubCategory.Name.Equals(request.Name) && DbContext.ProductSubCategory.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            throw new ApiException("error_product_sub_category_name_exist");

        productSubCategory.ProductCategoryId = request.ProductCategoryId ?? productSubCategory.ProductCategoryId;
        productSubCategory.Name = request.Name ?? productSubCategory.Name;
        productSubCategory.Image = request.Image ?? productSubCategory.Image;

        var response = new ProductSubCategoryUpdateResponse
        {
            ProductCategoryId = productSubCategory.ProductCategoryId,
            Name = productSubCategory.Name,
            Image = productSubCategory.Image,
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Product Sub Category

    #region 3. Product Sub Category Get By Id

    public async Task<ProductSubCategoryGetByIdResponse> ProductSubCategoryGetById([FromBody] BaseIdRequest request)
    {
        var productSubCategory = await DbContext.ProductSubCategory.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

        if (productSubCategory == null)
            throw new ApiException("error_product_sub_category_not_found");

        var response = new ProductSubCategoryGetByIdResponse
        {
            ProductCategoryId = productSubCategory.ProductCategoryId,
            Name = productSubCategory.Name,
            Image = productSubCategory.Image,
        };

        return response;
    }

    #endregion 3. Product Sub Category Get By Id

    #region 4. Get Product Sub Category List

    public async Task<PaginationResponse> ProductSubCategoryGetListAsync([FromBody] ProductSubCategoryGetListRequest request)
    {
        var productSubCategoryList = (from PS in DbContext.ProductSubCategory
                                      join P in DbContext.ProductCategories on PS.ProductCategoryId equals P.Id

                                      where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                                        || P.Name.ToLower().Contains(request.Name.ToLower()))

                                        && P.Deleted.Equals(request.IsDeleted)

                                        && PS.Deleted.Equals(request.IsDeleted)

                                      select new ProductSubCategoryListResponse
                                      {
                                          Id = PS.Id,
                                          ProductCategoryName = P.Name,
                                          Name = PS.Name,
                                          Image = PS.Image,
                                          Status = PS.Active,
                                          Modified = PS.Modified,
                                          Created = PS.Created,
                                      }).OrderByDescending(x => x.Created).ToList();

        if (productSubCategoryList == null)
            throw new ApiException("error_product_sub_category_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    productSubCategoryList = productSubCategoryList.OrderByDescending(x => x.Name).ToList();
                else
                    productSubCategoryList = productSubCategoryList.OrderBy(x => x.Name).ToList();
                break;

            default:

                productSubCategoryList = productSubCategoryList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        var total = productSubCategoryList.Count();
        var totalPages = ProductSubCategoryHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = productSubCategoryList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Product Sub Category List

    #region 5. Store List without Pagination

    public async Task<List<ProductSubCategoryListResponse>> ProductSubCategoryListAsync([FromBody] ProductSubCategoryListRequest request)
    {
        var productSubCategoryList = (from PS in DbContext.ProductSubCategory
                                      join P in DbContext.ProductCategories on PS.ProductCategoryId equals P.Id

                                      where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                                        || P.Name.ToLower().Contains(request.Name.ToLower()))

                                        && P.Deleted.Equals(request.IsDeleted)

                                        && PS.Deleted.Equals(request.IsDeleted)

                                      select new ProductSubCategoryListResponse
                                      {
                                          Id = PS.Id,
                                          ProductCategoryName = P.Name,
                                          Name = PS.Name,
                                          Image = PS.Image,
                                          Status = PS.Active,
                                          Modified = PS.Modified,
                                          Created = PS.Created,
                                      }).OrderByDescending(x => x.Created).ToList();

        if (productSubCategoryList == null)
            throw new ApiException("error_product_sub_category_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    productSubCategoryList = productSubCategoryList.OrderByDescending(x => x.Name).ToList();
                else
                    productSubCategoryList = productSubCategoryList.OrderBy(x => x.Name).ToList();
                break;

            default:

                productSubCategoryList = productSubCategoryList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return productSubCategoryList;
    }

    #endregion 5. Store List without Pagination

    #region 6. Delete Product Sub Category

    public async Task<BaseResponse> ProductSubCategoryDelete([FromBody] BaseIdRequest request)
    {
        var product = DbContext.ProductSubCategory.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (product == null)
            throw new ApiException("error_product_sub_category_not_found");

        if (product.Active == true && product.Deleted == false)
        {
            throw new ApiException("error_product_sub_category_active");
        }
        else
        {
            product.Deleted = true;
        }

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Product Sub Category

    #region 7. Product Sub Category Status Update

    public async Task<BaseResponse> ProductSubCategoryStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var productSubCategory = await DbContext.ProductSubCategory.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (productSubCategory == null)
            throw new ApiException("error_product_sub_category_not_found");

        productSubCategory.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Product Sub Category Status Update

    #region 8. Get ProductSubCategory By ProductCategoryId

    public async Task<List<ApiBaseDropdownResponse>> GetProductCategoryList([FromBody] BaseIdRequest request)
    {
        var productSubCategory = await DbContext.ProductSubCategory.Where(x => x.ProductCategoryId.Equals(request.Id) && !x.Deleted && x.Active)
                                    .Select(s => new ApiBaseDropdownResponse
                                    {
                                        Id = s.Id.ToString(),
                                        Name = s.Name,
                                        Image = s.Image
                                    }).ToListAsync();

        if (productSubCategory == null)
            throw new ApiException("error_product_sub_category_not_found");

        return productSubCategory;
    }

    #endregion 8. Get ProductSubCategory By ProductCategoryId

    #region 9. Get Product List

    public async Task<ProductSubCategoryGetListResponse> GetProductSubCategoryProduct([FromBody] BaseIdRequest request)
    {
        return new ProductSubCategoryGetListResponse
        {
            ProductList = await DbContext.Product.Where(w => !w.Deleted && w.ProductSubCategoryId.Equals(request.Id))
            .Select(p => new ProductGetByIdResponse
            {
                Image = !string.IsNullOrWhiteSpace(p.Image) ? JsonConvert.DeserializeObject<List<string>>(p.Image).FirstOrDefault() : string.Empty,
                Name = p.Name,
                Description = p.Description,
                OriginalPrice = p.OriginalPrice,
                DiscountedPrice = p.DiscountedPrice,
                OfferId = p.OfferId,
            }).ToListAsync()
        };
    }

    #endregion 9. Get Product List

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}