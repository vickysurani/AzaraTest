using azara.models.Requests.GoogleMap;

namespace azara.api.Helpers;

public class StoreHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    public StoreHelpers(
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

    #region 1. Insert Store

    public async Task<BaseResponse> StoreInsert([FromBody] StoreInsertRequest request)
    {
        if (DbContext.Stores.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_store_name_exists" });

        #region Vijay's Code

        //if (!string.IsNullOrWhiteSpace(request.Address))
        //{
        //    var geoLocationData = await GetLocationDetail(request.Address);
        //    if (geoLocationData != null)
        //    {
        //        request.Location = geoLocationData.Location;
        //        request.Latitude = geoLocationData.Latitude;
        //        request.Longitude = geoLocationData.Longitude;
        //    }
        //}

        #endregion Vijay's Code

        #region Amisha's Code

        if (!string.IsNullOrWhiteSpace(request.Address))
        {
            var geoLocationData = await GeoLocation(new GeoLocationRequest
            {
                Address = request.Address
            });
            if (geoLocationData != null)
            {
                request.Location = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
        }

        #endregion Amisha's Code

        await DbContext.AddRangeAsync(new StoreEntity
        {
            Name = request.Name,
            Image = request.ImageList != null && request.ImageList.Any() ? JsonConvert.SerializeObject(request.ImageList) : string.Empty,
            Location = request.Location,
            Address = request.Address,
            EmailId = request.EmailId,
            ContactNumber = request.ContactNumber,
            Description = request.Description,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Created = DateTime.UtcNow,
            Code = "OnHandstore1"
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Store

    #region 2. Update Store

    public async Task<StoreUpdateResponse> StoreUpdate([FromBody] StoreUpdateRequest request)
    {
        var store = DbContext.Stores.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (store == null)
            throw new ApiException("error_store_not_found");

        if (!store.Name.Equals(request.Name) && DbContext.Stores.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            throw new ApiException("error_store_name_exists");

        #region Vijay's Code

        //if (!string.IsNullOrWhiteSpace(request.Address))
        //{
        //    //var httpResult = await new HttpClient().GetAsync($"https://search.map.santolibre.net/api/v1/search/{request.Address}");
        //    //var result = await httpResult.Content.ReadAsStringAsync();
        //    //GeoLocationDetail geoLocationData = JsonConvert.DeserializeObject<GeoLocationDetail>(result);
        //    //if (geoLocationData != null && geoLocationData.locations != null && geoLocationData.locations.Count > 0)
        //    //{
        //    //    request.Location = geoLocationData.locations.FirstOrDefault().name;
        //    //    request.Latitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lat.ToString();
        //    //    request.Longitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lng.ToString();
        //    //}

        //    var geoLocationData = await GetLocationDetail(request.Address);
        //    if (geoLocationData != null)
        //    {
        //        request.Location = geoLocationData.Location;
        //        request.Latitude = geoLocationData.Latitude;
        //        request.Longitude = geoLocationData.Longitude;
        //    }
        //}

        #endregion Vijay's Code

        #region Amisha's Code

        if (!string.IsNullOrWhiteSpace(request.Address))
        {
            var geoLocationData = await GeoLocation(new GeoLocationRequest
            {
                Address = request.Address
            });
            if (geoLocationData != null)
            {
                request.Location = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
        }

        #endregion Amisha's Code

        store.Name = request.Name ?? store.Name;
        store.Image = request.ImageList != null && request.ImageList.Any() ? JsonConvert.SerializeObject(request.ImageList) : string.Empty;
        store.Location = request.Location;
        store.Address = request.Address ?? store.Address;
        store.EmailId = request.EmailId ?? store.EmailId;
        store.ContactNumber = request.ContactNumber ?? store.ContactNumber;
        store.Description = request.Description ?? store.Description;
        store.Latitude = request.Latitude;
        store.Longitude = request.Longitude;
        store.Modified = DateTime.UtcNow;

        var response = new StoreUpdateResponse
        {
            Name = store.Name,
            Image = store.Image,
            Location = store.Location,
            Address = store.Address,
            EmailId = store.EmailId,
            ContactNumber = store.ContactNumber,
            Description = store.Description
        };

        DbContext.SaveChanges();

        return response;
    }

    #endregion 2. Update Store

    #region 3. Store Get By Id

    public async Task<StoreGetByIdResponse> StoreGetById([FromBody] BaseRequiredIdRequest request)
    {
        var store = await DbContext.Stores.FirstOrDefaultAsync(x => x.Id.ToString().Equals(request.Id.ToString()));

        if (store == null)
            throw new ApiException("error_store_not_found");

        //var productList = await DbContext.Product
        //    .Where(p => p.StoreDropdownId == request.Id.ToString() && !p.Deleted && p.Active)
        //    .Select(s => new ProductListResponse
        //    {
        //        Id = s.Id,
        //        Image = !string.IsNullOrWhiteSpace(s.Image) && JsonConvert.DeserializeObject<List<string>>(s.Image) != null ? JsonConvert.DeserializeObject<List<string>>(s.Image).FirstOrDefault() : null,
        //        Name = s.Name,
        //        Description = s.Description,
        //        OriginalPrice = s.OriginalPrice,
        //        DiscountedPrice = s.DiscountedPrice,
        //        OfferId = s.OfferId,
        //    }).ToListAsync();

        var storeProductDetails = new List<StoreProductDetails>();

        switch (store.Code)
        {
            case "OnHandStore01":
                DbContext.POSInventory.Where(x => x.OnHandStore01 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore01
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore02":
                DbContext.POSInventory.Where(x => x.OnHandStore02 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore02
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore03":
                DbContext.POSInventory.Where(x => x.OnHandStore03 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore03
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore04":
                DbContext.POSInventory.Where(x => x.OnHandStore04 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore04
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore05":
                DbContext.POSInventory.Where(x => x.OnHandStore05 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore05
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore06":
                DbContext.POSInventory.Where(x => x.OnHandStore06 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore06
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore07":
                DbContext.POSInventory.Where(x => x.OnHandStore07 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore07
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore08":
                DbContext.POSInventory.Where(x => x.OnHandStore08 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore08
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore09":
                DbContext.POSInventory.Where(x => x.OnHandStore09 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore09
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore10":
                DbContext.POSInventory.Where(x => x.OnHandStore10 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore10
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore11":
                DbContext.POSInventory.Where(x => x.OnHandStore11 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore11
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore12":
                DbContext.POSInventory.Where(x => x.OnHandStore12 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore12
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore13":
                DbContext.POSInventory.Where(x => x.OnHandStore13 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore13
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;


            case "OnHandStore14":
                DbContext.POSInventory.Where(x => x.OnHandStore14 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore14
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore15":
                DbContext.POSInventory.Where(x => x.OnHandStore15 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore15
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore16":
                DbContext.POSInventory.Where(x => x.OnHandStore16 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore16
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore17":
                DbContext.POSInventory.Where(x => x.OnHandStore17 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Quantity = x.OnHandStore17
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore18":
                DbContext.POSInventory.Where(x => x.OnHandStore18 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore18
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore19":
                DbContext.POSInventory.Where(x => x.OnHandStore19 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore19
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;

            case "OnHandStore20":
                DbContext.POSInventory.Where(x => x.OnHandStore20 > 0 && x.Active).Select(x => new StoreProductDetails
                {
                    ListId = x.ListId,
                    ProductName = x.Attribute,
                    Image = x.Image,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Price3 = x.Price3,
                    Price4 = x.Price4,
                    Price5 = x.Price5,
                    Description = x.Description,
                    Quantity = x.OnHandStore20
                }).ToList().ForEach(x => storeProductDetails.Add(x));
                break;
        }

        var response = new StoreGetByIdResponse
        {
            Name = store.Name,
            Image = store.Image,
            Location = store.Location,
            Address = store.Address,
            EmailId = store.EmailId,
            ContactNumber = store.ContactNumber,
            Description = store.Description,
            Longitude = store.Longitude,
            Latitude = store.Latitude,
            //ProductDetail = productList,
            StoreProductDetails = storeProductDetails,
        };

        return response;
    }

    #endregion 3. Store Get By Id

    #region 4. Get Store List

    public async Task<PaginationResponse> StoreGetListAsync([FromBody] StoreGetListRequest request)
    {

        var checkname = string.IsNullOrWhiteSpace(request.Name.ToLower());
        var storeList = await (from S in DbContext.Stores

                               select new StoreListResponse
                               {
                                   Id = S.Id,
                                   Name = S.Name,
                                    //Image = S.Image,
                                   Image = !string.IsNullOrWhiteSpace(S.Image) && JsonConvert.DeserializeObject<List<string>>(S.Image) != null ? JsonConvert.DeserializeObject<List<string>>(S.Image).FirstOrDefault() : null,
                                   Location = S.Location,
                                   Address = S.Address,
                                   EmailId = S.EmailId,
                                   ContactNumber = S.ContactNumber,
                                   Description = S.Description,
                                   Latitude = S.Latitude,
                                   Longitude = S.Longitude,
                                   Modified = S.Modified,
                                   Created = S.Created,
                                   Active = S.Active,
                                   Code = S.Code
                               }).OrderByDescending(x => x.Created).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in storeList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (storeList == null)
            throw new ApiException("error_store_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    storeList = storeList.OrderByDescending(x => x.Name).ToList();
                else
                    storeList = storeList.OrderBy(x => x.Name).ToList();
                break;

            default:
                storeList = storeList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        var total = storeList.Count();
        var totalPages = StoreHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = storeList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Store List

    #region 5. Store List without Pagination

    public async Task<List<StoreListResponse>> StoreListAsync([FromBody] StoreListRequest request)
    {
        var storeList = await (from S in DbContext.Stores

                               where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                                   || S.Name.ToLower().Contains(request.Name.ToString().ToLower()))
                                   && S.Deleted.Equals(request.IsDeleted) && S.Active.Equals(true)

                               select new StoreListResponse
                               {
                                   Id = S.Id,
                                   Name = S.Name,
                                   Image = !string.IsNullOrWhiteSpace(S.Image) && JsonConvert.DeserializeObject<List<string>>(S.Image) != null ? JsonConvert.DeserializeObject<List<string>>(S.Image).FirstOrDefault() : null,
                                   Location = S.Location,
                                   Address = S.Address,
                                   EmailId = S.EmailId,
                                   ContactNumber = S.ContactNumber,
                                   Description = S.Description,
                                   Latitude = S.Latitude,
                                   Longitude = S.Longitude,
                                   Modified = S.Modified,
                                   Created = S.Created,
                                   Active = S.Active
                               }).OrderByDescending(x => x.Created).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in storeList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (storeList == null)
            throw new ApiException("error_store_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    storeList = storeList.OrderByDescending(x => x.Name).ToList();
                else
                    storeList = storeList.OrderBy(x => x.Name).ToList();
                break;

            default:
                storeList = storeList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        return storeList;
    }

    #endregion 5. Store List without Pagination

    #region 6. Delete Store

    public async Task<BaseResponse> DeleteStore([FromBody] BaseIdRequest request)
    {
        var store = DbContext.Stores.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (store == null)
            throw new ApiException("error_store_not_found");

        if (store.Deleted == false)
        {
            store.Deleted = true;
        }

        store.Active = false;

        DbContext.SaveChanges();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 6. Delete Store

    #region 7. Get Geo Location from Lat Long

    //private string getLocationByGeoLocation(string longitude, string latitude)
    //{
    //    string locationName = string.Empty;

    //    try
    //    {
    //        if (string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(latitude))
    //            return "";

    //        string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude, longitude);

    //        WebRequest request = WebRequest.Create(url);

    //        using (WebResponse response = (HttpWebResponse)request.GetResponse())
    //        {
    //            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
    //            {
    //                System.Data.DataSet dsResult = new();
    //                dsResult.ReadXml(reader);
    //                try
    //                {
    //                    foreach (System.Data.DataRow row in dsResult.Tables["result"].Rows)
    //                    {
    //                        string fullAddress = row["formatted_address"].ToString();
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new ApiException("error_longitude_latitude_InValid");
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new ApiException("Something was wrong!");
    //    }
    //    return locationName;
    //}

    #endregion 7. Get Geo Location from Lat Long

    #region 8. Get Store Based on 5 km Radius

    public bool IsGeoLocationFound(string LocationDetail, double storeLat, double storeLong)
    {
        if (!string.IsNullOrWhiteSpace(LocationDetail))
        {
            bool isFound = false;
            List<double> locationDetail = JsonConvert.DeserializeObject<List<double>>(LocationDetail);
            if (locationDetail != null && locationDetail.Count > 0)
            {
                double userLat = locationDetail.LastOrDefault(); //Convert.ToDouble(23.063616);
                double userLong = locationDetail.FirstOrDefault(); //Convert.ToDouble(72.531605);

                if (storeLat >= userLat && storeLong >= userLong) // 1 km to 0.008 degree
                    isFound = userLat + 0.045 >= storeLat && userLong + 0.045 >= storeLong;
                else if (storeLat <= userLat && storeLong >= userLong)
                    isFound = userLat - 0.045 <= storeLat && userLong + 0.045 >= storeLong;
                else if (storeLat <= userLat && storeLong <= userLong)
                    isFound = userLat - 0.045 <= storeLat && userLong - 0.045 <= storeLong;
                else if (storeLat >= userLat && storeLong <= userLong)
                    isFound = userLat + 0.045 >= storeLat && userLong - 0.045 <= storeLong;
            }
            return isFound;
        }
        else
            return false;
    }

    #endregion 8. Get Store Based on 5 km Radius

    #region 9. Set the geoLocation Data

    //public async Task<SetLocaionDetail> GetLocationDetail(string address)
    //{
    //    try
    //    {
    //        address = CompressAddress(address);
    //        var requestMessage = new HttpRequestMessage
    //        {
    //            Method = HttpMethod.Get,
    //            RequestUri = new Uri($"https://nominatim.openstreetmap.org/search?format=json&q={address}"),
    //            Headers =
    //                    {
    //                        { "user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)" },
    //                        { "Referer", "http://www.microsoft.com" }
    //                    }
    //        };
    //        using var data = await new HttpClient().SendAsync(requestMessage);
    //        var body = await data.Content.ReadAsStringAsync();
    //        if (data.StatusCode == HttpStatusCode.OK && body != null && !body.Equals("[]"))
    //        {
    //            var locationDetails = JsonConvert.DeserializeObject<List<GeoLocationDetail>>(body);
    //            if (locationDetails != null && locationDetails.Count > 0)
    //            {
    //                var locationDetail = locationDetails.FirstOrDefault();
    //                return new SetLocaionDetail
    //                {
    //                    //Latitude = locationDetail.lat,
    //                    //Location = locationDetail.display_name,
    //                    //Longitude = locationDetail.lon
    //                };
    //            }
    //        }
    //        else
    //        {
    //            throw new Exception("Please enter valid location");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        throw new Exception("Please enter valid location");
    //    }
    //    return null;
    //}

    #endregion 9. Set the geoLocation Data

    #region 10. Store Status Update

    public async Task<BaseResponse> StoreStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var store = await DbContext.Stores.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (store == null)
            throw new ApiException("error_store_not_found");

        store.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 10. Store Status Update

    #region 11. Google Map Api (New Integration)

    public async Task<SetLocaionDetail> GeoLocation([FromBody] GeoLocationRequest geoLocationRequest)
    {
        try
        {
            geoLocationRequest.Address = CompressAddress(geoLocationRequest.Address);
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://maps.googleapis.com/maps/api/geocode/json?address={geoLocationRequest.Address}&key={geoLocationRequest.GoogleApiKey}")
            };
            using var data = await new HttpClient().SendAsync(requestMessage);
            var body = await data.Content.ReadAsStringAsync();
            if (data.StatusCode == HttpStatusCode.OK && body != null && !body.Equals("[]"))
            {
                var locationDetails = JsonConvert.DeserializeObject<GeoLocationDetail>(body);
                if (locationDetails != null && locationDetails.results.Count > 0)
                {
                    return new SetLocaionDetail
                    {
                        Latitude = locationDetails.results[0].geometry.location.lat.ToString(),
                        Location = locationDetails.results[0].formatted_address,
                        Longitude = locationDetails.results[0].geometry.location.lng.ToString()
                    };
                }
            }
            else
            {
                throw new Exception("Please enter valid location");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Please enter valid location");
        }
        return null;
    }

    #endregion Google Map Api (New Integration)

    private string CompressAddress(string address)
    {
        string result = string.Empty;
        string[] data = address.Split(' ');
        foreach (var item in data)
        {
            result += item;
            if (data.LastOrDefault() != item)
                result += "+";
        }
        return result;
    }

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}