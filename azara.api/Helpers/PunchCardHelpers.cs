using azara.models.Requests.PunchCard;
using azara.models.Responses.PunchCards;

namespace azara.api.Helpers
{
    public class PunchCardHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        IMapper Mapper { get; set; }

        ICSVService CSVService { get; set; }

        public PunchCardHelpers(
            AzaraContext DbContext,
            ICrypto Crypto,
            IMapper Mapper,
            ICSVService cSVService)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
            this.Mapper = Mapper;
            CSVService = cSVService;
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

        #region 1. Insert PunchCard

        public async Task<BaseResponse> PunchCardInsert([FromBody] PunchCardInsertUpdateRequest request)
        {
            try
            {
                if (DbContext.PunchCards.Any(x => x.PunchCardName.ToLower().Equals(request.PunchCardName.ToLower())))
                    return await Task.FromResult(new BaseResponse { IsSuccess = false, ErrorMessage = "error_name_exist" });

                await DbContext.AddRangeAsync(new PunchCardEntity
                {
                    PunchCardName = request.PunchCardName,
                    Image = request.Image,
                    Description = request.Description,
                    PunchCardPromos = request.PunchCardPromos,
                    ExpiryDate = DateTime.UtcNow.AddDays(21),
                    Active = request.Active
                });

                await DbContext.SaveChangesAsync();

                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception e)
            {

                return new BaseResponse { ErrorMessage = e.Message };
            }
        }

        #endregion 1. Insert PunchCard

        #region 2. Update PunchCard

        public async Task<PunchCardUpdateResponse> PunchCardUpdate([FromBody] PunchCardInsertUpdateRequest request)
        {
            var punchCard = DbContext.PunchCards.FirstOrDefault(x => x.Id.ToString().Equals(request.Id.ToString()));

            if (punchCard == null)
                throw new ApiException("error_punchcard_not_found");

            punchCard.PunchCardName = request.PunchCardName ?? punchCard.PunchCardName;
            punchCard.Image = request.Image;
            punchCard.Description = request.Description ?? punchCard.Description;
            punchCard.PunchCardPromos = request.PunchCardPromos ?? punchCard.PunchCardPromos;
            punchCard.ExpiryDate = DateTime.UtcNow.AddDays(21);
            punchCard.Active = request.Active;
            punchCard.Modified = request.Modified;

            var response = new PunchCardUpdateResponse
            {
                PunchCardName = punchCard.PunchCardName,
                Image = punchCard.Image,
                Description = punchCard.Description,
                PunchCardPromos = punchCard.PunchCardPromos,
                ExpiryDate = DateTime.UtcNow.AddDays(21),
                Active = punchCard.Active
            };

            DbContext.SaveChanges();

            return response;
        }

        #endregion 2. Update PunchCard

        #region 3. PunchCard Get By Id

        public async Task<PunchCardGetByIdResponse> PunchCardGetById([FromBody] BaseIdRequest request)
        {
            var punchCard = await DbContext.PunchCards.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            if (punchCard == null)
                throw new ApiException("error_punchcard_not_found");

            var response = new PunchCardGetByIdResponse
            {
                PunchCardName = punchCard.PunchCardName,
                Image = punchCard.Image,
                Description = punchCard.Description,
                PunchCardPromos = punchCard.PunchCardPromos,
                ExpiryDate = punchCard.ExpiryDate,
                Active = punchCard.Active
            };

            return response;
        }

        #endregion 3. PunchCard Get By Id

        #region 4. Get PunchCard List

        public async Task<PaginationResponse> PunchCardGetListAsync([FromBody] PunchCardGetListRequest request)
        {
            var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.PunchCardId);

            var punchCardList = (
                from P in DbContext.PunchCards

                where (string.IsNullOrWhiteSpace(request.PunchCardName.ToLower())
                    || P.PunchCardName.ToLower().Contains(request.PunchCardName.ToLower()))

                    && P.Deleted.Equals(request.IsDeleted)

                select new PunchCardsListResponse
                {
                    Id = P.Id,
                    PunchCardName = P.PunchCardName,
                    Image = P.Image,
                    Description = P.Description,
                    PunchCardPromos = P.PunchCardPromos,
                    ExpiryDate = P.ExpiryDate,
                    Active = P.Active,
                    IsUsed = rewards.Contains(P.Id),
                    Modified = P.Modified,
                    Created = P.Created
                }).OrderByDescending(x => x.Created).ToList();

            if (punchCardList == null)
                throw new ApiException("error_punchcard_not_found");

            string[] sort = request.SortBy.ToLower().Split("_");

            switch (sort[0])
            {
                case "name":

                    if (sort.Length > 1 && sort[1] == "desc")
                        punchCardList = punchCardList.OrderByDescending(x => x.PunchCardName).ToList();
                    else
                        punchCardList = punchCardList.OrderBy(x => x.PunchCardName).ToList();
                    break;

                default:

                    punchCardList = punchCardList.OrderByDescending(x => x.Modified).ToList();
                    break;
            }

            if (punchCardList != null && punchCardList.Any() && request.IsDisplayActive)
                punchCardList = punchCardList.Where(pc => pc.Active.Equals(true)).ToList();

            var total = punchCardList.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var contestPaginationList = punchCardList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = contestPaginationList
            };

            return new PaginationResponse
            {
                Total = response.Total,
                TotalPages = response.Total,
                OffSet = response.Offset,
                Details = response.Details
            };
        }

        #endregion 4. Get PunchCard List

        #region 5. PunchCard List Without Pagination

        public async Task<List<PunchCardsListResponse>> PunchCardList([FromBody] PunchCardListRequest request)
        {
            var rewards = DbContext.MyReward.Where(x => x.UserId == request.UserId).Select(x => x.PunchCardId);

            var punchCardList = (
                from P in DbContext.PunchCards

                where (string.IsNullOrWhiteSpace(request.PunchCardName.ToLower())
                    || P.PunchCardName.ToLower().Contains(request.PunchCardName.ToLower()))

                    && P.Deleted.Equals(request.IsDeleted)

                select new PunchCardsListResponse
                {
                    Id = P.Id,
                    PunchCardName = P.PunchCardName,
                    Image = P.Image,
                    Description = P.Description,
                    PunchCardPromos = P.PunchCardPromos,
                    ExpiryDate = P.ExpiryDate,
                    Active = P.Active,
                    IsUsed = rewards.Contains(P.Id),
                    Modified = P.Modified,
                    Created = P.Created
                }).OrderByDescending(x => x.Created).ToList();

            if (punchCardList == null)
                throw new ApiException("error_punchcard_not_found");

            string[] sort = request.SortBy.ToLower().Split("_");

            switch (sort[0])
            {
                case "name":

                    if (sort.Length > 1 && sort[1] == "desc")
                        punchCardList = punchCardList.OrderByDescending(x => x.PunchCardName).ToList();
                    else
                        punchCardList = punchCardList.OrderBy(x => x.PunchCardName).ToList();
                    break;

                default:

                    punchCardList = punchCardList.OrderByDescending(x => x.Modified).ToList();
                    break;
            }

            if (punchCardList != null && punchCardList.Any() && request.IsDisplayActive)
                punchCardList = punchCardList.Where(pc => pc.Active.Equals(true)).ToList();

            return punchCardList;
        }

        #endregion 5. PunchCard List Without Pagination

        #region 6. Delete PunchCard

        public async Task<BaseResponse> PunchCardDelete([FromBody] BaseIdRequest request)
        {
            var punchCard = DbContext.PunchCards.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (punchCard == null)
                throw new ApiException("error_punchcard_not_found");

            if (punchCard.Active == true && punchCard.Deleted == false)
            {
                throw new ApiException("error_punchcard_active");
            }
            else
            {
                punchCard.Deleted = true;
            }

            DbContext.SaveChanges();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 6. Delete PunchCard

        #region 7. PunchCard Status Update

        public async Task<BaseResponse> PunchCardStatusUpdate([FromBody] BaseStatusUpdateRequest request)
        {
            var punchCardData = await DbContext.PunchCards.FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && !x.Deleted);

            if (punchCardData == null)
                throw new ApiException("error_punchcard_not_found");

            punchCardData.Active = request.Active;
            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion 7. PunchCard Status Update

        #region 8. PunchCard Add Into MyRewards Based on UserId

        public async Task<List<PunchCardGetByIdResponse>> PunchCardAddIntoMyRewardsById(string userId)
        {
            var punchCard = await (from P in DbContext.PunchCards
                                   join MR in DbContext.MyReward on P.Id equals MR.PunchCardId
                                   where MR.UserId == new Guid(userId) && !P.Deleted && P.Active
                                   select new PunchCardGetByIdResponse
                                   {
                                       PunchCardName = P.PunchCardName,
                                       Image = P.Image,
                                       Description = P.Description,
                                       ExpiryDate = P.ExpiryDate,
                                       PunchCardPromos = P.PunchCardPromos,
                                       Active = P.Active
                                   }).ToListAsync();

            if (punchCard == null)
                throw new ApiException("error_punchcard_not_found");

            return punchCard;
        }

        #endregion 8. PunchCard Add Into MyRewards Based on UserId

        #region 9. Insert PunchCard CSV

        public async Task<BaseResponse> PunchCardCSVsInsert([FromBody] PunchCardDataList request)
        {
            foreach (var punchCard in request.Details)
            {
                if (!DbContext.PunchCards.Any(x => x.PunchCardName.ToLower().Equals(punchCard.PunchCardName.ToLower())))
                {
                    await DbContext.AddRangeAsync(new PunchCardEntity
                    {
                        PunchCardName = punchCard.PunchCardName,
                        Image = "abc.jpg",
                        ExpiryDate = DateTime.UtcNow.AddDays(21),
                        Description = "sjhefgjlsdghf",
                        PunchCardPromos = punchCard.PunchCardPromos,
                        Active = true
                    });

                    await DbContext.SaveChangesAsync();
                }
            }

            return new BaseResponse { IsSuccess = true };
        }

        #endregion 9. Insert PunchCard CSV

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
