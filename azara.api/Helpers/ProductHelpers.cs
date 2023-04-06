namespace azara.api.Helpers;

public class ProductHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IUploadManager UploadManager { get; set; }

    public ProductHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IUploadManager UploadManager = null)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.UploadManager = UploadManager;
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

    #region 1. Insert Product

    public async Task<BaseResponse> ProductInsert([FromBody] ProductInsertUpdateRequest request, Guid adminId)
    {
        if (DbContext.Product.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_product_name_exist" });

        decimal originalPrice = request.OriginalPrice.All(a => Char.IsDigit(a)) ? Convert.ToDecimal(request.OriginalPrice) : 0;

        string offerLabel = string.Empty;

        decimal offerAmount = 0m;

        if (request.OfferId != null)
        {
            var offerDetails = await DbContext.Offers.Where(w => !w.Deleted && w.Id.Equals(request.OfferId)).Select(s => new
            {
                OfferName = s.Name,
                OfferAmount = s.Amount
            }).FirstOrDefaultAsync();

            if (offerDetails != null)
            {
                offerAmount = offerDetails.OfferAmount;
                offerLabel = offerDetails.OfferName;
            }
        }

        await DbContext.AddRangeAsync(new ProductEntity
        {
            //Image = !string.IsNullOrWhiteSpace(request.Image) ? request.Image : string.Empty,
            Image = request.ImageList != null && request.ImageList.Any() ? JsonConvert.SerializeObject(request.ImageList) : string.Empty,
            Name = request.Name,
            Description = request.Description,
            OriginalPrice = originalPrice,
            DiscountedPrice = originalPrice - ((originalPrice / 100) * offerAmount),
            OfferId = request.OfferId,
            ProductCategoryId = request.ProductCategoryId,
            ModifiedBy = adminId,
            ProductSubCategoryId = request.ProductSubCategoryId,
            RewardsId = request.RewardsId,
            StoreDropdownId = request.StoreDropdownId
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Product

    #region 2. Update Product

    public async Task<ProductUpdateResponse> ProductUpdate([FromBody] ProductInsertUpdateRequest request)
    {
        var product = DbContext.Product.FirstOrDefault(x => x.Id.Equals(Guid.Parse(request.Id)));

        if (product == null)
            throw new ApiException("error_product_not_found");

        if (!product.Name.Equals(request.Name) && DbContext.Product.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            throw new ApiException("error_product_name_exists");

        decimal originalPrice = request.OriginalPrice.Where(w => char.IsNumber(w)).All(a => char.IsDigit(a)) ? Convert.ToDecimal(request.OriginalPrice) : 0;

        string offerLabel = string.Empty;

        decimal offerAmount = 0m;

        if (request.OfferId != null)
        {
            var offerDetails = await DbContext.Offers.Where(w => !w.Deleted && w.Id.Equals(request.OfferId)).Select(s => new
            {
                OfferName = s.Name,
                OfferAmount = s.Amount
            }).FirstOrDefaultAsync();

            if (offerDetails != null)
            {
                offerAmount = offerDetails.OfferAmount;
                offerLabel = offerDetails.OfferName;
            }
        }

        //product.Image = request.Image ?? product.Image;
        product.Image = request.ImageList != null && request.ImageList.Any() ? JsonConvert.SerializeObject(request.ImageList) : string.Empty;
        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        product.OriginalPrice = originalPrice;
        product.OfferId = request.OfferId ?? product.OfferId;
        product.DiscountedPrice = offerAmount > 0 ? originalPrice - ((originalPrice / 100) * offerAmount) : product.DiscountedPrice;
        product.ProductCategoryId = request.ProductCategoryId ?? product.ProductCategoryId;
        product.ModifiedBy = request.ModifiedBy ?? product.ModifiedBy;
        product.ProductSubCategoryId = request.ProductSubCategoryId;
        product.RewardsId = request.RewardsId;
        product.Modified = DateTime.UtcNow;
        product.StoreDropdownId = request.StoreDropdownId;

        var response = new ProductUpdateResponse
        {
            Image = product.Image,
            Name = product.Name,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            DiscountedPrice = product.DiscountedPrice,
            OfferId = product.OfferId,
            ProductCategoryId = product.ProductCategoryId,
            RewardsId = product.RewardsId,
            ModifiedBy = product.ModifiedBy,
            StoreDropdownId = product.StoreDropdownId
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Product

    #region 3. Product Get By Id

    public async Task<ProductGetByIdResponse> ProductGetById([FromBody] BaseIdRequest request)
    {
        var product = await DbContext.Product.Where(x => x.Id.Equals(request.Id))
                    .Include(i => i.ProductCategoryEntity)
                    .Include(i => i.ProductSubCategoryEntity)
                    .Include(i => i.RewardsEntity)
                    .Include(i => i.OffersEntity).FirstOrDefaultAsync();

        if (product == null)
            throw new ApiException("error_product_not_found");

        var response = new ProductGetByIdResponse
        {
            Image = product.Image,
            Name = product.Name,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            DiscountedPrice = product.DiscountedPrice,
            OfferId = product.OfferId,
            OfferName = product.OffersEntity.Name,
            ProductCategoryId = product.ProductCategoryId,
            ModifiedBy = product.ModifiedBy,
            ProductCategoryName = product.ProductCategoryEntity.Name,
            ProductSubCategoryId = product.ProductSubCategoryId,
            RewardsId = product.RewardsId,
            RewardsName = product.RewardsEntity.Name,
            StoreDropdownId = product.StoreDropdownId
        };

        return response;
    }

    #endregion 3. Product Get By Id

    #region 4. Get Product List

    public async Task<PaginationResponse> ProductGetListAsync([FromBody] GetListRequest request)
    {
        var productList = (
            from P in DbContext.Product
            join R in DbContext.Rewards on P.RewardsId equals R.Id
            //join S in DbContext.Stores on P.StoreDropdownId equals S.Id.ToString()
            join PC in DbContext.ProductCategories on P.ProductCategoryId equals PC.Id
            join O in DbContext.Offers on P.OfferId equals O.Id
            into t
            from rt in t.DefaultIfEmpty()
                //orderby P.OfferId
            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                    || P.Name.ToLower().Contains(request.Name.ToString().ToLower()))

            && (string.IsNullOrWhiteSpace(request.ProductCategoryName.ToString())
            || PC.Name.ToLower().Contains(request.ProductCategoryName.ToString().ToLower()))

            //&& (string.IsNullOrWhiteSpace(request.StoreName.ToString())
            //|| S.Name.ToLower().Contains(request.StoreName.ToString().ToLower()))

            && (string.IsNullOrWhiteSpace(request.RewardsName.ToString())
            || R.Name.ToLower().Contains(request.RewardsName.ToString().ToLower()))

            && P.Deleted.Equals(request.IsDeleted)

            select new
            {
                Id = P.Id,
                Image = !string.IsNullOrWhiteSpace(P.Image) && JsonConvert.DeserializeObject<List<string>>(P.Image) != null ? JsonConvert.DeserializeObject<List<string>>(P.Image).FirstOrDefault() : null,
                Name = P.Name,
                Descripition = P.Description,
                OriginalPrice = P.OriginalPrice,
                DiscountedPrice = P.DiscountedPrice,
                //StoreName = S.Name,
                ProductCategoryName = PC.Name,
                OfferName = rt.Name,
                RewardsName = R.Name,
                Modified = P.Modified,
                ModifiedBy = P.ModifiedBy,
                RewardId = P.RewardsId,
                StoreDropdownId = P.StoreDropdownId,
                Active = P.Active
            }
            ).OrderByDescending(x => x.Modified).ToList();

        if (productList == null)
            throw new ApiException("error_product_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "category":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.ProductCategoryName).ToList();
                else
                    productList = productList.OrderBy(x => x.ProductCategoryName).ToList();
                break;

            case "price":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.OriginalPrice).ToList();
                else
                    productList = productList.OrderBy(x => x.OriginalPrice).ToList();
                break;

            case "discount":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.DiscountedPrice).ToList();
                else
                    productList = productList.OrderBy(x => x.DiscountedPrice).ToList();
                break;

            //case "store":

            //    if (sort.Length > 1 && sort[1] == "desc")
            //        productList = productList.OrderByDescending(x => x.StoreName).ToList();
            //    else
            //        productList = productList.OrderBy(x => x.StoreName).ToList();
            //    break;

            default:

                productList = productList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (productList != null && productList.Any() && request.IsDisplayActive)
            productList = productList.Where(pc => pc.Active.Equals(true)).ToList();



        var total = productList.Count();
        var totalPages = ProductHelpers.CalculateTotalPages(total, request.PageSize);
        var productPaginationList = productList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

        var response = new
        {
            Total = total,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            Offset = request.PageNo,
            Details = productPaginationList
        };

        return new PaginationResponse
        {
            Total = response.Total,
            TotalPages = response.Total,
            OffSet = response.Offset,
            Details = response.Details
        };
    }

    #endregion 4. Get Product List

    #region 5. Product List without Pagination

    public async Task<List<ProductListResponse>> ProductListAsync([FromBody] ProductListRequest request)
    {
        var productList = (
            from P in DbContext.Product
            join R in DbContext.Rewards on P.RewardsId equals R.Id
            //join S in DbContext.Stores on P.StoreDropdownId equals S.Id.ToString()
            join PC in DbContext.ProductCategories on P.ProductCategoryId equals PC.Id
            join O in DbContext.Offers on P.OfferId equals O.Id
            into t
            from rt in t.DefaultIfEmpty()
            orderby P.OfferId
            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                    || P.Name.ToLower().Contains(request.Name.ToString().ToLower()))

            && (string.IsNullOrWhiteSpace(request.ProductCategoryName.ToString())
            || PC.Name.ToLower().Contains(request.ProductCategoryName.ToString().ToLower()))

            //&& (string.IsNullOrWhiteSpace(request.StoreName.ToString())
            //|| S.Name.ToLower().Contains(request.StoreName.ToString().ToLower()))

            && (string.IsNullOrWhiteSpace(request.RewardsName.ToString())
            || R.Name.ToLower().Contains(request.RewardsName.ToString().ToLower()))

            && !P.Deleted
            && P.Active

            select new ProductListResponse
            {
                Id = P.Id,
                Image = !string.IsNullOrWhiteSpace(P.Image) && JsonConvert.DeserializeObject<List<string>>(P.Image) != null ? JsonConvert.DeserializeObject<List<string>>(P.Image).FirstOrDefault() : null,
                Name = P.Name,
                Description = P.Description,
                OriginalPrice = P.OriginalPrice,
                DiscountedPrice = P.DiscountedPrice,
                //StoreName = S.Name,
                ProductCategoryName = PC.Name,
                OfferName = rt.Name,
                RewardsName = R.Name,
                Modified = P.Modified,
                ModifiedBy = P.ModifiedBy,
                RewardsId = P.RewardsId,
                StoreDropdownId = P.StoreDropdownId,
            }
            ).OrderByDescending(x => x.Modified).ToList();

        if (productList == null)
            throw new ApiException("error_product_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "category":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.ProductCategoryName).ToList();
                else
                    productList = productList.OrderBy(x => x.ProductCategoryName).ToList();
                break;

            case "price":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.OriginalPrice).ToList();
                else
                    productList = productList.OrderBy(x => x.OriginalPrice).ToList();
                break;

            case "discount":

                if (sort.Length > 1 && sort[1] == "desc")
                    productList = productList.OrderByDescending(x => x.DiscountedPrice).ToList();
                else
                    productList = productList.OrderBy(x => x.DiscountedPrice).ToList();
                break;

            //case "store":

            //    if (sort.Length > 1 && sort[1] == "desc")
            //        productList = productList.OrderByDescending(x => x.StoreName).ToList();
            //    else
            //        productList = productList.OrderBy(x => x.StoreName).ToList();
            //    break;

            default:

                productList = productList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return productList;
    }

    #endregion 5. Product List without Pagination

    #region 6. Delete Product

    public async Task<BaseResponse> ProductDelete([FromBody] BaseIdRequest request)
    {
        var product = DbContext.Product.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (product == null)
            throw new ApiException("error_product_not_found");

        //product.Deleted = true;
        if (product.Deleted == false)
        {
            product.Deleted = true;
        }

        product.Active = false;
        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Product

    //#region 7. Upload Product Image
    //public async Task<BaseResponse> UploadProductImage(FileWithNameUploadRequest request,IFileManagerLogic fileUpload)
    //{
    //    //return await UploadManager.Store(request.File, Guid.NewGuid().ToString(), "product");
    //    var imageUrl = await fileUpload.Upload(request.File, request.FileName, BlobContainerConsts.Profile);

    //    return new BaseResponse { IsSuccess = true };
    //}
    //#endregion

    #region 8. Get Store & Product Category

    public async Task<StoreProductCategoryResponse> GetStoreProductCategory(LocationDetailRequest request)
    {
        StoreProductCategoryResponse storeProduct = new();
        if (request != null && !string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            storeProduct = new StoreProductCategoryResponse
            {
                ProductCategoryList = await DbContext.ProductCategories.Where(w => !w.Deleted).Select(s => new ApiBaseDropdownResponse
                {
                    Id = s.Id.ToString(),
                    Name = s.Name
                }).ToListAsync(),
                StoreList = await DbContext.Stores.Where(w => !w.Deleted).Select(s => new StoreDropDownResponse
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Lat = s.Latitude,
                    Long = s.Longitude
                }).ToListAsync(),
                OfferLabelList = await DbContext.Offers.Where(w => !w.Deleted).Select(s => new ApiBaseDropdownResponse
                {
                    Id = s.Id.ToString(),
                    Name = s.Name
                }).ToListAsync()
            };

            foreach (var item in storeProduct.StoreList.Where(w => !string.IsNullOrWhiteSpace(w.Long) && !string.IsNullOrWhiteSpace(w.Lat)))
            {
                if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Lat), Convert.ToDouble(item.Long)))
                    item.IsLocationFound = true;
            }
        }
        return storeProduct;
    }

    #endregion 8. Get Store & Product Category

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}