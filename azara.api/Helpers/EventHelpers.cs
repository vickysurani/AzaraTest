using azara.models.Requests.GoogleMap;

namespace azara.api.Helpers;

public class EventHelpers : IDisposable
{
    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    IUploadManager UploadManager { get; set; }

    public EventHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper,
        IUploadManager UploadManager = null)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.UploadManager = UploadManager;
        this.Mapper = Mapper;
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

    #region 1. Insert Event

    public async Task<BaseResponse> InsertEvent([FromBody] EventInsertUpdateRequest request)
    {
        if (DbContext.Events.Any(x => x.Name.ToLower().Equals(request.Name.ToLower()) && !x.Deleted))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

        if (!string.IsNullOrWhiteSpace(request.EventLocation))
        {
            var geoLocationData = await new StoreHelpers(DbContext, Crypto).GeoLocation(new GeoLocationRequest
            {
                Address = request.EventLocation
            });
            if (geoLocationData == null)
            {
                throw new ApiException("Please enter valid location");
            }
            else
            {
                request.EventLocation = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
        }

        request.EventStartTime = UpdateEventTime(request.EventDate, request.EventStartTime);
        request.EventEndTime = UpdateEventTime(request.EventDate, request.EventEndTime);

        var eventEntity = new EventsEntity();
        Mapper.Map(request, eventEntity);
        await DbContext.AddRangeAsync(eventEntity);
        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 1. Insert Event

    #region 2. Update Event

    public async Task<BaseResponse> UpdateEvent([FromBody] EventInsertUpdateRequest request)
    {
        var eventData = DbContext.Events.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()) && !x.Deleted);

        if (eventData == null)
            throw new ApiException("error_event_not_found");

        // Validation for duplicate Name
        if (!eventData.Name.Equals(request.Name) && DbContext.Events.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
            throw new ApiException("error_event_name_exist");

        if (!string.IsNullOrWhiteSpace(request.EventLocation))
        {
            //var httpResult = await new HttpClient().GetAsync($"https://search.map.santolibre.net/api/v1/search/{request.Address}");
            //var result = await httpResult.Content.ReadAsStringAsync();
            //GeoLocationDetail geoLocationData = JsonConvert.DeserializeObject<GeoLocationDetail>(result);
            //if (geoLocationData != null && geoLocationData.locations != null && geoLocationData.locations.Count > 0)
            //{
            //    request.Location = geoLocationData.locations.FirstOrDefault().name;
            //    request.Latitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lat.ToString();
            //    request.Longitude = geoLocationData.locations.FirstOrDefault().geoCoordinates.lng.ToString();
            //}

            var geoLocationData = await new StoreHelpers(DbContext, Crypto).GeoLocation(new GeoLocationRequest
            {
                Address = request.EventLocation
            });
            if (geoLocationData != null)
            {
                request.EventLocation = geoLocationData.Location;
                request.Latitude = geoLocationData.Latitude;
                request.Longitude = geoLocationData.Longitude;
            }
        }

        request.EventStartTime = UpdateEventTime(request.EventDate, request.EventStartTime);
        request.EventEndTime = UpdateEventTime(request.EventDate, request.EventEndTime);

        Mapper.Map(request, eventData);
        DbContext.Update(eventData);
        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 2. Update Event

    #region 3. Delete Event

    public async Task<BaseResponse> DeleteEvent([FromBody] BaseIdRequest request)
    {
        var eventData = DbContext.Events.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (eventData == null)
            throw new ApiException("error_event_not_found");

        if (eventData.Active == true && eventData.Deleted == false)
        {
            throw new ApiException("error_event_active");
        }
        else
        {
            eventData.Deleted = true;
        }
        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 3. Delete Event

    #region 4. Get Event List

    public async Task<PaginationResponse> EventGetListAsync([FromBody] EventGetListRequest request)
    {
        var eventList = await (
            from E in DbContext.Events

            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || E.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                && E.Deleted.Equals(request.IsDeleted)

            select new EventListResponse
            {
                Id = E.Id,
                Name = E.Name,
                Active = E.Active,
                EventDate = E.EventDate,
                EventStartTime = E.EventStartTime,
                EventEndTime = E.EventEndTime,
                EventLocation = E.EventLocation,
                Description = E.Description,
                Image = E.Image,
                Latitude = E.Latitude,
                Longitude = E.Longitude,
                Modified = E.Modified
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in eventList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (eventList == null)
            throw new ApiException("error_event_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    eventList = eventList.OrderByDescending(x => x.Name).ToList();
                else
                    eventList = eventList.OrderBy(x => x.Name).ToList();
                break;

            case "event_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    eventList = eventList.OrderByDescending(x => x.EventDate).ToList();
                else
                    eventList = eventList.OrderBy(x => x.EventDate).ToList();
                break;

            default:

                eventList = eventList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (eventList != null && eventList.Any() && request.IsDisplayActive)
            eventList = eventList.Where(pc => pc.Active.Equals(true) && pc.EventEndTime <= DateTime.Now).ToList();

        var total = eventList.Count();
        var totalPages = ProductHelpers.CalculateTotalPages(total, request.PageSize);
        var eventPaginationList = eventList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

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

    #endregion 4. Get Event List

    #region 5. Event List Without Pagination

    public async Task<List<EventListResponse>> EventList([FromBody] EventListRequest request)
    {
        var eventList = await (
            from E in DbContext.Events

            where (string.IsNullOrWhiteSpace(request.Name.ToString().ToLower())
                || E.Name.ToLower().Contains(request.Name.ToString().ToLower()))

                && E.Deleted.Equals(request.IsDeleted)

            select new EventListResponse
            {
                Id = E.Id,
                Name = E.Name,
                Active = E.Active,
                EventDate = E.EventDate,
                EventStartTime = E.EventStartTime,
                EventEndTime = E.EventEndTime,
                EventLocation = E.EventLocation,
                Description = E.Description,
                Image = E.Image,
                Latitude = E.Latitude,
                Longitude = E.Longitude,
                Modified = E.Modified
            }).OrderByDescending(x => x.Modified).ToListAsync();

        if (!string.IsNullOrWhiteSpace(request.LocationDetail))
        {
            foreach (var item in eventList.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
            {
                if (new StoreHelpers(DbContext, Crypto).IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                    item.IsLocationFound = true;
            }
        }

        if (eventList == null)
            throw new ApiException("error_event_not_found");

        string[] sort = request.SortBy.ToLower().Split("_");

        switch (sort[0])
        {
            case "name":

                if (sort.Length > 1 && sort[1] == "desc")
                    eventList = eventList.OrderByDescending(x => x.Name).ToList();
                else
                    eventList = eventList.OrderBy(x => x.Name).ToList();
                break;

            case "event_date":

                if (sort.Length > 1 && sort[1] == "desc")
                    eventList = eventList.OrderByDescending(x => x.EventDate).ToList();
                else
                    eventList = eventList.OrderBy(x => x.EventDate).ToList();
                break;

            default:

                eventList = eventList.OrderByDescending(x => x.Modified).ToList();
                break;
        }

        if (eventList != null && eventList.Any() && request.IsDisplayActive)
            eventList = eventList.Where(pc => pc.Active.Equals(true) && pc.EventEndTime >= DateTime.Now).ToList();

        return eventList;
    }

    #endregion 5. Event List Without Pagination

    #region 6. Event Get By Id

    public async Task<InsertUpdateEventResponse> EventGetById([FromBody] BaseIdRequest request)
    {
        var eventData = await DbContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (eventData == null)
            throw new ApiException("error_event_not_found");

        var response = new InsertUpdateEventResponse();
        Mapper.Map(eventData, response);
        return response;
    }

    #endregion 6. Event Get By Id

    #region 7. Event Status Update

    public async Task<BaseResponse> EventStatusUpdate([FromBody] BaseStatusUpdateRequest request)
    {
        var eventData = await DbContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

        if (eventData == null)
            throw new ApiException("error_event_not_found");

        eventData.Active = request.Active;
        await DbContext.SaveChangesAsync();
        return new BaseResponse { IsSuccess = true };
    }

    #endregion 7. Event Status Update

    #region 8. Events Insert As PerUserId

    public async Task<BaseResponse> UserEventsInsert([FromBody] EventUsersIntertRequest request)
    {
        if (DbContext.UserEvents.Any(x => x.EventId.Equals(request.EventId) && x.UserId.Equals(request.UserId)))
            return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "event_alredy_assigned" });



        await DbContext.AddRangeAsync(new UserEventsEntity
        {
            EventId = request.EventId,
            UserId = request.UserId ?? new Guid(),
        });

        await DbContext.SaveChangesAsync();

        return new BaseResponse { IsSuccess = true };
    }

    #endregion 8. Events Insert As PerUserId

    #region 9. Event Details Get By UserId

    public async Task<List<InsertUpdateEventResponse>> EventDetailsGetByUserId([FromBody] BaseIdRequest request)
    {
        var userEventsList = await (from U in DbContext.UserEvents
                                    join E in DbContext.Events on U.EventId equals E.Id
                                    where (U.UserId.Equals(request.Id) && DateTime.Compare(E.EventDate, DateTime.Now) >= 0)

                                    select new InsertUpdateEventResponse
                                    {
                                        Id = E.Id,
                                        Name = E.Name,
                                        Image = E.Image,
                                        EventDate = E.EventDate,
                                        EventStartTime = E.EventStartTime,
                                        EventEndTime = E.EventEndTime,
                                        EventLocation = E.EventLocation,
                                        Description = E.Description,
                                        Active = E.Active,
                                        Latitude = E.Latitude,
                                        Longitude = E.Longitude
                                    }).ToListAsync();

        return userEventsList;

    }

    #endregion

    #region Common method

    private DateTime UpdateEventTime(DateTime source, DateTime destination)
    {
        return new DateTime(source.Year, source.Month, source.Day, destination.Hour, destination.Minute, destination.Second);
    }

    #endregion Common method

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}